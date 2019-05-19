using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bruteforcer2.Bruteforce;
using Bruteforcer2.Hash;
using Bruteforcer2.Model;

namespace Bruteforcer2
{
    public partial class MainForm : Form
    {
        private readonly List<double> _speedAverage = new List<double>();

        //private long hundredsOfSeconds;

        //private CancellationTokenSource token;
        //private long wordCounter;

        public MainForm()
        {
            InitializeComponent();
            InitHashes();
            tbCharset.SelectedIndex = 0;
        }

        public BruteforceManager Bruteforce { get; set; } = new BruteforceManager();
        public ObservableCollection<IBruteforceHash> Hashes { get; set; } = HashRepository.GetHashes();

        private void InitHashes()
        {
            comboBoxHash.DataSource = Hashes;
            comboBoxHashMethods.DataSource = Hashes;

            //comboBoxHash.SelectedIndex = 1;
            //comboBoxHashMethods.SelectedIndex = 1;
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            _speedAverage.Clear();
            var hash = comboBoxHash.SelectedItem as IBruteforceHash;
            Bruteforce.Start(hash, tbCharset.Text, (int)numMinCount.Value, (int)numCount.Value, (int)numThreads.Value, tbInput.Text);

            var space = (ulong)StringHelper.CalculateSpace(Bruteforce.Charset, (int)numMinCount.Value, (int)numCount.Value);
            lblCharspaceValue.Text = space.ToString("0,0");
        }

        private void btStop_Click(object sender, EventArgs e)
        {
            Bruteforce.Stop();
            //token?.Cancel();
            timer2.Enabled = false;
        }

        private void btHash_Click(object sender, EventArgs e)
        {
            var hash = comboBoxHashMethods.SelectedItem as IBruteforceHash;
            var input = tbHashInput.Text.ToCharArray();
            tbHashOutput.Text = HashHelper.ToHexString(hash.Hash(BruteforceManager.Encoding.GetBytes(input), 0, input.Length));
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (Bruteforce.IsActive)
            {
                Bruteforce.Update();
                if (Bruteforce.HasResult)
                {
                    lbCount.Text = Bruteforce.FirstTask.Counter.ToString("0,0");
                    tbResult.Text = Bruteforce.Result;
                    var duration = Bruteforce.EndTime - Bruteforce.StartTime;
                    lbTime.Text =
                        $"{duration.TotalHours:00}:hr {duration.Minutes:00}:min {duration.Seconds:00}:s {duration.Milliseconds:0000}:ms";
                    //if (e.HasResult)
                    //{
                    tbResult.BackColor = Color.LightGreen;
                    //}
                    //else
                    //{
                    //    tbResult.BackColor = Color.Tomato;
                    //}
                }

                if (Bruteforce.Speed > 0)
                {
                    _speedAverage.Add(Bruteforce.Speed);
                    if (_speedAverage.Count > 50)
                        _speedAverage.RemoveAt(0);

                    lbSpeed.Text = $"{Bruteforce.Speed:n} hashes/s";
                    var avg = _speedAverage.Average();
                    lbAvarage.Text = $"{avg:n} hashes/s";
                    lbCount.Text = $"{Bruteforce.Tasks.Sum(x => x.Counter):0,0}";

                    var time = DateTime.Now - Bruteforce.StartTime;
                    lbTime.Text =
                        $"{time.TotalHours:00}:hr {time.Minutes:00}:min {time.Seconds:00}:s {time.Milliseconds:0000}:ms";
                    TimeSpan maxTime;
                    try
                    {
                        var space = (ulong)StringHelper.CalculateSpace(Bruteforce.Charset, (int)numMinCount.Value,
                            (int)numCount.Value);
                        maxTime = TimeSpan.FromSeconds(space / avg);
                        lblMaxTime.Text =
                            $"{maxTime.TotalHours:00}:hr {maxTime.Minutes:00}:min {maxTime.Seconds:00}:s {maxTime.Milliseconds:0000}:ms";
                    }
                    catch (OverflowException)
                    {
                        maxTime = TimeSpan.MaxValue;
                        lblMaxTime.Text =
                            $"> {maxTime.Days:00}days: {maxTime.Hours:00}:hr {maxTime.Minutes:00}:min {maxTime.Seconds:00}:s {maxTime.Milliseconds:0000}:ms";
                    }
                }
            }
        }

        private void numThreads_ValueChanged(object sender, EventArgs e)
        {
        }

        private void numCount_ValueChanged(object sender, EventArgs e)
        {
        }

        //private void Bruteforce1(IBruteforceHash hash, string input, string charset)
        //{
        //    var hashInputBytes = HashHelper.ToBuffer(input);

        //    wordCounter = 0;
        //    hundredsOfSeconds = 0;
        //    var wordLength = 8;

        //    for (var currentWordLength = 1; currentWordLength <= wordLength; currentWordLength++)
        //    {
        //        Console.WriteLine($"Starting with word length: {currentWordLength}");

        //        var charBuffer = new char[currentWordLength];
        //        var indexArray = new int[currentWordLength];
        //        var position = currentWordLength - 1;
        //        var possibilites = Math.Pow(charset.Length, currentWordLength);

        //        for (var j = 0; j < indexArray.Length; j++) indexArray[j] = 0;

        //        for (var n = 0; n < possibilites; n++)
        //        {
        //            for (var i = 0; i < indexArray.Length; i++)
        //            {
        //                var index = indexArray[i];
        //                charBuffer[i] = charset[index];
        //            }

        //            var bytes = Encoding.Default.GetBytes(charBuffer);
        //            var hashBytes = hash.Hash(bytes, bytes.Length);

        //            if (HashHelper.ArraysEqual(hashBytes, hashInputBytes))
        //            {
        //                Console.WriteLine($"Hash found: {new string(charBuffer)}");
        //                wordCounter = 0;
        //                return;
        //            }

        //            wordCounter++;

        //            var update = true;
        //            for (var i = position; i >= 0 && update; i--)
        //            {
        //                update = false;
        //                indexArray[i]++;
        //                if (indexArray[i] >= charset.Length)
        //                {
        //                    indexArray[i] = 0;
        //                    update = true;
        //                }
        //            }

        //            if (token.IsCancellationRequested) break;
        //        }
        //    }

        //    wordCounter = 0;
        //}

        //private void btnAlgo_Click(object sender, EventArgs e)
        //{
        //    timer2.Enabled = true;
        //    var hash = comboBoxHash.SelectedItem as IBruteforceHash;
        //    var charset = tbCharset.Text;
        //    var hashInput = tbInput.Text;
        //    if (hash != null)
        //        try
        //        {
        //            token = new CancellationTokenSource();
        //            Task.Factory.StartNew(() => { Bruteforce1(hash, hashInput, charset); }, token.Token);
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"Task stopped: {ex}");
        //        }
        //}

        //private void timer2_Tick(object sender, EventArgs e)
        //{
        //    hundredsOfSeconds++;

        //    lbSpeed.Text = $"{wordCounter / hundredsOfSeconds * 10:n} hashes/s";
        //}
    }
}