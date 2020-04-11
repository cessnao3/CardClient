using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameLibrary.Messages;

namespace CardClient
{
    public partial class MainMenu : Form
    {
        Dictionary<int, GameWindow> game_windows = new Dictionary<int, GameWindow>();

        public MainMenu()
        {
            InitializeComponent();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.ShowDialog(this);

            if (lw.DialogResult != DialogResult.OK)
            {
                Close();
            }
            else
            {
                tmrServerTick.Enabled = true;
                game_windows.Add(0, new GameWindow(0));
                game_windows[0].Show(this);
            }
        }

        private void tmrServerTick_Tick(object sender, EventArgs e)
        {
            // Clean existing game windows
            List<int> windows_to_remove = new List<int>();
            foreach (var pair in game_windows)
            {
                if (!pair.Value.Visible)
                {
                    windows_to_remove.Add(pair.Key);
                }
            }

            foreach (int i in windows_to_remove)
            {
                game_windows.Remove(i);
            }

            // Read input messages
            int read_msg = 0;
            while (read_msg < 10)
            {
                MsgBase msg = Network.GameComms.ReceiveMessage();

                if (msg == null) break;

                Console.WriteLine("Received " + msg.GetType().ToString());

                if (msg is MsgGameStatus)
                {
                    MsgGameStatus status = (MsgGameStatus)msg;

                    if (game_windows.ContainsKey(status.game_id))
                    {
                        game_windows[status.game_id].UpdateGame(status);
                    }
                }

                read_msg += 1;
            }

            // Check for server failure
            if (Network.GameComms.Failed())
            {
                Application.Restart();
            }
        }
    }
}
