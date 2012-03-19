using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace helpers
{
    public class Slave
    {
        public string name { get; set; }
        public string ip { get; set; }
        public bool matlab { get; set; }
        public int num_cpu { get; set; }
        public bool checkedItem { get; set; }
    }

    public class staticAddress
    {
        public string startAddr { get; set; }
        public string endAddr { get; set; }
    }

    public class sender
    {
        private static int packetNumber = 0;
        private static int maxPacketNum = 16000;       ///opravit
        private static List<int> Ack = new List<int>();

        public static List<Slave> slaves = new List<Slave>();

        public static void Send(string address, string data)            //posiela data na ip adresu
        {
            byte[] send_data = new byte[1024];

            Socket dataSender = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint ie2 = new IPEndPoint(IPAddress.Parse(address), 9050);

            EndPoint iep2 = (EndPoint)ie2;

            
            /**
             * data
             * */
            //data = txtData.Text;

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

        private static void Odoslat(byte[] send_data, Socket dataSender, EndPoint iep)
        {
            int num = 0;
            packetNumber++;
            do
            {
                num++;
                send_data[0] = Convert.ToByte(packetNumber);
                send_data[1] = 0;
                dataSender.SendTo(send_data, iep);
            } while ((WaitAck() == false) && (num < 5));
        }

        private static bool WaitAck()                  //zistuje ci bolo prijate ack
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
    }
}
