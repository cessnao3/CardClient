using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardClient
{
    public partial class MainMenu : Form
    {
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
        }
    }
}
