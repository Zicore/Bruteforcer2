using System;
using System.Collections.Generic;
using System.Text;
using Bruteforcer2.Hash;

namespace Bruteforcer2.Bruteforce
{
    public class BruteforceManager
    {
        public static readonly Encoding Encoding = Encoding.ASCII;

        private volatile bool _cancel;
        private volatile bool _isActive;
        private readonly object _resultLock = new object();
        public List<BruteforceTask> Tasks { get; set; } = new List<BruteforceTask>();

        public bool IsActive
        {
            get => _isActive;
            set => _isActive = value;
        }

        public bool Cancel
        {
            get => _cancel;
            set => _cancel = value;
        }

        public double Speed { get; set; }
        public bool HasResult { get; set; }
        public string Result { get; set; } = "";
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Charset { get; set; } = "";
        public BruteforceTask FirstTask { get; set; }
        public int TaskCount { get; set; }

        public void Start(IBruteforceHash hash, string charset, int minWordLength, int maxWordLength, int taskCount, string hashValue)
        {
            if (hash == null)
                throw new ArgumentException("Bitte einen Hash-Algorithmus auswählen.");
            if (string.IsNullOrEmpty(charset))
                throw new ArgumentException("Der Zeichensatz darf nicht leer sein.");
            foreach (var character in charset)
                if (character > 127)
                    throw new ArgumentException("Der Zeichensatz darf nur ASCII-Zeichen enthalten.");
            if (minWordLength < 1 || maxWordLength < minWordLength || taskCount < 1)
                throw new ArgumentException("Wortlängen und Threadanzahl müssen positiv sein; Maximum muss mindestens Minimum sein.");

            // Exact integer arithmetic, with a bound that also keeps aggregate counters safe.
            long words = 1;
            long searchSpace = 0;
            checked
            {
                for (var length = 1; length <= maxWordLength; length++)
                {
                    words *= charset.Length;
                    if (length >= minWordLength)
                        searchSpace += words;
                }
            }
            var input = HashHelper.ToBuffer(hashValue);
            var characters = charset.ToCharArray();
            Stop();
            TaskCount = taskCount;
            Charset = charset;
            Tasks.Clear();
            HasResult = false;
            Result = "";
            Speed = 0;
            EndTime = default;

            IsActive = true;
            Cancel = false;
            
            for (var i = 0; i < TaskCount; i++)
            {
                var task = new BruteforceTask(this,
                    hash,
                    i,
                    characters,
                    input,
                    minWordLength,
                    maxWordLength
                );
                Tasks.Add(task);
            }

            FirstTask = Tasks[0];

            StartTime = DateTime.Now;
            foreach (var t in Tasks)
            {
                t.Start();
            }
        }

        public void Stop()
        {
            Cancel = true;
            foreach (var task in Tasks)
                task.WaitForCompletion();
            IsActive = false;
        }

        internal void ReportResult(BruteforceTask task)
        {
            lock (_resultLock)
            {
                if (Cancel)
                    return;
                FirstTask = task;
                Result = task.Result;
                EndTime = DateTime.Now;
                HasResult = true;
                Cancel = true;
            }
        }

        public void Update()
        {
            lock (_resultLock)
            {
                if (IsActive)
                {
                    Speed = 0;
                    for (var i = 0; i < Tasks.Count; i++)
                    {
                        Tasks[i].Update();
                        Speed += Tasks[i].Speed;
                    }
                    if (Tasks.TrueForAll(task => !task.IsActive))
                        IsActive = false;
                }
            }
        }
    }
}
