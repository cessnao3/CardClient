using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace CardClient.Network
{
    public class GameComms
    {
        TcpClient client;

        protected static GameComms gc_instance = new GameComms();

        static public GameComms GetInstance()
        {
            return gc_instance;
        }

        protected GameComms()
        {
            client = new TcpClient();
            client.Connect(IPAddress.Loopback, 8088);
        }

        public void SendString(string s)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(s);
            client.GetStream().Write(bytes, 0, bytes.Length);
        }
    }
}
