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
        protected TcpClient client;

        protected static GameComms gc_instance = new GameComms();

        protected GameLibrary.Games.GamePlayer player;

        bool failed = false;

        IPAddress host;

        public static bool SetHost(string hostname)
        {
            return IPAddress.TryParse(
                hostname,
                out gc_instance.host);
        }

        protected GameComms()
        {
            // Do Nothing
        }

        static public void SetPlayer(GameLibrary.Games.GamePlayer p)
        {
            gc_instance.player = p;
        }

        static public GameLibrary.Games.GamePlayer GetPlayer()
        {
            return gc_instance.player;
        }

        static public void ResetSocket()
        {
            TcpClient client = gc_instance.client;

            if (client != null) client.Close();
            client = new TcpClient();
            client.Connect(gc_instance.host, 8088);

            gc_instance.client = client;
        }

        static public bool Failed()
        {
            return gc_instance.failed;
        }

        static public void SendMessage(MsgBase msg)
        {
            if (gc_instance.client == null) return;

            try
            {
                MessageReader.SendMessage(gc_instance.client, msg);
            }
            catch (System.IO.IOException)
            {
                gc_instance.failed = true;
            }
        }

        static public MsgBase ReceiveMessage()
        {
            if (gc_instance.client == null) return null;

            try
            {
                return MessageReader.ReadMessage(gc_instance.client);
            }
            catch (System.IO.IOException)
            {
                gc_instance.failed = true;
            }

            return null;
        }
    }
}
