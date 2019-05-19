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
            TaskCount = taskCount;
            Charset = charset;
            Stop();
            Tasks.Clear();
            HasResult = false;

            IsActive = true;
            Cancel = false;
            
            for (var i = 0; i < TaskCount; i++)
            {
                var task = new BruteforceTask(this,
                    hash,
                    i,
                    charset.ToCharArray(),
                    HashHelper.ToBuffer(hashValue),
                    1,
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
            IsActive = false;
        }

        public void Update()
        {
            if (IsActive)
            {
                Speed = 0;
                for (var i = 0; i < Tasks.Count; i++)
                {
                    Tasks[i].Update();
                    Speed += Tasks[i].Speed;
                    if (Tasks[i].HasResult)
                    {
                        HasResult = true;
                        Result = Tasks[i].Result;
                    }
                }
            }
        }
    }
}