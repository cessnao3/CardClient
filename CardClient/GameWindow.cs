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
            gameScreen.UpdateFromStatus(status);
        }
    }
}
