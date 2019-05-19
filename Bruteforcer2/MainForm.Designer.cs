namespace Bruteforcer2
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupCracker = new System.Windows.Forms.GroupBox();
            this.btnAlgo = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.numThreads = new System.Windows.Forms.NumericUpDown();
            this.lblMaxTime = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblCharspaceValue = new System.Windows.Forms.Label();
            this.lblCharSpace = new System.Windows.Forms.Label();
            this.lbCount = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbCharset = new System.Windows.Forms.ComboBox();
            this.btStop = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbHash = new System.Windows.Forms.Label();
            this.btStart = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lbAvarage = new System.Windows.Forms.Label();
            this.tbInput = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxHash = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbSpeed = new System.Windows.Forms.Label();
            this.numMinCount = new System.Windows.Forms.NumericUpDown();
            this.numCount = new System.Windows.Forms.NumericUpDown();
            this.lbTime = new System.Windows.Forms.Label();
            this.lbDauer = new System.Windows.Forms.Label();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.groupHasher = new System.Windows.Forms.GroupBox();
            this.tbHashInput = new System.Windows.Forms.TextBox();
            this.comboBoxHashMethods = new System.Windows.Forms.ComboBox();
            this.tbHashOutput = new System.Windows.Forms.TextBox();
            this.btHash = new System.Windows.Forms.Button();
            this.btHashByteFormat = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.groupCracker.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numThreads)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCount)).BeginInit();
            this.groupHasher.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupCracker
            // 
            this.groupCracker.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupCracker.Controls.Add(this.btnAlgo);
            this.groupCracker.Controls.Add(this.label8);
            this.groupCracker.Controls.Add(this.numThreads);
            this.groupCracker.Controls.Add(this.lblMaxTime);
            this.groupCracker.Controls.Add(this.label9);
            this.groupCracker.Controls.Add(this.lblCharspaceValue);
            this.groupCracker.Controls.Add(this.lblCharSpace);
            this.groupCracker.Controls.Add(this.lbCount);
            this.groupCracker.Controls.Add(this.label7);
            this.groupCracker.Controls.Add(this.tbCharset);
            this.groupCracker.Controls.Add(this.btStop);
            this.groupCracker.Controls.Add(this.label10);
            this.groupCracker.Controls.Add(this.label6);
            this.groupCracker.Controls.Add(this.lbHash);
            this.groupCracker.Controls.Add(this.btStart);
            this.groupCracker.Controls.Add(this.label2);
            this.groupCracker.Controls.Add(this.lbAvarage);
            this.groupCracker.Controls.Add(this.tbInput);
            this.groupCracker.Controls.Add(this.label5);
            this.groupCracker.Controls.Add(this.comboBoxHash);
            this.groupCracker.Controls.Add(this.label4);
            this.groupCracker.Controls.Add(this.label1);
            this.groupCracker.Controls.Add(this.label3);
            this.groupCracker.Controls.Add(this.lbSpeed);
            this.groupCracker.Controls.Add(this.numMinCount);
            this.groupCracker.Controls.Add(this.numCount);
            this.groupCracker.Controls.Add(this.lbTime);
            this.groupCracker.Controls.Add(this.lbDauer);
            this.groupCracker.Controls.Add(this.tbResult);
            this.groupCracker.Location = new System.Drawing.Point(12, 12);
            this.groupCracker.Name = "groupCracker";
            this.groupCracker.Size = new System.Drawing.Size(600, 260);
            this.groupCracker.TabIndex = 27;
            this.groupCracker.TabStop = false;
            this.groupCracker.Text = "Cracker";
            // 
            // btnAlgo
            // 
            this.btnAlgo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAlgo.Enabled = false;
            this.btnAlgo.Location = new System.Drawing.Point(168, 231);
            this.btnAlgo.Name = "btnAlgo";
            this.btnAlgo.Size = new System.Drawing.Size(75, 23);
            this.btnAlgo.TabIndex = 34;
            this.btnAlgo.Text = "Algorithm";
            this.btnAlgo.UseVisualStyleBackColor = true;
            this.btnAlgo.Visible = false;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(360, 174);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 13);
            this.label8.TabIndex = 33;
            this.label8.Text = "Threads";
            // 
            // numThreads
            // 
            this.numThreads.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numThreads.Location = new System.Drawing.Point(459, 172);
            this.numThreads.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.numThreads.Name = "numThreads";
            this.numThreads.Size = new System.Drawing.Size(135, 20);
            this.numThreads.TabIndex = 32;
            this.numThreads.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numThreads.ValueChanged += new System.EventHandler(this.numThreads_ValueChanged);
            // 
            // lblMaxTime
            // 
            this.lblMaxTime.AutoSize = true;
            this.lblMaxTime.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxTime.Location = new System.Drawing.Point(109, 175);
            this.lblMaxTime.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblMaxTime.Name = "lblMaxTime";
            this.lblMaxTime.Size = new System.Drawing.Size(15, 13);
            this.lblMaxTime.TabIndex = 31;
            this.lblMaxTime.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 176);
            this.label9.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 13);
            this.label9.TabIndex = 30;
            this.label9.Text = "Max. Duration:";
            // 
            // lblCharspaceValue
            // 
            this.lblCharspaceValue.AutoSize = true;
            this.lblCharspaceValue.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCharspaceValue.Location = new System.Drawing.Point(109, 143);
            this.lblCharspaceValue.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblCharspaceValue.Name = "lblCharspaceValue";
            this.lblCharspaceValue.Size = new System.Drawing.Size(15, 13);
            this.lblCharspaceValue.TabIndex = 29;
            this.lblCharspaceValue.Text = "0";
            // 
            // lblCharSpace
            // 
            this.lblCharSpace.AutoSize = true;
            this.lblCharSpace.Location = new System.Drawing.Point(6, 144);
            this.lblCharSpace.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblCharSpace.Name = "lblCharSpace";
            this.lblCharSpace.Size = new System.Drawing.Size(61, 13);
            this.lblCharSpace.TabIndex = 28;
            this.lblCharSpace.Text = "Possibilites:";
            // 
            // lbCount
            // 
            this.lbCount.AutoSize = true;
            this.lbCount.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCount.Location = new System.Drawing.Point(109, 128);
            this.lbCount.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbCount.Name = "lbCount";
            this.lbCount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbCount.Size = new System.Drawing.Size(15, 13);
            this.lbCount.TabIndex = 27;
            this.lbCount.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 128);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "Counter:";
            // 
            // tbCharset
            // 
            this.tbCharset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCharset.FormattingEnabled = true;
            this.tbCharset.Items.AddRange(new object[] {
            "abcdefghijklmnopqrstuvwxyz",
            "abcdefghijklmnopqrstuvwxyzABCDEFGHJIKLMNOPQRSTUVWXYZ",
            "ABCDEFGHJIKLMNOPQRSTUVWXYZ",
            "0123456789",
            "abcdefghijklmnopqrstuvwxyz0123456789",
            "abcdefghijklmnopqrstuvwxyzABCDEFGHJIKLMNOPQRSTUVWXYZ0123456789",
            "ABCDEFGHJIKLMNOPQRSTUVWXYZ0123456789"});
            this.tbCharset.Location = new System.Drawing.Point(90, 12);
            this.tbCharset.Name = "tbCharset";
            this.tbCharset.Size = new System.Drawing.Size(503, 21);
            this.tbCharset.TabIndex = 25;
            // 
            // btStop
            // 
            this.btStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btStop.Location = new System.Drawing.Point(87, 231);
            this.btStop.Name = "btStop";
            this.btStop.Size = new System.Drawing.Size(75, 23);
            this.btStop.TabIndex = 24;
            this.btStop.Text = "Stop";
            this.btStop.UseVisualStyleBackColor = true;
            this.btStop.Click += new System.EventHandler(this.btStop_Click);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(360, 148);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(92, 13);
            this.label10.TabIndex = 23;
            this.label10.Text = "Max Word Length";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(360, 122);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Min Word Length";
            // 
            // lbHash
            // 
            this.lbHash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbHash.AutoSize = true;
            this.lbHash.Location = new System.Drawing.Point(360, 94);
            this.lbHash.Name = "lbHash";
            this.lbHash.Size = new System.Drawing.Size(50, 13);
            this.lbHash.TabIndex = 22;
            this.lbHash.Text = "Algorithm";
            // 
            // btStart
            // 
            this.btStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btStart.Location = new System.Drawing.Point(6, 231);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(75, 23);
            this.btStart.TabIndex = 0;
            this.btStart.Text = "Crack";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Charset";
            // 
            // lbAvarage
            // 
            this.lbAvarage.AutoSize = true;
            this.lbAvarage.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAvarage.Location = new System.Drawing.Point(109, 112);
            this.lbAvarage.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbAvarage.Name = "lbAvarage";
            this.lbAvarage.Size = new System.Drawing.Size(15, 13);
            this.lbAvarage.TabIndex = 21;
            this.lbAvarage.Text = "0";
            // 
            // tbInput
            // 
            this.tbInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInput.Location = new System.Drawing.Point(90, 39);
            this.tbInput.Name = "tbInput";
            this.tbInput.Size = new System.Drawing.Size(504, 20);
            this.tbInput.TabIndex = 1;
            this.tbInput.Text = "b4dc23d1ee880f8338380763955b5983";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 112);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Average:";
            // 
            // comboBoxHash
            // 
            this.comboBoxHash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxHash.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxHash.FormattingEnabled = true;
            this.comboBoxHash.Location = new System.Drawing.Point(459, 91);
            this.comboBoxHash.Name = "comboBoxHash";
            this.comboBoxHash.Size = new System.Drawing.Size(135, 21);
            this.comboBoxHash.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Output";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 96);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Speed:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Input";
            // 
            // lbSpeed
            // 
            this.lbSpeed.AutoSize = true;
            this.lbSpeed.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSpeed.Location = new System.Drawing.Point(109, 96);
            this.lbSpeed.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbSpeed.Name = "lbSpeed";
            this.lbSpeed.Size = new System.Drawing.Size(15, 13);
            this.lbSpeed.TabIndex = 4;
            this.lbSpeed.Text = "0";
            // 
            // numMinCount
            // 
            this.numMinCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numMinCount.Location = new System.Drawing.Point(459, 120);
            this.numMinCount.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numMinCount.Name = "numMinCount";
            this.numMinCount.Size = new System.Drawing.Size(135, 20);
            this.numMinCount.TabIndex = 13;
            this.numMinCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMinCount.ValueChanged += new System.EventHandler(this.numCount_ValueChanged);
            // 
            // numCount
            // 
            this.numCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numCount.Location = new System.Drawing.Point(459, 146);
            this.numCount.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numCount.Name = "numCount";
            this.numCount.Size = new System.Drawing.Size(135, 20);
            this.numCount.TabIndex = 13;
            this.numCount.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numCount.ValueChanged += new System.EventHandler(this.numCount_ValueChanged);
            // 
            // lbTime
            // 
            this.lbTime.AutoSize = true;
            this.lbTime.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTime.Location = new System.Drawing.Point(109, 159);
            this.lbTime.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(15, 13);
            this.lbTime.TabIndex = 15;
            this.lbTime.Text = "0";
            // 
            // lbDauer
            // 
            this.lbDauer.AutoSize = true;
            this.lbDauer.Location = new System.Drawing.Point(6, 160);
            this.lbDauer.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lbDauer.Name = "lbDauer";
            this.lbDauer.Size = new System.Drawing.Size(50, 13);
            this.lbDauer.TabIndex = 14;
            this.lbDauer.Text = "Duration:";
            // 
            // tbResult
            // 
            this.tbResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbResult.Location = new System.Drawing.Point(90, 65);
            this.tbResult.Name = "tbResult";
            this.tbResult.Size = new System.Drawing.Size(504, 20);
            this.tbResult.TabIndex = 6;
            // 
            // groupHasher
            // 
            this.groupHasher.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupHasher.Controls.Add(this.tbHashInput);
            this.groupHasher.Controls.Add(this.comboBoxHashMethods);
            this.groupHasher.Controls.Add(this.tbHashOutput);
            this.groupHasher.Controls.Add(this.btHash);
            this.groupHasher.Controls.Add(this.btHashByteFormat);
            this.groupHasher.Location = new System.Drawing.Point(12, 278);
            this.groupHasher.Name = "groupHasher";
            this.groupHasher.Size = new System.Drawing.Size(600, 72);
            this.groupHasher.TabIndex = 26;
            this.groupHasher.TabStop = false;
            this.groupHasher.Text = "Hasher";
            // 
            // tbHashInput
            // 
            this.tbHashInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbHashInput.Location = new System.Drawing.Point(87, 19);
            this.tbHashInput.Name = "tbHashInput";
            this.tbHashInput.Size = new System.Drawing.Size(366, 20);
            this.tbHashInput.TabIndex = 7;
            // 
            // comboBoxHashMethods
            // 
            this.comboBoxHashMethods.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxHashMethods.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxHashMethods.FormattingEnabled = true;
            this.comboBoxHashMethods.Location = new System.Drawing.Point(459, 20);
            this.comboBoxHashMethods.Name = "comboBoxHashMethods";
            this.comboBoxHashMethods.Size = new System.Drawing.Size(135, 21);
            this.comboBoxHashMethods.TabIndex = 8;
            // 
            // tbHashOutput
            // 
            this.tbHashOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbHashOutput.Location = new System.Drawing.Point(87, 47);
            this.tbHashOutput.Name = "tbHashOutput";
            this.tbHashOutput.Size = new System.Drawing.Size(366, 20);
            this.tbHashOutput.TabIndex = 9;
            // 
            // btHash
            // 
            this.btHash.Location = new System.Drawing.Point(6, 19);
            this.btHash.Name = "btHash";
            this.btHash.Size = new System.Drawing.Size(75, 23);
            this.btHash.TabIndex = 10;
            this.btHash.Text = "Hash";
            this.btHash.UseVisualStyleBackColor = true;
            this.btHash.Click += new System.EventHandler(this.btHash_Click);
            // 
            // btHashByteFormat
            // 
            this.btHashByteFormat.Location = new System.Drawing.Point(6, 45);
            this.btHashByteFormat.Name = "btHashByteFormat";
            this.btHashByteFormat.Size = new System.Drawing.Size(75, 23);
            this.btHashByteFormat.TabIndex = 12;
            this.btHashByteFormat.Text = "Hash Bytes";
            this.btHashByteFormat.UseVisualStyleBackColor = true;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 362);
            this.Controls.Add(this.groupCracker);
            this.Controls.Add(this.groupHasher);
            this.Name = "MainForm";
            this.Text = "Bruteforcer 2";
            this.groupCracker.ResumeLayout(false);
            this.groupCracker.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numThreads)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCount)).EndInit();
            this.groupHasher.ResumeLayout(false);
            this.groupHasher.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupCracker;
        private System.Windows.Forms.Label lblMaxTime;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblCharspaceValue;
        private System.Windows.Forms.Label lblCharSpace;
        private System.Windows.Forms.Label lbCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox tbCharset;
        private System.Windows.Forms.Button btStop;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbHash;
        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbAvarage;
        private System.Windows.Forms.TextBox tbInput;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxHash;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbSpeed;
        private System.Windows.Forms.NumericUpDown numCount;
        private System.Windows.Forms.Label lbTime;
        private System.Windows.Forms.Label lbDauer;
        private System.Windows.Forms.TextBox tbResult;
        private System.Windows.Forms.GroupBox groupHasher;
        private System.Windows.Forms.TextBox tbHashInput;
        private System.Windows.Forms.ComboBox comboBoxHashMethods;
        private System.Windows.Forms.TextBox tbHashOutput;
        private System.Windows.Forms.Button btHash;
        private System.Windows.Forms.Button btHashByteFormat;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numThreads;
        private System.Windows.Forms.Button btnAlgo;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numMinCount;
    }
}

