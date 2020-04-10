using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Newtonsoft.Json;

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

            if (username.Length > 0 && password.Length > 0)
            {
                output_username = username;
                output_password = password;

                GameLibrary.Network.GameConnectionMessage msg = new GameLibrary.Network.GameConnectionMessage();
                msg.action = GameLibrary.Network.GameConnectionMessage.ActionType.LoginUser;
                msg.username = username;
                msg.password_hash = password;

                string json = JsonConvert.SerializeObject(msg);

                Network.GameComms.GetInstance().SendString(json);

                Close();
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
