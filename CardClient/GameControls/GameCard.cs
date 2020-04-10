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
        GameLibrary.Cards.Card base_card;
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
            if (base_card == null || !card_shown)
            {
                this.PicCard.Image = Properties.Resources.card_back;
            }
            else
            {
                string suit_name = base_card.suit.ToString().ToLower();
                string value_name = base_card.value.ToString().ToLower();

                string card_name = string.Format(
                    "{0:s}_{1:s}",
                    suit_name,
                    value_name);

                this.PicCard.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject(card_name);
            }

            //this.Visible = base_card != null;
        }

        public void SetWidth(int width)
        {
            this.Width = width;
            this.Height = (int)(width * 1.5);
        }
    }
}
