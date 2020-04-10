using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GameLibrary.Cards;

namespace CardClient
{
    public partial class GameWindow : Form
    {
        Hand player_hand;
        List<GameControls.GameCard> player_hand_controls;

        public GameWindow()
        {
            InitializeComponent();

            player_hand = new Hand();

            Deck deck = new Deck();
            deck.Shuffle();

            player_hand_controls = new List<GameControls.GameCard>();

            for (int i = 0; i < 13; ++i)
            {
                player_hand.AddCard(deck.Next());
            }

            foreach (Card c in player_hand.cards)
            {
                GameControls.GameCard card = new GameControls.GameCard(card: c);
                card.SetFaceShown(true);

                player_hand_controls.Add(card);
                Controls.Add(card);
            }

            UpdateCardLocations();
        }

        private void UpdateCardLocations()
        {
            int card_width = Width / 16;
            int center_x = Width / 2;

            int card_incr = card_width + 2;

            int x_loc = center_x - card_width / 4 - card_incr * player_hand_controls.Count / 2;

            foreach (GameControls.GameCard gc in player_hand_controls)
            {
                gc.SetWidth(card_width);

                gc.Location = new Point(
                    x_loc,
                    Height - (int)(gc.Height*1.4));

                x_loc += card_incr;
            }
        }

        private void GameWindow_SizeChanged(object sender, EventArgs e)
        {
            UpdateCardLocations();
        }
    }
}
