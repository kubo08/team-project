using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using Microsoft.Win32;

namespace network
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Socket newSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            ////IPAddress a = new IPAddress(

            //IPAddress ip = IPAddress.Parse("196.15.1.1");
            //IPAddress a = IPAddress.Any;
            //IPAddress b = IPAddress.Broadcast;
            //IPAddress c = IPAddress.Loopback;
            //IPAddress d = IPAddress.None;
            //IPHostEntry ihe = Dns.GetHostByName(Dns.GetHostName());
            //IPAddress myself = ihe.AddressList[0];
            //if (IPAddress.IsLoopback(c))
            //{
            //    Console.WriteLine("{0}", c.ToString());
            //}
            //IPEndPoint ipend = new IPEndPoint(ip, 8000);
            //richTextBox1.Text += string.Format("{0}\r\n", ipend.AddressFamily);
            //richTextBox1.Text += string.Format("{0}\r\n", ipend.Address);
            //richTextBox1.Text += string.Format("{0}\r\n", ipend.Port);
            //richTextBox1.Text += string.Format("{0}\r\n", IPEndPoint.MinPort);
            //richTextBox1.Text += string.Format("{0}\r\n", IPEndPoint.MaxPort);
            //richTextBox1.Text += string.Format("{0}\r\n", ipend.ToString());
            //SocketAddress sa = ipend.Serialize();
            //richTextBox1.Text += string.Format("{0}\r\n", sa.ToString());

            IPHostEntry ipe = Dns.GetHostByName(Dns.GetHostName());

            IPAddress ia = IPAddress.Any;
            IPEndPoint ie = new IPEndPoint(ia, 8000);
            Socket test = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //test.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.BlockSource, false);
            test.Bind(ie);
            test.Listen(5);
            Socket newSocket = test.Accept();
            byte[] data = new byte[1024];
            newSocket.Receive(data);
            richTextBox1.Text += Encoding.ASCII.GetString(data);
            //richTextBox1.Text += test.EnableBroadcast + Environment.NewLine + test.LocalEndPoint + Environment.NewLine + test.RemoteEndPoint;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IPHostEntry ipe = Dns.GetHostByName(Dns.GetHostName());

            IPAddress ia = IPAddress.Any;
            IPEndPoint ie = new IPEndPoint(ia, 8001);
            Socket test = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            test.Bind(ie);
            byte[] text = (Encoding.ASCII.GetBytes("testovaci text"));
            IPAddress ia2= IPAddress.Broadcast;
            IPEndPoint ie2 = new IPEndPoint(ia2, 8000);
            test.Connect(ie2);
            test.Send(text); 
       
            //richTextBox1.Text += test.EnableBroadcast + Environment.NewLine + test.LocalEndPoint + Environment.NewLine + test.RemoteEndPoint;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int recv;
            byte[] data = new byte[1024];
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);
            Socket newsock = new
            Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);
            newsock.Bind(ipep);
            newsock.Listen(10);
            richTextBox1.Text += ("Waiting for a client...");
            Socket client = newsock.Accept();
            IPEndPoint clientep =
            (IPEndPoint)client.RemoteEndPoint;
            richTextBox1.Text +=string.Format("Connected with {0} at port {1}",
            clientep.Address, clientep.Port);
            string welcome = "Welcome to my test server";
            data = Encoding.ASCII.GetBytes(welcome);
            client.Send(data, data.Length,
            SocketFlags.None);
            while (true)
            {
                data = new byte[1024];
                recv = client.Receive(data);
                if (recv == 0)
                    break;
                richTextBox1.Text += (
                Encoding.ASCII.GetString(data, 0, recv));
                client.Send(data, recv, SocketFlags.None);
            }
            richTextBox1.Text +=string.Format("Disconnected from {0}",
            clientep.Address);
            client.Close();
            newsock.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            IPHostEntry ipe = Dns.GetHostByName(Dns.GetHostName());

            IPAddress ia = IPAddress.Any;
            IPEndPoint ie = new IPEndPoint(ia, 8000);
            EndPoint iep = (EndPoint)ie;
            
            Socket test = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //test.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.BlockSource, false);
            test.Bind(ie);
            //test.Listen(5);
            //Socket newSocket = test.Accept();
            byte[] data = new byte[1024];
            //newSocket.Receive(data);
            test.ReceiveFrom(data, ref iep);
            IPEndPoint ie2 = new IPEndPoint(IPAddress.Loopback, 8001);
            EndPoint iep2 = (EndPoint)ie2;

            richTextBox1.Text += Encoding.ASCII.GetString(data);
            test.SendTo(Encoding.ASCII.GetBytes(Dns.GetHostName()),iep2);
            test.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            IPHostEntry ipe = Dns.GetHostByName(Dns.GetHostName());
            //HostEntry ipe2 = Dns.GetHostEntry(Dns.GetHostName());

            IPAddress ia = IPAddress.Broadcast;
            IPEndPoint ie = new IPEndPoint(IPAddress.Any, 8001);
            IPEndPoint ie2 = new IPEndPoint(ia, 8000);
            EndPoint iep = (EndPoint)ie2;
            EndPoint iep2 = (EndPoint)ie;
            Socket test = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            Socket test2 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            test.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
            //test.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.BlockSource, false);
            //udp.Send(Encoding.ASCII.GetBytes("ohlas sa"), Encoding.ASCII.GetBytes("ohlas sa").Length, ie);
            //test.Bind(ie);
            //test.Listen(5);
            //Socket newSocket = test.Accept();
            byte[] data = new byte[1024];
            string aaa = string.Empty;
            aaa += "Ip:" + ipe.AddressList[0];
            //newSocket.Receive(data);
            test.SendTo(Encoding.ASCII.GetBytes("ohlas sa"), iep);

            test2.Bind(ie);
            test2.ReceiveFrom(data,ref iep2);
            richTextBox1.Text += Encoding.ASCII.GetString(data);
            test.Close();
            test2.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            richTextBox1.Text+=string.Format("Number Of Logical Processors: {0}", Environment.ProcessorCount);
            //foreach (var item in new System.Management.Instrumentation.management ("Select * from Win32_ComputerSystem").Get())
            //{
            //    Console.WriteLine("Number Of Physical Processors: {0} ", item["NumberOfProcessors"]);
            //}
            //int coreCount = 0;
            //foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_Processor").Get())
            //{
            //    coreCount += int.Parse(item["NumberOfCores"].ToString());
            //}
            //Console.WriteLine("Number Of Cores: {0}", coreCount);
        }
    }
}
