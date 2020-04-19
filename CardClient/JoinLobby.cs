using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CardGameLibrary.GameParameters;
using CardGameLibrary.Messages;

namespace CardClient
{
    public partial class JoinLobby : Form
    {
        int lobby_id;

        public JoinLobby(int lobby_id)
        {
            InitializeComponent();

            this.lobby_id = lobby_id;

            tmrStatusUpdate_Tick(null, null);
        }

        public void UpdateStatus(MsgLobbyStatus status)
        {
            if (lobby_id != status.game_id) return;

            string[] dir_string = new string[]
            {
                "North",
                "East",
                "South",
                "West"
            };

            Button[] buttons = new Button[]
            {
                BtnNorth,
                BtnEast,
                BtnSouth,
                BtnWest
            };

            bool player_is_in = false;
            GamePlayer player = Network.GameComms.GetPlayer();

            for (int i = 0; i < Math.Min(4, status.players.Count); ++i)
            {
                if (player.Equals(status.players[i]))
                {
                    player_is_in = true;
                }
            }

            for (int i = 0; i < Math.Min(4, status.players.Count); ++i)
            {
                if (status.players[i] == null)
                {
                    buttons[i].Text = dir_string[i];
                    buttons[i].Enabled = !player_is_in;
                }
                else
                {
                    buttons[i].Enabled = false;
                    buttons[i].Text = string.Format(
                        "{0:} {1:}",
                        dir_string[i].Substring(0, 1),
                        status.players[i].CapitalizedName().Substring(0, Math.Min(3, status.players[i].CapitalizedName().Length)));
                }
            }

            BtnLeave.Enabled = player_is_in;
        }

        private void tmrStatusUpdate_Tick(object sender, EventArgs e)
        {
            Network.GameComms.SendMessage(new MsgClientRequest()
            {
                request = MsgClientRequest.RequestType.LobbyStatus,
                game_id = lobby_id,
                data = -1
            });
        }

        private void SendJoinRequest(LobbyPositions pos)
        {
            Network.GameComms.SendMessage(new MsgClientRequest()
            {
                request = MsgClientRequest.RequestType.JoinLobby,
                game_id = lobby_id,
                data = (int)pos
            });
        }

        private void SendLeaveRequest()
        {
            Network.GameComms.SendMessage(new MsgClientRequest()
            {
                request = MsgClientRequest.RequestType.LeaveLobby,
                game_id = lobby_id,
                data = -1
            });
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnLeave_Click(object sender, EventArgs e)
        {
            SendLeaveRequest();
        }

        private void BtnNorth_Click(object sender, EventArgs e)
        {
            SendJoinRequest(LobbyPositions.North);
        }

        private void BtnSouth_Click(object sender, EventArgs e)
        {
            SendJoinRequest(LobbyPositions.South);
        }

        private void BtnEast_Click(object sender, EventArgs e)
        {
            SendJoinRequest(LobbyPositions.East);
        }

        private void BtnWest_Click(object sender, EventArgs e)
        {
            SendJoinRequest(LobbyPositions.West);
        }
    }
}
