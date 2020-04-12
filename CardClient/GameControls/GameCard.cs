using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardClient.GameControls
{
    public partial class GameCard : UserControl
    {
        public GameLibrary.Cards.Card base_card { get; private set; }
        bool card_shown = false;

        public GameCard(GameLibrary.Cards.Card card=null)
        {
            InitializeComponent();

            base_card = card;
            UpdatePicture();
        }

        public void SetCard(GameLibrary.Cards.Card c)
        {
            base_card = c;
            UpdatePicture();
        }

        public void SetFaceShown(bool face_shown)
        {
            card_shown = face_shown;
            UpdatePicture();
        }

        public void UpdatePicture()
        {
            Bitmap bmp_to_set;

            if (base_card == null || !card_shown)
            {
                bmp_to_set = Properties.Resources.card_back;
            }
            else
            {
                string suit_name = base_card.suit.ToString().ToLower();
                string value_name = base_card.value.ToString().ToLower();

                string card_name = string.Format(
                    "{0:s}_{1:s}",
                    suit_name,
                    value_name);

                bmp_to_set = (Bitmap)Properties.Resources.ResourceManager.GetObject(card_name);
            }

            PicCard.Image = bmp_to_set;

            Visible = base_card != null;
        }

        public void SetWidth(int width)
        {
            Size = new Size(width, (int)(width * HeightRatio()));
            UpdatePicture();
        }

        public static double HeightRatio()
        {
            return 1.5;
        }

        private void PicCard_Click(object sender, EventArgs e)
        {
            this.InvokeOnClick(this, e);
        }
    }
}
