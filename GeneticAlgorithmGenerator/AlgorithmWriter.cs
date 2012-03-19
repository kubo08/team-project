using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace master_multithread
{
    class AlgorithmWriter
    {
        public int numgen;
        public int lpop;
        public int numpop;
        public int lret;
        public int spaceMax;
        public int spaceMin;
        public string fitnessName;
        public string fitnessParam;
        public int typeMigration;
        public int periodMigration;
        public int numMigration;
        public bool doneBool;
        public string errorMessage;

        public void GenerateInitialization()
        {
            try 
            {
                doneBool = false;
                string line;
                int lineNumber = 0;

                using (StreamReader sr = new StreamReader("Templates/initialization.m"))
                {
                    using (StreamWriter sw = new StreamWriter("Generated/master_initialization.m"))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            lineNumber++;
                            switch (lineNumber)
                            {
                                case 6:
                                    line = "numgen = " + numgen;
                                    break;
                                case 8:
                                    line = "lpop = " + lpop;
                                    break;
                                case 10:
                                    line = "numpop = " + numpop;
                                    break;
                                case 12:
                                    line = "lret = " + lret;
                                    break;
                                case 14:
                                    line = "spacemin = " + spaceMin;
                                    break;
                                case 16:
                                    line = "spacemax = " + spaceMax;
                                    break;
                                case 20:
                                    line = "fitnesname = " + fitnessName;
                                    break;
                                case 21:
                                    line = "fitnesparam = " + fitnessParam;
                                    break;
                                case 24:
                                    line = "typemigration = " + typeMigration;
                                    break;
                                case 25:
                                    line = "periodmigration = " + periodMigration;
                                    break;
                                case 26:
                                    line = "nummigration = " + numMigration;
                                    break;
                            }

                            sw.WriteLine(line);
                        }

                        doneBool = true;
                    }
                }
            }
            catch (Exception ex)
            {
                doneBool = false;
                errorMessage = ex.Message;
            }
        }

        public void GenerateSlaveAlgorithm()
        {
            try
            {
                doneBool = false;
                string line;

                using (StreamReader sr = new StreamReader("Templates/slave_ga.m"))
                {
                    using (StreamWriter sw = new StreamWriter("Generated/slave_ga.m"))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            sw.WriteLine(line);
                        }

                        doneBool = true;
                    }
                }
            }
            catch (Exception ex)
            {
                doneBool = false;
                errorMessage = ex.Message;
            }
        }

        public void GenerateFinishing()
        {
            try
            {
                doneBool = false;
                string line;

                using (StreamReader sr = new StreamReader("Templates/finishing.m"))
                {
                    using (StreamWriter sw = new StreamWriter("Generated/master_finishing.m"))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            sw.WriteLine(line);
                        }

                        doneBool = true;
                    }
                }
            }
            catch (Exception ex)
            {
                doneBool = false;
                errorMessage = ex.Message;
            }
        }
    }
}
