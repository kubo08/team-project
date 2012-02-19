using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.Net.Sockets;

/**********
 * Master UDP Listen on port 9051
 * Master UDP Send on port 9050
 * Master UDP Listen for Ack 9052
 * Master TCP Send on port 9055
 * Master TCP Listen on port 9058
************/


namespace master_multithread
{

    public partial class Form1 : Form
    {
        List<Slave> slaves = new List<Slave>();
        private Panel SlavePanel;
        private List<CheckBox> lstCheckSlave;
        List<Label> lstlblSlaveName = new List<Label>();
        List<Label> lstlblSlaveIp = new List<Label>();
        List<Label> lstlblCpu = new List<Label>();
        private CheckBox checkSlave;
        private List<bool> checkedItems = new List<bool>();
        private Socket server;
        int testSend = 0;
        private byte[] prijate_data = new byte[1024];
        private int prijate_dlzka;
        private EndPoint prijate_EndPoint;
        List<int> Ack = new List<int>();
        List<staticAddress> stAddresses = new List<staticAddress>();
        int maxPacketNum = 16000;       ///opravit

        int packetNumber = 1;

        private class Slave
        {
            public string name {get; set;}
            public string ip { get; set; }
            public bool matlab { get; set; }
            public int num_cpu { get; set; }
            public bool checkedItem { get; set; }
        }

        private class staticAddress
        {
            public string startAddr { get; set; }
            public string endAddr { get; set; }
        }

        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SlavePanel = new Panel();                
            SlavePanel.Location = new Point(10, 30);
            SlavePanel.Size = new System.Drawing.Size(230, 200);
            SlavePanel.AutoScroll = true;
            SlavePanel.Parent = this;
            SlavePanel.BorderStyle = BorderStyle.FixedSingle;
            Label lblName = new Label();
            lblName.Name = "PcName";
            lblName.Location = new Point(50, 10);
            lblName.AutoSize = true;
            lblName.Text = "Meno";
            lblName.AutoSize = true;
            lblName.Parent = this;
            Label lblAddress = new Label();
            lblAddress.Name = "IpAddress";
            lblAddress.Location = new Point(150, 10);
            lblAddress.AutoSize = true;
            lblAddress.Text = "Ip";
            lblAddress.AutoSize = true;
            lblAddress.Parent = this;
            Label lblCpu = new Label();
            lblCpu.Name = "CpuCount";
            lblCpu.Location = new Point(200, 10);
            lblCpu.AutoSize = true;
            lblCpu.Text = "Cpu";
            lblCpu.AutoSize = true;
            lblCpu.Parent = this;
            Button btnSend = new Button();
            btnSend.Text = "Poslat";
            btnSend.Name = "SendTcp";
            btnSend.Location = new Point(10, 235);
            btnSend.AutoSize = true;
            btnSend.Parent = this;
            btnSend.Click+=new EventHandler(btnSend_Click);
            Button btnCheck = new Button();
            btnCheck.Text = "Vyber všetky";
            btnCheck.Name = "CheckAll";
            btnCheck.Location = new Point(90, 235);
            btnCheck.AutoSize = true;
            btnCheck.Parent = this;
            btnCheck.Click += new EventHandler(btnCheck_Click);

            Thread findUDP = new Thread(new ThreadStart(ListenUDP));
            findUDP.IsBackground = true;
            findUDP.Start();
            Thread SendUDP = new Thread(new ThreadStart(Send));
            SendUDP.IsBackground = true;
            SendUDP.Start();
            
        }

