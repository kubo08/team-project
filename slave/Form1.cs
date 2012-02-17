using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using helpers;

namespace slave
{
    public partial class Form1 : Form
    {
        const int Listen_port = 8000, Send_port=8001;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int address_len, port_len;
            int offset = 0;
            IPAddress ia = IPAddress.Any;
            IPEndPoint ie = new IPEndPoint(ia, 8000);
            EndPoint iep = (EndPoint)ie;
            char[] send_data = new char[1024];

            Socket test = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //test.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.BlockSource, false);
            test.Bind(ie);
            //test.Listen(5);
            //Socket newSocket = test.Accept();
            byte[] data = new byte[1024];
            //newSocket.Receive(data);
            test.ReceiveFrom(data, ref iep);
            address_len = Convert.ToInt16(Encoding.ASCII.GetString(data).Substring(0,3));
            port_len = Convert.ToInt16(Encoding.ASCII.GetString(data).Substring(4+address_len,4));
            IPEndPoint ie2 = new IPEndPoint(IPAddress.Parse(Encoding.ASCII.GetString(data).Substring(4, address_len)), Convert.ToInt16(Encoding.ASCII.GetString(data).Substring(8 + address_len, port_len)));            
            //IPEndPoint ie2 = new IPEndPoint(IPAddress.Loopback, 8001);
            EndPoint iep2 = (EndPoint)ie2;

            richTextBox1.Text += Encoding.ASCII.GetString(data).Substring(8+address_len+port_len);
            send_data = fillUDP.fillingUDP(out offset, Listen_port);
            test.SendTo(Encoding.ASCII.GetBytes(send_data), iep2);
            test.Close();
        }
    }
}
