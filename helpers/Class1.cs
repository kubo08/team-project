using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace helpers
{
    public class fillUDP
    {
        public static char[] fillingUDP(out int offset, int port)
        {
            char[] data = new char[1024];

            offset = 0;

            IPAddress myAddress = Dns.GetHostByName(Dns.GetHostName()).AddressList[0];
            for (int a = 0; a < myAddress.ToString().Length.ToString().Length; a++)             //da do udp paketu velkost mojej adresy
            {
                data[a + offset] = myAddress.ToString().Length.ToString()[a];
            }
            offset += 4;

            for (int a = 0; a < myAddress.ToString().Length; a++)                               //da do udp paketu moju adresu
            {
                data[a + offset] = myAddress.ToString()[a];
            }
            offset += myAddress.ToString().Length;

            for (int a = 0; a < port.ToString().Length.ToString().Length; a++)           //da do udp paketu dlzku portu
            {
                data[a + offset] = port.ToString().Length.ToString()[a];
            }
            offset += 4;

            for (int a = 0; a < port.ToString().Length; a++)                             //da do udp paketu port na ktorom budem pocuvat
            {
                data[a + offset] = port.ToString()[a];
            }
            offset += port.ToString().Length;

            for (int a = 0; a < Dns.GetHostName().Length; a++)                                  //da do udp paketu moje meno
            {
                data[a + offset] = Dns.GetHostName()[a];
            }
            offset += Dns.GetHostName().Length;

            data[offset] = '\0';

            return data;
        }
    }
}
