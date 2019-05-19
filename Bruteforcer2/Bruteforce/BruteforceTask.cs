using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bruteforcer2.Hash;

namespace Bruteforcer2.Bruteforce
{
    public class BruteforceTask
    {
        private volatile bool _active;
        private readonly char[] _charset;
        private volatile bool _hasResult;
        private readonly byte[] _input;
        private decimal _lastCounter;
        private readonly int _maxLength;
        private readonly int _minLength;
        public long Counter;
        private DateTime _startDateTime;
        private DateTime _endDateTime;

        // ------------------------------------------------------------

        // ------------------------------------------------------------

        public BruteforceTask(BruteforceManager manager, IBruteforceHash hash, int taskNumber, char[] charset, byte[] input,
            int minLength, int maxLength)
        {
            BruteforceManager = manager;
            _input = input;
            Hash = hash.Clone() as IBruteforceHash;
            _charset = charset;
            TaskNumber = taskNumber;
            _minLength = minLength;
            _maxLength = maxLength;
        }

        public BruteforceManager BruteforceManager { get; set; }
        public IBruteforceHash Hash { get; set; }
        public double Speed { get; set; }
        public int TaskNumber { get; set; }
        public bool HasResult
        {
            get => _hasResult;
            set => _hasResult = value;
        }

        public string Result { get; set; }

        // ------------------------------------------------------------

        public void Start()
        {
            if (!_active)
            {
                _active = true;
                new Task(() =>
                {
                    _startDateTime = DateTime.Now;
                    if (!BruteforceManager.Cancel)
                        Bruteforce(_minLength, _maxLength, TaskNumber, BruteforceManager.TaskCount);

                    _endDateTime = DateTime.Now;
                    _active = false;
                },TaskCreationOptions.LongRunning).Start();
            }
        }

        private void SkipIndexArray(long[] indexArray, int threadNumber, int charsetSize, int currentWordLength,
            long words)
        {
            var i = currentWordLength - 1;
            var wordsToSkip = words * threadNumber;

            var remainder = wordsToSkip % charsetSize;

            for (var j = 0; j < currentWordLength; j++)
            {
                var wordPos = (long) Math.Pow(charsetSize, i - j);
                var n = wordsToSkip / wordPos;
                if (n > 0)
                {
                    indexArray[j] = n;
                    wordsToSkip -= wordPos * n;
                }
                else
                {
                    if (wordsToSkip > wordPos) wordsToSkip -= wordPos;
                }
            }

            indexArray[currentWordLength - 1] = remainder;
        }

        readonly byte[] currentHashBuffer = new byte[100];

        private void Bruteforce(int minWordLength, int maxWordLength, int threadNumber, int threadCount)
        {
            var wordsPerThread = new long[maxWordLength];

            for (var i = 0; i < wordsPerThread.Length; i++)
                wordsPerThread[i] = (long) Math.Pow(_charset.Length, i + 1) / threadCount;

            var charset = _charset;
            Counter = 0;

            for (var currentWordLength = minWordLength; currentWordLength <= maxWordLength; currentWordLength++)
            {
                Console.WriteLine($"Starting with word length: {currentWordLength} on Task Number: {TaskNumber} ThreadID: {Thread.CurrentThread.ManagedThreadId}");

                var charBuffer = new char[currentWordLength];
                var indexArray = new long[currentWordLength];


                var position = currentWordLength - 1;
                //var possibilites = (long)Math.Pow(charset.Length, currentWordLength);
                var possibilites = wordsPerThread[minWordLength - 1 + currentWordLength - 1];

                for (var j = 0; j < indexArray.Length; j++) indexArray[j] = 0;

                if (threadNumber > 0)
                    SkipIndexArray(indexArray, threadNumber, charset.Length, currentWordLength, possibilites);

                for (var n = 0; n < possibilites; n++)
                {
                    for (var i = 0; i < indexArray.Length; i++)
                    {
                        var index = indexArray[i];
                        charBuffer[i] = charset[index];
                    }

                    BruteforceManager.Encoding.GetBytes(charBuffer, 0, charBuffer.Length, currentHashBuffer, 0);
                    if (CheckItem(currentHashBuffer, _input, 0, charBuffer.Length, Hash))
                    {
                        _endDateTime = DateTime.Now;
                        _active = false;
                        BruteforceManager.EndTime = DateTime.Now;
                        Result = new string(charBuffer);
                        BruteforceManager.FirstTask = this;
                        HasResult = true;
                        BruteforceManager.Cancel = true;
                        Console.WriteLine($"Hash with Result '{Result}' found on Task Number: {TaskNumber} ThreadID: {Thread.CurrentThread.ManagedThreadId}");
                        return;
                    }

                    //Counter++;
                    Interlocked.Increment(ref Counter);

                    var update = true;
                    for (var i = position; i >= 0 && update; i--)
                    {
                        update = false;
                        indexArray[i]++;
                        if (indexArray[i] >= charset.Length)
                        {
                            indexArray[i] = 0;
                            update = true;
                        }
                    }

                    if (BruteforceManager.Cancel)
                        return;
                }
            }

            //Counter = 0;
            Interlocked.Exchange(ref Counter, 0);
        }

        private static bool CheckItem(byte[] charBuffer, byte[] input, int offset, int count, IBruteforceHash hash)
        {
            return HashHelper.ArraysEqual(hash.Hash(charBuffer, offset, count), input);
        }

        // ------------------------------------------------------------

        public void Update()
        {
            Speed = (double) (Counter - _lastCounter);
            _lastCounter = Counter;
        }
    }
}