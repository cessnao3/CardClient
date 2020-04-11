using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameLibrary.Messages;
using GameLibrary.Network;

namespace CardClient
{
    public partial class LoginWindow : Form
    {
        public string output_username { get; private set; }
        public string output_password { get; private set; }

        public LoginWindow()
        {
            InitializeComponent();

            DialogResult = DialogResult.Cancel;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = TxtUser.Text.ToLower().Trim();
            string password = TxtPass.Text.ToLower().Trim();

            bool is_new_user = (Button)sender == BtnNew;

            if (username.Length > 0 && password.Length > 0)
            {
                output_username = username;
                output_password = password;

                MsgLogin msg = new MsgLogin();
                msg.action = (is_new_user) ? MsgLogin.ActionType.NewUser : MsgLogin.ActionType.LoginUser;
                msg.username = username;
                msg.password_hash = password;

                Network.GameComms gc = Network.GameComms.GetInstance();
                gc.ResetSocket();
                gc.SendMessage(msg);

                MsgServerResponse msg_response = null;

                for (int i = 0; i < 10; ++i)
                {
                    MsgBase msg_b = gc.ReceiveMessage();

                    if (msg_b == null)
                    {
                        Thread.Sleep(100);
                    }
                    else
                    {
                        if (msg_b is MsgServerResponse)
                        {
                            msg_response = (MsgServerResponse)msg_b;
                        }

                        break;
                    }
                }

                if (msg_response != null && msg_response.code == ResponseCodes.OK)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Login Failed");
                }
            }
        }

        private void TxtBox_UpdateText(object sender, EventArgs e)
        {
            string string_val = ((TextBox)sender).Text.Trim().ToLower();
            int i = 0;
            while (i < string_val.Length)
            {
                if ((string_val[i] >= 'a' && string_val[i] <= 'z') ||
                    (string_val[i] >= '0' && string_val[i] <= '9'))
                {
                    i += 1;
                }
                else
                {
                    string_val.Remove(i);
                }
            }

            ((TextBox)sender).Text = string_val;
        }
    }
}