        void Send()
        {
            bool cont = true;
            IPAddress ipadd;
            string[] startAdd, endAdd;
            while (true)
            {
                byte[] send_data = new byte[1024];

                Socket test = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                IPEndPoint ie2 = new IPEndPoint(IPAddress.Broadcast, 9050); 

                EndPoint iep2 = (EndPoint)ie2;
                test.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);

                send_data[2] = 0x01<<4;         //hladame spustenych slave-ov
                test.SendTo(send_data, iep2);


                for (int i = 0; i < stAddresses.Count; i++)         //asi nebude najstastnejsie riesenie
                {
                    ipadd = IPAddress.Parse(stAddresses[i].startAddr);
                    while (cont)
                    {
                        ie2 = new IPEndPoint(ipadd, 9050);
                        iep2 = (IPEndPoint)ie2;
                        test.SendTo(send_data, iep2);
                        startAdd = (ipadd.ToString().Split('.'));
                        endAdd=(stAddresses[i].endAddr).ToString().Split('.');

                        if (Convert.ToInt32(startAdd[3]) >= Convert.ToInt32(endAdd[3]))
                        {
                            if (Convert.ToInt32(startAdd[2]) >= Convert.ToInt32(endAdd[2]))
                            {
                                if (Convert.ToInt32(startAdd[1]) >= Convert.ToInt32(endAdd[1]))
                                {
                                    if (Convert.ToInt32(startAdd[0]) >= Convert.ToInt32(endAdd[0]))
                                    {
                                        cont = false;
                                    }
                                    else
                                    {
                                        ipadd = zvysAdresu(startAdd);
                                    }
                                }
                                else
                                {
                                    ipadd = zvysAdresu(startAdd);
                                }
                            }
                            else
                            {
                                ipadd = zvysAdresu(startAdd);
                            }
                        }
                        else
                        {
                            ipadd = zvysAdresu(startAdd);
                        }
                    }
                }
                test.Close();
                Thread.Sleep(30000);
            }
        }

        IPAddress zvysAdresu(string[] add)
        {
            add[3] = (Convert.ToInt32(add[3]) + 1).ToString();
            if (Convert.ToInt32(add[3]) > 256)
            {
                add[2] = (Convert.ToInt32(add[2]) + 1).ToString();
                add[3] = (0).ToString();
                if (Convert.ToInt32(add[2]) > 256)
                {
                    add[1] = (Convert.ToInt32(add[1]) + 1).ToString();
                    add[2] = (0).ToString();
                    if (Convert.ToInt32(add[1]) > 256)
                    {
                        add[0] = (Convert.ToInt32(add[0]) + 1).ToString();
                        add[1] = (0).ToString();
                    }
                }
            }

            return IPAddress.Parse(string.Format("{0}.{1}.{2}.{3}", add[0], add[1], add[2], add[3]));
        }

        void ListenUDP()
        {            
            /**************
             * 
             *************/

            while (true)
            {
                Socket remoteslave = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                IPEndPoint iep = new IPEndPoint(IPAddress.Any, 9051);
                EndPoint ep = (EndPoint)iep;
                remoteslave.Bind(iep);
                byte[] data = new byte[1024];
                remoteslave.Blocking = true;
                int recv = remoteslave.ReceiveFrom(data, ref ep);               //prijme paket              

                string stringData = Encoding.ASCII.GetString(data, 0, recv);

                if (data[0] != 0)
                {
                    PosliAck(((IPAddress)((IPEndPoint)ep).Address).ToString(), data[0]);
                }
                if (data[1] != 0)
                {
                    if (!Ack.Contains(data[1]))
                    {
                        Ack.Add(data[1]);
                    }
                }

                DecodePacket(stringData, ep, data);

                remoteslave.Close();

                Obnov();
            }
        }

        void DecodePacket(string stringData, EndPoint ep, byte[] data)
        {
            Slave newSlave = readHeader(stringData);

            if ((data[1]) != 0)
                Ack.Add(Convert.ToInt32(data[1]));

            //ToDo: skontrolovat ci uz sa nachadza v zozname
            if ((data[2] & 0x10) > 0 || (data[2] & 0x20) > 0)
            {
                newSlave.ip = ep.ToString().Split(':')[0];
                if (!CheckIfIsInList(newSlave))
                {
                    slaves.Add(newSlave);
                }
            }
            
        }

        void PosliAck(string address, int num)
        {
            byte[] send_data = new byte[1024];
            Socket test = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint ie2 = new IPEndPoint(IPAddress.Parse(address), 9050); 

            EndPoint iep2 = (EndPoint)ie2;
            test.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);

            send_data[0] = 0;
            send_data[1] = Convert.ToByte(num);
            test.SendTo(send_data, iep2);
            test.Close();
        }

        bool CheckIfIsInList(Slave newSlave)
        {
            bool exist = false;
            Slave old;

            old = slaves.Find(t => t.ip == newSlave.ip);
            if(old==null)           //if (old.name == null && old.ip == null)
                exist = false;
            else
                exist = true;

            return exist;
        }

        Slave readHeader(string stringData)
        {
            Slave paket = new Slave();
            byte[] data = new byte[1024];
            byte[] vysledky= new byte[1024];
            int lengthName = 0, cpu = 0, data_len = 0;            

            data = Encoding.ASCII.GetBytes(stringData);

            lengthName = 0;

            while ((data[2]>>4) > 1)
            {
                if (((data[2] >> 4) & 0x01)>0)
                {                    
                    lengthName = (data[3]);
                    data[2] -= 0x01 << 4;
                    for (int a = 0; a < lengthName; a++)
                    {
                        paket.name += (char)data[a + 4];
                    }
                }
                if (((data[2] >> 4) & 0x02) > 0)
                {
                    data[2] -= 0x02 << 4;
                    paket.num_cpu = data[lengthName + 4];
                    cpu = paket.num_cpu.ToString().Length;
                }
                if (((data[2] >> 4) & 0x04) > 0)
                {
                    data[2] -= 0x04 << 4;
                    data_len = data[3 + lengthName + cpu];

                    //TODO: spracovanie prijatych dat

                    for (int a = 0; a < data_len; a++)
                    {
                        vysledky[a] = data[4 + lengthName + cpu+a];
                    }
                    refreshTextBox(Encoding.ASCII.GetString(vysledky));
                }
            }
            return paket;
        }

        void Obnov()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(delegate() { Obnov(); }));
            }
            else
            {
                lstCheckSlave = new List<CheckBox>();
                for (int i = 0; i < slaves.Count; i++)
                {
                    checkSlave = new CheckBox();
                    checkSlave.Name = "chckSlave" + i;
                    checkSlave.Parent = SlavePanel;
                    SlavePanel.Controls.Add(checkSlave);
                    checkSlave.Location = new Point(10, 5 + i * Font.Height * 2);
                    checkSlave.Visible = true;
                    checkSlave.AutoSize = true;
                    checkSlave.Width = 20;
                    checkSlave.Click+=new EventHandler(checkSlave_Click);
                    lstCheckSlave.Add(checkSlave);

                    Label lblSlaveName = new Label();
                    lblSlaveName.Name = "lblSlave" + i;
                    lblSlaveName.Parent = SlavePanel;
                    SlavePanel.Controls.Add(lblSlaveName);
                    lblSlaveName.Location = new Point(30, 5 + i * Font.Height * 2);
                    lblSlaveName.AutoSize = true;
                    lblSlaveName.Text = slaves[i].name;

                    Label lblSlaveIp = new Label();
                    lblSlaveIp.Name = "lblIp" + i;
                    lblSlaveIp.Parent = SlavePanel;
                    SlavePanel.Controls.Add(lblSlaveIp);
                    lblSlaveIp.Location = new Point(120 , 5 + i * Font.Height * 2);
                    lblSlaveIp.Text = slaves[i].ip;
                    lblSlaveIp.AutoSize = true;

                    Label lblCpu = new Label();
                    lblCpu.Name = "lblCpu" + i;
                    lblSlaveIp.Parent = SlavePanel;
                    SlavePanel.Controls.Add(lblCpu);
                    lblCpu.Location = new Point(190, 5 + i * Font.Height * 2);
                    lblCpu.Text = slaves[i].num_cpu.ToString();
                    lblCpu.AutoSize = true;
                    
                }
            }
        }

        private void checkSlave_Click(object sender, EventArgs e)
        {               
            if(((CheckBox)sender).Checked==true)
                slaves[Convert.ToInt32(((CheckBox)sender).Name.ToString().Substring(((CheckBox)sender).Name.ToString().Length - 1))].checkedItem = true;
            else
                slaves[Convert.ToInt32(((CheckBox)sender).Name.ToString().Substring(((CheckBox)sender).Name.ToString().Length - 1))].checkedItem = false;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            foreach (CheckBox box in lstCheckSlave)
            {
                box.Checked = true;
                box.CheckState = CheckState.Checked;
            }
            foreach (Slave a in slaves)
            {
                a.checkedItem = true;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            int test = 0;
            
            bool OK = false;

            textBox1.Text = "";
            
            foreach (Slave slave in slaves)
            {
                if (slave.checkedItem == true)
                {
                    Send(slave.ip);
                }
            }
            
        }

        private void Send(string address)
        {
            string data;
            byte[] send_data = new byte[1024];

            Socket dataSender = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint ie2 = new IPEndPoint(IPAddress.Parse(address), 9050);

            EndPoint iep2 = (EndPoint)ie2;

            //TODO: pripravit data na vypocet
            /**
             * data
             * */
            data = txtData.Text;

            /**
             * 
             * */
            send_data[0] = Convert.ToByte(packetNumber);
            packetNumber++;
            if (packetNumber > maxPacketNum)
                packetNumber = 1;
            send_data[2] = 0x04 << 4;
            
            send_data[3] = send_data[3] = (byte)(data).ToString().Length;
            for (int a = 0; a < (data.Length); a++)
            {
                send_data[a + 4] = (byte)(data).ToCharArray()[a];
            }

            Odoslat(send_data, dataSender, iep2);
            
            dataSender.Close();           
        }

        void Odoslat(byte[] send_data, Socket dataSender, EndPoint iep)
        {
            int num=0;
            packetNumber++;
            do
            {
                num++;
                send_data[0] = Convert.ToByte(packetNumber);
                send_data[1] = 0;
                dataSender.SendTo(send_data, iep);
            } while ((WaitAck() == false) && (num<5));
        }

        bool WaitAck()                  //zistuje ci bolo prijate ack
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

        private void Data(string data)
        {

        }

        void refreshTextBox(string data)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(delegate() { refreshTextBox(data); }));
            }
            else
            {
                textBox1.Text += data;
                textBox1.Text += "\r\n";
            }
        }

        void SendData(IAsyncResult iar)
        {
            Socket remote = (Socket)iar.AsyncState;
            int sent = remote.EndSend(iar);
            
        }

        private void btnStatic_Click(object sender, EventArgs e)
        {
            string[] addresses, address;
            staticAddress stAddress = new staticAddress();
            List<staticAddress> staticAddresses = new List<staticAddress>();
            addresses = txtIP.Text.Split(';');
            for (int i = 0; i < addresses.Length; i++)
            {
                address = addresses[i].Split('-');
                stAddress.startAddr = address[0];
                if (address.Length > 1)
                {
                    stAddress.endAddr = address[1];
                }
                else
                {
                    stAddress.endAddr = address[0];
                }
            }
        }
    }
}
