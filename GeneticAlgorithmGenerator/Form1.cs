using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using helpers;

namespace master_multithread
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //TODO: ControlEvents

        private void ResetGroupBox()
        {
            progressBar1.Value = 0;
            PictureBoxFinishing.ImageLocation = "Resources/error.png";
            PictureBoxGenerating.ImageLocation = "Resources/error.png";
            PictureBoxInitializing.ImageLocation = "Resources/error.png";
            PictureBoxSlave.ImageLocation = "Resources/error.png";
            groupBox1.Enabled = false;
        }

        private bool CheckValidation()
        {
            bool isOk = true;

            if (TextBoxFitnessParam.Text.Trim() == null)
            {
                isOk = false;
            }

            if (TextBoxLpop.Text.Trim() == null)
            {
                isOk = false;
            }

            if (TextBoxLret.Text.Trim() == null)
            {
                isOk = false;
            }

            if (TextBoxNumgen.Text.Trim() == null)
            {
                isOk = false;
            }

            if (TextBoxNumMigration.Text.Trim() == null)
            {
                isOk = false;
            }

            if (TextBoxPeriodMigration.Text.Trim() == null)
            {
                isOk = false;
            }

            if (TextBoxSpaceMax.Text.Trim() == null)
            {
                isOk = false;
            }

            if (TextBoxSpaceMin.Text.Trim() == null)
            {
                isOk = false;
            }

            if (comboBox1.SelectedItem == null)
            {
                isOk = false;
            }

            return isOk;
        }

        #region ControlEvents

        private void button1_Click(object sender, EventArgs e)
        {
            SlaveConfiguration slave_config = new SlaveConfiguration();
            slave_config.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ResetGroupBox();

            if (CheckValidation())
            {
                groupBox1.Enabled = true;
                AlgorithmGeneration();
                InitializeMaster();
                
                //slave algorithm (zatial len jeden
                //TODO: rozdelit na viac podla toho kolko slave-ov vyberieme
                //TODO: tie data!!
                string data="aa";
                foreach (Slave slave in helpers.sender.slaves)
                {
                    helpers.sender.Send(slave.ip, data);
                }
                FinishAlgorithm();
            }
            else
            {
                MessageBox.Show("Skontrolujte dáta!", "Informácia!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region AlgorithmGeneration

        private void AlgorithmGeneration()
        {
            AlgorithmWriter writer = new AlgorithmWriter();
            ConfigureWriter(writer);

            writer.GenerateInitialization();

            if (writer.doneBool)
            {
                progressBar1.PerformStep();
                writer.GenerateSlaveAlgorithm();

                if (writer.doneBool)
                {
                    progressBar1.PerformStep();
                    writer.GenerateFinishing();

                    if (writer.doneBool)
                    {
                        progressBar1.PerformStep();
                        PictureBoxGenerating.ImageLocation = "Resources/success.png";
                    }
                    else
                    {
                        MessageBox.Show(writer.errorMessage, "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    groupBox1.Enabled = false;
                    MessageBox.Show(writer.errorMessage, "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                groupBox1.Enabled = false;
                MessageBox.Show(writer.errorMessage, "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureWriter(AlgorithmWriter writer)
        {
            writer.numgen = int.Parse(TextBoxNumgen.Text);
            writer.lpop = int.Parse(TextBoxLpop.Text);
            writer.numpop = int.Parse(TextBoxNumpop.Text);
            writer.lret = int.Parse(TextBoxLret.Text);
            writer.spaceMin = int.Parse(TextBoxSpaceMin.Text);
            writer.spaceMax = int.Parse(TextBoxSpaceMax.Text);
            writer.fitnessName = comboBox1.SelectedItem.ToString();
            writer.fitnessParam = TextBoxFitnessParam.Text;
            writer.typeMigration = int.Parse(TextBoxTypeMigration.Text);
            writer.periodMigration = int.Parse(TextBoxPeriodMigration.Text);
            writer.numMigration = int.Parse(TextBoxNumMigration.Text);
        }

        #endregion

        #region MasterCommands

        private void FinishAlgorithm()
        {
            progressBar1.Value = 0;

            string commandFile = "Generated/master_finishing.m";
            string command = "";
            command = GetCommands(command, commandFile);

            string[] lines = command.Split('\r');

            progressBar1.Maximum = lines.Count() + 1;
            progressBar1.PerformStep();

            ExecuteMatlabCommand(lines);
            PictureBoxFinishing.ImageLocation = "Resources/success.png";
        }

        private void InitializeMaster()
        {
            progressBar1.Value = 0;

            string commandFile = "Generated/master_initialization.m";
            string command = "";
            command = GetCommands(command, commandFile);

            string[] lines = command.Split('\r');

            progressBar1.Maximum = lines.Count() + 1;
            progressBar1.PerformStep();

            ExecuteMatlabCommand(lines);
            PictureBoxInitializing.ImageLocation = "Resources/success.png";
        }

        private void ExecuteMatlabCommand(string[] lines)
        {
            MLApp.MLApp matlab = new MLApp.MLApp();

            for (int i = 0; i < lines.Count(); i++)
            {
                if (lines[i].StartsWith("\n"))
                {
                    lines[i] = lines[i].Remove(0, 1);
                }

                try
                {
                    matlab.Execute(lines[i]);
                    progressBar1.PerformStep();
                }
                catch (Exception ex)
                {
                    groupBox1.Enabled = false;
                    MessageBox.Show(ex.Message, "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private static string GetCommands(string command, string commandFile)
        {
            string line;

            using (StreamReader sr = new StreamReader(commandFile))
            {
                try
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        command += line;
                        command += "\r";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return command;
        }

        #endregion

    }
}
