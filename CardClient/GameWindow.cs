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
    public partial class GameWindow : Form
    {
        int game_id;

        public GameWindow(int game_id)
        {
            InitializeComponent();

            this.game_id = game_id;
            gameScreen.SetGameID(this.game_id);

            tmrGameUpdate_Tick(null, null);

            Text = "Game - " + Network.GameComms.GetPlayer().CapitalizedName();
        }

        private void gameScreen_Click(object sender, EventArgs e)
        {
            gameScreen.onGameCardClick(sender, e);
        }

        private void tmrGameUpdate_Tick(object sender, EventArgs e)
        {
            Network.GameComms.SendMessage(new MsgClientRequest()
            {
                game_id = game_id,
                request = MsgClientRequest.RequestType.GameStatus
            });
        }

        public void UpdateGame(MsgGameStatus status)
        {
            // Check that the ID values
            if (status.game_id != game_id) return;

            // Update the status window
            gameScreen.UpdateFromStatus(status);
            if (status.current_game_status != null && status.current_game_status.Length > 0)
            {
                toolStripStatus.Text = "Status: " + status.current_game_status;
            }
            else
            {
                toolStripStatus.Text = "No Game Status";
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new AboutForm()).ShowDialog(this);
        }
    }
}
