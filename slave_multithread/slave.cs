using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.Runtime.InteropServices;

/***
 * TODO: 1. ak vypadne slave vyhodit zo zoznamu
 *       2. staticke pridavanie ip adries (do zoznamu a zo zoznamu sa budu posielat hello pakety)
 *       3. ftp
 *       4. otestovat Ack
 *       5. vyriesit padanie v skole
 *       6. vyhadzovat chyby (try catch)
 * **/


/**********
 * Slave UDP Listen on port 9050
 * Slave UDP Send on port 9051
 * Slave TCP Send on port 9058
 * Slave TCP Listen on port 9055
 *********/

/*************
* 1. byte cislo paketu
* 2. byte cislo paketu ktory sa potvrdzuje
* 3. byte, 1-4 bit
* 0x01 - meno            
* 0x02 - pocet cpu
* 0x04 - data na vypocet
* 3. byte, 5-8 bit dlzka mena max. 17
* 4-x - meno
* x+1 - pocet cpu
*     - velkost dat na vypocet
*     - data na vypocet
*************/ 



namespace slave_multithread
{
    public partial class slave : Form
    {

        Socket client;
        string result;
        int packetNumber = 1, recPacketNum, potvrdene=0;
        volatile List<int> Ack = new List<int>();
        
        bool OK = false;
        int hlavicka ;

        struct dataToSend
        {
            private int _AckNum;
            private IPAddress _address;
            private byte[] _data;

            public int AckNum
            {
                get { return _AckNum; }
                set { _AckNum = value; }
            }

            public IPAddress Address
            {
                get { return _address; }
                set { _address = value; }
            }

            public byte[] data
            {
                get { return _data; }
                set { _data = value; }
            }
        }

        volatile List<dataToSend> ToSend = new List<dataToSend>();

        public slave()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread ListenUDP = new Thread(new ThreadStart(listeningUDP));
            ListenUDP.IsBackground = true;
            ListenUDP.Start();
        }        

        void SpracujData(string data)
        {

        }
        
        void matlab(string code)
        {
            string[] lines, var_temp;
            List<string> variables=new List<string>();
            List<string> var_result = new List<string>();
            int var_count=0;

            lines = code.Split('\r');
            

            MLApp.MLApp matlab = new MLApp.MLApp();
            string d;
            d = "";

            for (int a = 0; a < lines.Length; a++)
            {
                if(lines[a].StartsWith("\n"))
                    lines[a]=lines[a].Remove(0,1);
                
                string i = matlab.Execute(lines[a]);
                //TODO osetrit ak nie je spravny matlabovsky prikaz

                if (lines[a].Contains("="))                         //ak obsahuje = prekpokladame ze pred = je premenna ktoru posleme spet
                {
                    var_temp = lines[a].Split('=')[0].Split(", ".ToCharArray());   
                    for (int b = 0; b < var_temp.Length; b++)
                    {
                        if (var_temp[b].StartsWith("["))                //odstranime [] ak nahodou obsahuje
                        {
                            var_temp[b]=var_temp[b].Remove(0, 1);
                        }
                        if (var_temp[b].EndsWith("]"))                //odstranime [] ak nahodou obsahuje
                        {
                            var_temp[b]=var_temp[b].Remove(var_temp[b].Length - 1, 1);
                        }
                        if (!variables.Contains(var_temp[b]) && var_temp[b]!="")
                        {
                            variables.Add(var_temp[b]);
                            var_count++;
                        }
                    }
                }
                //matlab.Execute(lines[a]);
            }
            foreach (string variable in variables)
            {
                try
                {
                    var_result.Add(Convert.ToString(matlab.GetVariable(variable, "base")));
                }
                catch(Exception e)
                {
                    var_result.Add(variable + ": ??? Undefined function or variable");
                }
            }
            
            for (int a = 0; a < variables.Count; a++)
            {
                result += variables[a] + " = " + var_result[a] + "\r\n";
            }
        }

