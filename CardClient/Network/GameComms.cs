using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using GameLibrary.Messages;
using GameLibrary.Network;

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
            // Do Nothing
        }

        public void ResetSocket()
        {
            if (client != null) client.Close();
            client = new TcpClient();
            //client.Connect(IPAddress.Loopback, 8088);
            client.Connect(IPAddress.Parse("10.0.0.11"), 8088);
        }

        public void SendString(string s)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(s);
            client.GetStream().Write(bytes, 0, bytes.Length);
        }

        public void SendMessage(MsgBase msg)
        {
            MessageReader.SendMessage(client, msg);
        }

        public MsgBase ReceiveMessage()
        {
            return MessageReader.ReadMessage(client);
        }
    }
}
