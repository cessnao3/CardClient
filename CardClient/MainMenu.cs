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
        class LobbyEntry
        {
            public int id { get; private set; }

            public LobbyEntry(int id)
            {
                this.id = id;
            }

            public override string ToString()
            {
                return string.Format("Lobby ID {0:}", id);
            }
        }

        class GameEntry
        {
            public int id { get; private set; }

            public GameEntry(int id)
            {
                this.id = id;
            }

            public override string ToString()
            {
                return string.Format("Games ID {0:}", id);
            }
        }

        GameWindow game_window = null;

        JoinLobby lobby_window = null;

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
                // Enable the tick timers
                tmrLobbyCheck.Enabled = true;
                tmrServerTick.Enabled = true;

                // Check for lobby parameters
                tmrLobbyCheck_Tick(null, null);

                /*
                Hide();

                tmrServerTick.Enabled = true;
                tmrLobbyCheck.Enabled = false;
                game_windows.Add(0, new GameWindow(0));
                game_windows[0].ShowDialog(this);
                game_windows.Remove(0);

                try
                {
                    tmrServerTick.Enabled = false;
                    tmrLobbyCheck.Enabled = true;
                    Show();
                }
                catch (ObjectDisposedException)
                {
                    // Do nothing, this object has already been disposed
                }
                */
            }
        }

        private void tmrServerTick_Tick(object sender, EventArgs e)
        {
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

                    if (game_window != null)
                    {
                        game_window.UpdateGame(status);
                    }
                }
                else if (msg is MsgGameList)
                {
                    MsgGameList game_list = (MsgGameList)msg;

                    ListGames.Items.Clear();
                    foreach (int i in game_list.games)
                    {
                        GameEntry gi = new GameEntry(id: i);
                        ListViewItem lvi = new ListViewItem(gi.ToString())
                        {
                            Tag = gi
                        };
                        ListGames.Items.Add(lvi);
                    }

                    ListLobbies.Items.Clear();
                    foreach (int i in game_list.lobbies)
                    {
                        LobbyEntry li = new LobbyEntry(id: i);
                        ListViewItem lvi = new ListViewItem(li.ToString())
                        {
                            Tag = li
                        };
                        ListLobbies.Items.Add(lvi);
                    }
                }
                else if (msg is MsgLobbyStatus)
                {
                    MsgLobbyStatus status = (MsgLobbyStatus)msg;
                    if (lobby_window != null) lobby_window.UpdateStatus(status);
                }

                read_msg += 1;
            }

            // Check for server failure
            if (Network.GameComms.Failed())
            {
#if DEBUG
                Application.Exit();
#else
                Application.Restart();
#endif
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void newLobbyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create the game lobby
            Network.GameComms.SendMessage(new MsgClientRequest()
            {
                request = MsgClientRequest.RequestType.NewLobby,
                game_id = -1,
                data = (int)GameLibrary.Games.GameTypes.Hearts
            });

            // Request a lobby list
            tmrLobbyCheck_Tick(null, null);
        }

        private void tmrLobbyCheck_Tick(object sender, EventArgs e)
        {
            // Send the message to request lobby status
            Network.GameComms.SendMessage(new MsgClientRequest()
            {
                request = MsgClientRequest.RequestType.AvailableGames,
                data = -1,
                game_id = -1
            });
        }

        private void ListLobbies_DoubleClick(object sender, EventArgs e)
        {
            LobbyEntry entry = null;

            foreach (ListViewItem lvi in ListLobbies.SelectedItems)
            {
                entry = (LobbyEntry)lvi.Tag;
            }

            if (entry != null)
            {
                // Checkout Lobby Parameters
                lobby_window = new JoinLobby(entry.id);
                lobby_window.ShowDialog();
                lobby_window = null;

                // Recheck game parameters
                tmrLobbyCheck_Tick(null, null);
            }
        }

        private void ListGames_DoubleClick(object sender, EventArgs e)
        {
            GameEntry entry = null;

            foreach (ListViewItem lvi in ListGames.SelectedItems)
            {
                entry = (GameEntry)lvi.Tag;
            }

            if (entry != null)
            {
                // Hide the background form
                Hide();
                tmrLobbyCheck.Enabled = false;

                // Checkout Lobby Parameters
                game_window = new GameWindow(entry.id);
                game_window.ShowDialog();
                game_window = null;

                // Re-show the main form
                try
                {
                    tmrLobbyCheck.Enabled = true;
                    Show();
                }
                catch (ObjectDisposedException)
                {
                    // Do nothing, this object has already been disposed
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new AboutForm()).ShowDialog(this);
        }
    }
}
