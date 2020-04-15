using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using CardGameLibrary.Games;
using CardGameLibrary.Messages;
using CardGameLibrary.Network;

namespace CardClient.Network
{
    public class GameComms
    {
        protected ClientStruct client_struct;

        protected static GameComms gc_instance = new GameComms();

        protected GamePlayer player;

        bool failed = false;

        IPAddress host;

        public static bool SetHost(string hostname)
        {
            IPAddress[] addrs = Dns.GetHostAddresses(hostname);

            if (addrs.Length > 0)
            {
                gc_instance.host = addrs[0];
                return true;
            }
            else
            {
                return false;
            }
        }

        protected GameComms()
        {
            // Do Nothing
        }

        static public void SetPlayer(GamePlayer p)
        {
            gc_instance.player = p;
        }

        static public GamePlayer GetPlayer()
        {
            return gc_instance.player;
        }

        static public void ResetSocket()
        {
            if (gc_instance.client_struct != null)
            {
                gc_instance.client_struct.Close();
                gc_instance.client_struct = null;
            }

            TcpClient client = new TcpClient();
            client.Connect(gc_instance.host, 8088);

            gc_instance.client_struct = new ClientStruct(client);
        }

        static public bool Failed()
        {
            return gc_instance.failed;
        }

        static public void SendMessage(MsgBase msg)
        {
            if (gc_instance.client_struct == null) return;

            try
            {
                MessageReader.SendMessage(gc_instance.client_struct, msg);
            }
            catch (System.IO.IOException)
            {
                gc_instance.failed = true;
            }
        }

        static public MsgBase ReceiveMessage()
        {
            if (gc_instance.client_struct == null) return null;

            try
            {
                return MessageReader.ReadMessage(gc_instance.client_struct);
            }
            catch (System.IO.IOException)
            {
                gc_instance.failed = true;
            }

            return null;
        }
    }
}
