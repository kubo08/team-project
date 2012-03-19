namespace master_multithread
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.TextBoxNumgen = new System.Windows.Forms.TextBox();
            this.TextBoxLpop = new System.Windows.Forms.TextBox();
            this.TextBoxNumpop = new System.Windows.Forms.TextBox();
            this.TextBoxLret = new System.Windows.Forms.TextBox();
            this.TextBoxSpaceMin = new System.Windows.Forms.TextBox();
            this.TextBoxSpaceMax = new System.Windows.Forms.TextBox();
            this.TextBoxFitnessParam = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.TextBoxTypeMigration = new System.Windows.Forms.TextBox();
            this.TextBoxNumMigration = new System.Windows.Forms.TextBox();
            this.TextBoxPeriodMigration = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.PictureBoxFinishing = new System.Windows.Forms.PictureBox();
            this.PictureBoxSlave = new System.Windows.Forms.PictureBox();
            this.PictureBoxInitializing = new System.Windows.Forms.PictureBox();
            this.PictureBoxGenerating = new System.Windows.Forms.PictureBox();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxFinishing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxSlave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxInitializing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxGenerating)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(121, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Počet generácií:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Počet reťazcov v každej subpopulácii:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(107, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Počet subpopulácii:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(78, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Počet jedincov v reťazci:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(179, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Rozsah hľadania riešenia:          min:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(95, 140);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Meno fitness funkcie:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(74, 167);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(129, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Parametre fitness funkcie:";
            // 
            // TextBoxNumgen
            // 
            this.TextBoxNumgen.Location = new System.Drawing.Point(213, 6);
            this.TextBoxNumgen.Name = "TextBoxNumgen";
            this.TextBoxNumgen.Size = new System.Drawing.Size(100, 20);
            this.TextBoxNumgen.TabIndex = 7;
            // 
            // TextBoxLpop
            // 
            this.TextBoxLpop.Location = new System.Drawing.Point(213, 32);
            this.TextBoxLpop.Name = "TextBoxLpop";
            this.TextBoxLpop.Size = new System.Drawing.Size(100, 20);
            this.TextBoxLpop.TabIndex = 8;
            // 
            // TextBoxNumpop
            // 
            this.TextBoxNumpop.Location = new System.Drawing.Point(213, 58);
            this.TextBoxNumpop.Name = "TextBoxNumpop";
            this.TextBoxNumpop.Size = new System.Drawing.Size(100, 20);
            this.TextBoxNumpop.TabIndex = 9;
            // 
            // TextBoxLret
            // 
            this.TextBoxLret.Location = new System.Drawing.Point(213, 84);
            this.TextBoxLret.Name = "TextBoxLret";
            this.TextBoxLret.Size = new System.Drawing.Size(100, 20);
            this.TextBoxLret.TabIndex = 10;
            // 
            // TextBoxSpaceMin
            // 
            this.TextBoxSpaceMin.Location = new System.Drawing.Point(213, 110);
            this.TextBoxSpaceMin.Name = "TextBoxSpaceMin";
            this.TextBoxSpaceMin.Size = new System.Drawing.Size(100, 20);
            this.TextBoxSpaceMin.TabIndex = 11;
            // 
            // TextBoxSpaceMax
            // 
            this.TextBoxSpaceMax.Location = new System.Drawing.Point(366, 110);
            this.TextBoxSpaceMax.Name = "TextBoxSpaceMax";
            this.TextBoxSpaceMax.Size = new System.Drawing.Size(100, 20);
            this.TextBoxSpaceMax.TabIndex = 12;
            // 
            // TextBoxFitnessParam
            // 
            this.TextBoxFitnessParam.Location = new System.Drawing.Point(213, 164);
            this.TextBoxFitnessParam.Name = "TextBoxFitnessParam";
            this.TextBoxFitnessParam.Size = new System.Drawing.Size(100, 20);
            this.TextBoxFitnessParam.TabIndex = 13;
            this.TextBoxFitnessParam.Text = "[]";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "schwef"});
            this.comboBox1.Location = new System.Drawing.Point(213, 137);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(100, 21);
            this.comboBox1.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(331, 113);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "max:";
            // 
            // TextBoxTypeMigration
            // 
            this.TextBoxTypeMigration.Location = new System.Drawing.Point(213, 190);
            this.TextBoxTypeMigration.Name = "TextBoxTypeMigration";
            this.TextBoxTypeMigration.Size = new System.Drawing.Size(100, 20);
            this.TextBoxTypeMigration.TabIndex = 16;
            // 
            // TextBoxNumMigration
            // 
            this.TextBoxNumMigration.Location = new System.Drawing.Point(213, 216);
            this.TextBoxNumMigration.Name = "TextBoxNumMigration";
            this.TextBoxNumMigration.Size = new System.Drawing.Size(100, 20);
            this.TextBoxNumMigration.TabIndex = 17;
            // 
            // TextBoxPeriodMigration
            // 
            this.TextBoxPeriodMigration.Location = new System.Drawing.Point(213, 242);
            this.TextBoxPeriodMigration.Name = "TextBoxPeriodMigration";
            this.TextBoxPeriodMigration.Size = new System.Drawing.Size(100, 20);
            this.TextBoxPeriodMigration.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(133, 193);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Typ migrácie:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(102, 219);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Početnosť migrácie:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(115, 245);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(88, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "Perióda migrácie:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(213, 268);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 22;
            this.button1.Text = "Slave";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(213, 446);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 23);
            this.button2.TabIndex = 23;
            this.button2.Text = "Generovať";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(6, 19);
            this.progressBar1.Maximum = 3;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(473, 23);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 24;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.PictureBoxGenerating);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.PictureBoxFinishing);
            this.groupBox1.Controls.Add(this.PictureBoxSlave);
            this.groupBox1.Controls.Add(this.PictureBoxInitializing);
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(12, 298);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(485, 142);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Procesy";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(27, 117);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(118, 13);
            this.label14.TabIndex = 30;
            this.label14.Text = "Spracovanie výsledkov";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(27, 95);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(78, 13);
            this.label13.TabIndex = 29;
            this.label13.Text = "Slave algoritmy";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(27, 73);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(61, 13);
            this.label12.TabIndex = 28;
            this.label12.Text = "Inicializácia";
            // 
            // PictureBoxFinishing
            // 
            this.PictureBoxFinishing.Image = global::master_multithread.Properties.Resources.error;
            this.PictureBoxFinishing.InitialImage = global::master_multithread.Properties.Resources.error;
            this.PictureBoxFinishing.Location = new System.Drawing.Point(6, 114);
            this.PictureBoxFinishing.Name = "PictureBoxFinishing";
            this.PictureBoxFinishing.Size = new System.Drawing.Size(16, 16);
            this.PictureBoxFinishing.TabIndex = 27;
            this.PictureBoxFinishing.TabStop = false;
            // 
            // PictureBoxSlave
            // 
            this.PictureBoxSlave.Image = global::master_multithread.Properties.Resources.error;
            this.PictureBoxSlave.InitialImage = global::master_multithread.Properties.Resources.error;
            this.PictureBoxSlave.Location = new System.Drawing.Point(6, 92);
            this.PictureBoxSlave.Name = "PictureBoxSlave";
            this.PictureBoxSlave.Size = new System.Drawing.Size(16, 16);
            this.PictureBoxSlave.TabIndex = 26;
            this.PictureBoxSlave.TabStop = false;
            // 
            // PictureBoxInitializing
            // 
            this.PictureBoxInitializing.Image = global::master_multithread.Properties.Resources.error;
            this.PictureBoxInitializing.InitialImage = global::master_multithread.Properties.Resources.error;
            this.PictureBoxInitializing.Location = new System.Drawing.Point(6, 70);
            this.PictureBoxInitializing.Name = "PictureBoxInitializing";
            this.PictureBoxInitializing.Size = new System.Drawing.Size(16, 16);
            this.PictureBoxInitializing.TabIndex = 25;
            this.PictureBoxInitializing.TabStop = false;
            // 
            // PictureBoxGenerating
            // 
            this.PictureBoxGenerating.Image = global::master_multithread.Properties.Resources.error;
            this.PictureBoxGenerating.InitialImage = global::master_multithread.Properties.Resources.error;
            this.PictureBoxGenerating.Location = new System.Drawing.Point(6, 48);
            this.PictureBoxGenerating.Name = "PictureBoxGenerating";
            this.PictureBoxGenerating.Size = new System.Drawing.Size(16, 16);
            this.PictureBoxGenerating.TabIndex = 31;
            this.PictureBoxGenerating.TabStop = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(27, 51);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(119, 13);
            this.label15.TabIndex = 32;
            this.label15.Text = "Generovanie algoritmov";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 478);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.TextBoxPeriodMigration);
            this.Controls.Add(this.TextBoxNumMigration);
            this.Controls.Add(this.TextBoxTypeMigration);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.TextBoxFitnessParam);
            this.Controls.Add(this.TextBoxSpaceMax);
            this.Controls.Add(this.TextBoxSpaceMin);
            this.Controls.Add(this.TextBoxLret);
            this.Controls.Add(this.TextBoxNumpop);
            this.Controls.Add(this.TextBoxLpop);
            this.Controls.Add(this.TextBoxNumgen);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "GeneticAlgorithmGenerator";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxFinishing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxSlave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxInitializing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxGenerating)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TextBoxNumgen;
        private System.Windows.Forms.TextBox TextBoxLpop;
        private System.Windows.Forms.TextBox TextBoxNumpop;
        private System.Windows.Forms.TextBox TextBoxLret;
        private System.Windows.Forms.TextBox TextBoxSpaceMin;
        private System.Windows.Forms.TextBox TextBoxSpaceMax;
        private System.Windows.Forms.TextBox TextBoxFitnessParam;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TextBoxTypeMigration;
        private System.Windows.Forms.TextBox TextBoxNumMigration;
        private System.Windows.Forms.TextBox TextBoxPeriodMigration;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.PictureBox PictureBoxFinishing;
        private System.Windows.Forms.PictureBox PictureBoxSlave;
        private System.Windows.Forms.PictureBox PictureBoxInitializing;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.PictureBox PictureBoxGenerating;
    }
}