        void listeningUDP()
        {
            /****
             * 0x01 - poziadavka na odpoved
             * 0x04 - data na vypocet
             * */
            int address_len, port_len;
            int offset = 0;
            
            while (true)
            {
                string[] remoteAddress;
                IPEndPoint ie = new IPEndPoint(IPAddress.Any, 9050);
                EndPoint iep = (EndPoint)ie;
                char[] send_data = new char[1024];
                dataToSend dToSend;

                Socket test = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);                
                test.Bind(ie);
                byte[] data = new byte[1024];
                test.Blocking = true;
                test.ReceiveFrom(data, ref iep);        //prijme paket

                remoteAddress = iep.ToString().Split(':');
                IPEndPoint ie2 = new IPEndPoint(IPAddress.Parse(remoteAddress[0]), 9051); 

                EndPoint iep2 = (EndPoint)ie2;

                if (data[1] != 0)
                {
                    if (!Ack.Contains(data[1]))
                    {
                        Ack.Add(data[1]);
                    }
                }

                if ((data[0] > 0)&&((data[2] >> 4) & 0x01)<1)
                {
                    IPAddress addr = ((IPEndPoint)iep).Address;
                    PosliAck(remoteAddress[0], data[0]);
                }
                
                while (((data[2] >> 4)) > 0)
                {
                    if (((data[2] >> 4) & 0x01) > 0)
                    {
                        data[2] -= 0x01 << 4;

                        //Odoslat(Data(Dns.GetHostName().ToString(), CPU_num()), test, iep2);
                        //dToSend=new dataToSend();
                        //dToSend.AckNum = 0;
                        //dToSend.Address = (IPAddress)((IPEndPoint)(iep)).Address;
                        //dToSend.data = Data(Dns.GetHostName().ToString(), CPU_num());

                        //ToSend.Add(dToSend);

                        //Thread Odosielanie = new Thread(new ThreadStart(Odoslat));
                        //Odosielanie.IsBackground = true;
                        //Odosielanie.Start();
                        Odosli((IPAddress)((IPEndPoint)(iep)).Address, Data(Dns.GetHostName().ToString(), CPU_num()), 0);
                    }
                    if (((data[2] >> 4) & 0x04) > 0)
                    {
                        byte[] vypocet = new byte[1024];
                        int vypocet_len = 0;
                        string vypocet_string;

                        data[2] -= 0x04 << 4;
                        vypocet_len = (int)data[3];
                        for (int a = 0; a < vypocet_len; a++)
                        {
                            vypocet[a] = data[a + 4];
                        }
                        vypocet_string=Encoding.ASCII.GetString(vypocet,0,vypocet_len);
                        
                        matlab(vypocet_string);
                        //TODO: poslat ACK o prijatych
                        
                        //test.SendTo(Data(result), iep2);
                        Odosli((IPAddress)((IPEndPoint)(iep)).Address, Data(result), 0);
                        
                    }
                }
                test.Close();
            }
        }
        void Odosli(IPAddress ipadresa, byte[] data, int Acknum)
        {
            dataToSend dToSend = new dataToSend();

            dToSend.AckNum = Acknum;
            dToSend.Address = ipadresa;
            dToSend.data = data;

            ToSend.Add(dToSend);

            Thread Odosielanie = new Thread(new ThreadStart(Odoslat));
            Odosielanie.IsBackground = true;
            Odosielanie.Start();
        }

        void Odoslat()
        {
            if (ToSend.Count > 0)
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                IPEndPoint ie = new IPEndPoint(ToSend[0].Address, 9051);

                EndPoint iep = (EndPoint)ie;
                
                int num = 0;
                packetNumber++;
                do
                {
                    num++;
                    ToSend[0].data[0] = Convert.ToByte(packetNumber);
                    ToSend[0].data[1] = 0;
                    socket.SendTo(ToSend[0].data, iep);
                } while ((WaitAck() == false) && (num < 5));
                ToSend.RemoveAt(0);
                socket.Close();
            }
            

        }

        bool WaitAck()
        {
            Thread.Sleep(3000);

            if (Ack.Contains(packetNumber))
            {
                Ack.Remove(packetNumber);
                return true;
            }
            else
                return false;           
        }

        void ReadHeader(byte[] data, Socket test, EndPoint iep2)
        {
            while (((data[2] >> 4)) > 0)
            {
                if (((data[2] >> 4) & 0x01) > 0)        //posle info ze zije
                {
                    data[2] -= 0x01 << 4;

                    SendHello(test, iep2);
                }
                if (((data[2] >> 4) & 0x04) > 0)        //dostane data na vypocet
                {
                    byte[] vypocet = new byte[1024];
                    int vypocet_len = 0;
                    string vypocet_string;

                    data[2] -= 0x04 << 4;
                    vypocet_len = (int)data[3];
                    for (int a = 0; a < vypocet_len; a++)
                    {
                        vypocet[a] = data[a + 4];
                    }
                    vypocet_string = Encoding.ASCII.GetString(vypocet, 0, vypocet_len);

                    matlab(vypocet_string);
                    //TODO: poslat ACK o prijatych ?? prijat

                    SendPacket(test, iep2, Data(result));
                }
            }
        }

        void SendHello(Socket test, EndPoint iep2)
        {
            test.SendTo(Data(Dns.GetHostName().ToString(), CPU_num()), iep2);
            Thread.Sleep(3000);

            //TODO: prijat ack
        }
        void SendPacket(Socket test, EndPoint iep2, byte[] data)
        {
            test.SendTo(data, iep2);            

            //TODO: prijat ack
        }

        void PosliAck(string address, int num)
        {
            byte[] send_data = new byte[1024];
            Socket test = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint ie2 = new IPEndPoint(IPAddress.Parse(address), 9051);

            EndPoint iep2 = (EndPoint)ie2;
            //test.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);            

            send_data[0] = 0;
            send_data[1] = Convert.ToByte(num);
            test.SendTo(send_data, iep2);
            test.Close();
            
        }

        byte[] Data(string name, int num_CPU)
        {
            /*************          //stara tab - hore je nova
             * 1. byte, 1-4 bit
             * 0x01 - meno            
             * 0x02 - pocet cpu
             * 0x04 - data na vypocet
             * 1. byte, 5-8 bit dlzka mena max. 17
             * 2-x - meno
             * x+1 - pocet cpu
             *     - velkost dat na vypocet
             *     - data na vypocet
            *************/
            byte[] data = new byte[1024];
            data[2] = (0x1 + 0x2)<<4;               //obsah dat
            //if (name.Length < 18)
            //{
            //    data[0] += (byte)(name.Length);     //dlzka mena < 17
            //}
            //else
            //{
            //    name = name.Substring(0, 17);
            //    data[0] += (byte)(name.Length);     //dlzka mena == 17
            //}
            data[0] = Convert.ToByte(packetNumber);
            
            data[3] = (byte)name.Length;
            for (int a = 0; a < name.Length; a++)   //meno
            {
                data[a + 4] = (byte)name.ToCharArray()[a];
            }
            
            data[name.Length + 4] = (byte)num_CPU;  //pocet cpu          

            return data;
        }

        byte[] Data(string name, int num_CPU, string vysledok)
        {
            /*************
             * 1. byte, 1-4 bit
             * 0x01 - meno            
             * 0x02 - pocet cpu
             * 0x04 - data na vypocet
             * 1. byte, 5-8 bit dlzka mena max. 17
             * 2-x - meno
             * x+1 - pocet cpu
             *     - velkost dat na vypocet
             *     - data na vypocet
            *************/
            byte[] data = new byte[1024];
            data[0] = Convert.ToByte(packetNumber);
            data[2] = (0x01 + 0x02 + 0x04) << 4;               //obsah dat
            //if (name.Length < 18)
            //{
            //    data[0] += (byte)(name.Length);     //dlzka mena < 17
            //}
            //else
            //{
            //    name = name.Substring(0, 17);
            //    data[0] += (byte)(name.Length);     //dlzka mena == 17
            //}
            data[3] = (byte)name.Length;
            for (int a = 0; a < name.Length; a++)   //meno
            {
                data[a + 2] = (byte)name.ToCharArray()[a];
            }

            data[name.Length + 4] = (byte)num_CPU;  //pocet cpu          

            data[name.Length + 5] = (byte)(vysledok.Length);
            for (int a = 0; a < vysledok.Length; a++)
            {
                data[name.Length + 6] = (byte)vysledok[a];
            }

            return data;
        }

        byte[] Data(string vysledok)
        {
            /*************
             * 1. byte, 1-4 bit
             * 0x01 - meno            
             * 0x02 - pocet cpu
             * 0x04 - data na vypocet
             * 1. byte, 5-8 bit dlzka mena max. 17
             * 2-x - meno
             * x+1 - pocet cpu
             *     - velkost dat na vypocet
             *     - data na vypocet
            *************/
            byte[] data = new byte[1024];
            data[2] = (0x04) << 4;               //obsah dat
                     

            data[3] = (byte)(vysledok.Length);
            for (int a = 0; a < vysledok.Length; a++)
            {
                data[a + 4] = (byte)vysledok[a];
            }

            return data;
        }

        //byte[] Data(string name)
        //{
        //    /*************
        //     * 1. byte, 1-4 bit
        //     * 0x1 - meno            
        //     * 0x2 - pocet cpu
        //     * 
        //     * 2. byte dlzka mena 
        //     * 3-x - meno
        //    *************/
        //    byte[] data = new byte[1024];
        //    data[0] = (0x1 + 0x2) << 4;             //obsah dat
        //    //if (name.Length < 18)
        //    //{
        //    //    data[0] += (byte)(name.Length);     //dlzka mena < 17
        //    //}
        //    //else
        //    //{
        //    //    name = name.Substring(0, 17);
        //    //    data[0] += (byte)(name.Length);     //dlzka mena == 17
        //    //}
        //    data[1] = (byte)name.Length;

        //    for (int a = 0; a < name.Length; a++)   //meno
        //    {
        //        data[a + 2] = (byte)name.ToArray()[a];
        //    }       

        //    return data;
        //}

        int CPU_num()
        {
            int coreCount = 0;

            coreCount= Environment.ProcessorCount;

            return coreCount;
        }

        private void tlacidlo_Click(object sender, EventArgs e)
        {
            return;
        }
    }
}
