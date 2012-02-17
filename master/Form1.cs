using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;   
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using helpers;

namespace master
{
    public partial class Form1 : Form
    {
        const int Listen_Port = 8001, Send_Port = 8000;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            //Int32 port = Int32.Parse(ConfiguratioMnanager.AppSettings["port"]);

            //IPAddress localAddr =
            //IPAddress.Parse(ConfigurationManager.AppSettings["IpAddress"]);

            char[] data = new char[1024];
            byte[] recv_data = new byte[1024];
            int offset = 0, tmp_offset=0;
            int address_len, port_len;
            //IPAddress myAddress = Dns.GetHostByName(Dns.GetHostName()).AddressList[0];
            IPAddress ia = IPAddress.Broadcast;
            IPEndPoint ie = new IPEndPoint(IPAddress.Any, 8001);
            IPEndPoint ie2 = new IPEndPoint(ia, 8000);
            EndPoint iep = (EndPoint)ie2;
            EndPoint iep2 = (EndPoint)ie;
            Socket send = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            Socket recieve = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            send.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
    
            //for (int a = 0; a < myAddress.ToString().Length.ToString().Length; a++)             //da do udp paketu velkost mojej adresy
            //{
            //    data[a + offset] = myAddress.ToString().Length.ToString()[a];
            //}
            //offset += 4;

            //for (int a = 0; a < myAddress.ToString().Length; a++)                               //da do udp paketu moju adresu
            //{
            //    data[a + offset] = myAddress.ToString()[a];
            //}
            //offset += myAddress.ToString().Length;

            //for (int a = 0; a < Listen_Port.ToString().Length.ToString().Length; a++)           //da do udp paketu dlzku portu
            //{
            //    data[a + offset] = Listen_Port.ToString().Length.ToString()[a];
            //}
            //offset += 4;

            //for (int a = 0; a < Listen_Port.ToString().Length; a++)                             //da do udp paketu port na ktorom budem pocuvat
            //{
            //    data[a + offset] = Listen_Port.ToString()[a];
            //}
            //offset += Listen_Port.ToString().Length;

            //for (int a = 0; a < Dns.GetHostName().Length; a++)                                  //da do udp paketu moje meno
            //{
            //    data[a + offset] = Dns.GetHostName()[a];
            //}
            //offset += Dns.GetHostName().Length;

            //data[offset] = '\0';            

            data = fillUDP.fillingUDP(out tmp_offset, Listen_Port);

            send.SendTo(Encoding.ASCII.GetBytes(data), iep);                                    //posle paket

            recieve.Bind(ie);
            recieve.ReceiveFrom(recv_data, ref iep2);                       //caka na odpoved
            address_len = Convert.ToInt16(Encoding.ASCII.GetString(recv_data).Substring(0, 3));
            port_len = Convert.ToInt16(Encoding.ASCII.GetString(recv_data).Substring(4 + address_len, 4));
            //IPEndPoint ie2 = new IPEndPoint(IPAddress.Parse(data.ToString().Substring(4, address_len)), Convert.ToInt16(data.ToString().Substring(8 + address_len, port_len)));            
            richTextBox1.Text += Encoding.ASCII.GetString(recv_data).Substring(8 + address_len + port_len);
            send.Close();
            recieve.Close();
            
        }
    }
}
