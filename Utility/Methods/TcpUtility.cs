using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace Utility.Methods
{
    static class TcpUtility
    {
        public static void Connect(TcpClient connection, string ip, int port, out SocketException ex)
        {
            ex = null;

            for (int i = 0; i < 10 && !connection.Connected; i++)
            {
                try
                {
                    connection.Connect(IPAddress.Parse(ip), port);
                    break;
                }
                catch (SocketException se)
                {
                    ex = se;
                    continue;
                }
            }
        }
    }
}
