using System;
using System.Threading;
using System.Threading.Tasks;
using Bruteforcer2.Hash;

namespace Bruteforcer2.Bruteforce
{
    public class BruteforceTask
    {
        private const int ProgressBatchSize = 4096;
        private readonly byte[] _charset;
        private readonly byte[] _input;
        private readonly int _minLength;
        private readonly int _maxLength;
        private readonly int _threadCount;
        private long _counter;
        private long _lastCounter;
        private DateTime _lastUpdate;
        private Task _task;
        private volatile bool _active;
        private volatile bool _hasResult;

        public BruteforceTask(BruteforceManager manager, IBruteforceHash hash, int taskNumber,
            char[] charset, byte[] input, int minLength, int maxLength)
        {
            BruteforceManager = manager;
            Hash = (IBruteforceHash)hash.Clone();
            TaskNumber = taskNumber;
            _charset = BruteforceManager.Encoding.GetBytes(charset);
            _input = input;
            _minLength = minLength;
            _maxLength = maxLength;
            _threadCount = manager.TaskCount;
        }

        public BruteforceManager BruteforceManager { get; }
        public IBruteforceHash Hash { get; }
        public int TaskNumber { get; }
        public long Counter => Interlocked.Read(ref _counter);
        public double Speed { get; private set; }
        public bool IsActive => _active;
        public bool HasResult => _hasResult;
        public string Result { get; private set; }

        public void Start()
        {
            if (_task != null)
                return;

            _lastUpdate = DateTime.UtcNow;
            _active = true;
            _task = Task.Factory.StartNew(() =>
            {
                try
                {
                    Bruteforce();
                }
                finally
                {
                    _active = false;
                }
            }, CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        public void WaitForCompletion() => _task?.GetAwaiter().GetResult();

        private void Bruteforce()
        {
            var charset = _charset;
            var hash = Hash;
            long total = 1;
            long counter = 0;
            try
            {
                for (var length = 1; length <= _maxLength; length++)
                {
                    total = checked(total * charset.Length);
                    if (BruteforceManager.Cancel)
                        return;
                    if (length < _minLength)
                        continue;

                    // Distribute the remainder too, including when there are fewer words than workers.
                    var quotient = total / _threadCount;
                    var remainder = total % _threadCount;
                    var count = quotient + (TaskNumber < remainder ? 1 : 0);
                    if (count == 0)
                        continue;
                    var start = quotient * TaskNumber + Math.Min((long)TaskNumber, remainder);
                    if (hash is IPreparedBruteforceHash prepared)
                    {
                        var search = prepared.CreateSearch(charset, length, start, count, _input);
                        if (search != null)
                        {
                            if (BruteforcePrepared(search, count, ref counter))
                                return;
                            continue;
                        }
                    }
                    var indices = new int[length];
                    var candidate = new byte[length];
                    for (var i = length - 1; i >= 0; i--)
                    {
                        indices[i] = (int)(start % charset.Length);
                        candidate[i] = charset[indices[i]];
                        start /= charset.Length;
                    }

                    if (hash is IBatchedBruteforceHash batched && batched.BatchSize > 1 && length <= 55)
                    {
                        if (BruteforceBatches(batched, candidate, indices, count, ref counter))
                            return;
                        continue;
                    }

                    for (long n = 0; n < count; n++)
                    {
                        if ((n & (ProgressBatchSize - 1)) == 0)
                        {
                            Interlocked.Exchange(ref _counter, counter);
                            if (BruteforceManager.Cancel)
                                return;
                        }

                        var matches = hash.Hash(candidate, 0, length).AsSpan().SequenceEqual(_input);
                        counter++;
                        if (matches)
                        {
                            Result = BruteforceManager.Encoding.GetString(candidate);
                            Interlocked.Exchange(ref _counter, counter);
                            _hasResult = true;
                            BruteforceManager.ReportResult(this);
                            return;
                        }

                        // An odometer: only update bytes affected by the carry.
                        for (var i = length - 1; i >= 0; i--)
                        {
                            var next = indices[i] + 1;
                            if (next < charset.Length)
                            {
                                indices[i] = next;
                                candidate[i] = charset[next];
                                break;
                            }
                            indices[i] = 0;
                            candidate[i] = charset[0];
                        }
                    }
                }
            }
            finally
            {
                // Preserve the final count on success, exhaustion, cancellation and failure.
                Interlocked.Exchange(ref _counter, counter);
            }
        }

        private bool BruteforcePrepared(IPreparedBruteforceSearch search, long count, ref long counter)
        {
            long processed = 0;
            while (processed < count)
            {
                Interlocked.Exchange(ref _counter, counter);
                if (BruteforceManager.Cancel)
                    return true;
                var examined = search.Search(ProgressBatchSize, out var result);
                if (examined <= 0 || examined > count - processed)
                    throw new InvalidOperationException("Invalid prepared search progress.");
                counter += examined;
                processed += examined;
                if (result != null)
                {
                    Result = BruteforceManager.Encoding.GetString(result);
                    Interlocked.Exchange(ref _counter, counter);
                    _hasResult = true;
                    BruteforceManager.ReportResult(this);
                    return true;
                }
            }
            return false;
        }

        // Existing adapters keep the scalar loop above. Only batch-capable adapters enter here.
        private bool BruteforceBatches(IBatchedBruteforceHash hash, byte[] candidate,
            int[] indices, long count, ref long counter)
        {
            var length = candidate.Length;
            var batchSize = hash.BatchSize;
            var batch = new byte[batchSize * length];
            var charset = _charset;
            long processed = 0;
            while (processed < count)
            {
                Interlocked.Exchange(ref _counter, counter);
                if (BruteforceManager.Cancel)
                    return true;

                var remaining = (int)Math.Min(ProgressBatchSize, count - processed);
                while (remaining > 0)
                {
                    var lanes = Math.Min(batchSize, remaining);
                    for (var lane = 0; lane < lanes; lane++)
                    {
                        candidate.AsSpan().CopyTo(batch.AsSpan(lane * length, length));
                        for (var i = length - 1; i >= 0; i--)
                        {
                            var next = indices[i] + 1;
                            if (next < charset.Length)
                            {
                                indices[i] = next;
                                candidate[i] = charset[next];
                                break;
                            }
                            indices[i] = 0;
                            candidate[i] = charset[0];
                        }
                    }

                    var match = hash.FindMatch(batch.AsSpan(0, lanes * length), length, lanes, _input);
                    counter += lanes;
                    processed += lanes;
                    remaining -= lanes;
                    if (match >= 0)
                    {
                        Result = BruteforceManager.Encoding.GetString(batch, match * length, length);
                        Interlocked.Exchange(ref _counter, counter);
                        _hasResult = true;
                        BruteforceManager.ReportResult(this);
                        return true;
                    }
                }
            }
            return false;
        }

        public void Update()
        {
            var now = DateTime.UtcNow;
            var counter = Counter;
            var seconds = (now - _lastUpdate).TotalSeconds;
            Speed = seconds > 0 ? (counter - _lastCounter) / seconds : 0;
            _lastCounter = counter;
            _lastUpdate = now;
        }
    }
}
