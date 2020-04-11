using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GameLibrary.Cards;

namespace CardClient.GameControls
{
    public partial class GameScreen : UserControl
    {
        List<Hand> player_hands = new List<Hand>()
        {
            new Hand(),
            new Hand(),
            new Hand(),
            new Hand()
        };

        List<List<GameCard>> player_hand_controls;

        int current_player = 0;

        public GameScreen()
        {
            InitializeComponent();

            Deck deck = new Deck();
            deck.Shuffle();

            player_hand_controls = new List<List<GameCard>>();

            for (int i = 0; i < player_hands.Count; ++i)
            {
                player_hand_controls.Add(new List<GameCard>());

                for (int j = 0; j < 13; ++j)
                {
                    Card card = deck.Next();

                    player_hands[i].AddCard(card);

                    GameCard game_card = new GameCard(card: card);

                    game_card.SetFaceShown(i == current_player);

                    player_hand_controls[i].Add(game_card);
                    Controls.Add(game_card);
                }
            }

            UpdateCardLocations();
        }

        private int CardWidth()
        {
            return Width / 16;
        }

        private void UpdateCardHorizontal(
            int player_loc,
            bool is_top)
        {
            List<GameCard> cards = player_hand_controls[player_loc];

            int card_incr = CardWidth() + 2;
            int x_loc = Width / 2 - card_incr * cards.Count / 2;

            foreach (GameCard gc in player_hand_controls[player_loc])
            {
                gc.SetWidth(CardWidth());

                int y_loc = (is_top) ? (int)(gc.Height * 0.1) : (Height - (int)(gc.Height * 1.1));

                gc.Location = new Point(
                    x_loc,
                    y_loc);

                x_loc += card_incr;
            }
        }

        private void UpdateCardLocations()
        {
            int card_width = Width / 16;
            int center_x = Width / 2;

            UpdateCardHorizontal(
                current_player,
                is_top: false);
            UpdateCardHorizontal(
                (current_player + 2) % player_hand_controls.Count,
                is_top: true);

            foreach (GameCard gc in player_hand_controls[(current_player + 1) % player_hand_controls.Count])
            {
                gc.SetWidth(card_width);
                gc.Location = new Point(
                    (int)(0.1 * card_width),
                    Height / 2 - gc.Height / 2);
            }

            foreach (GameCard gc in player_hand_controls[(current_player + 3) % player_hand_controls.Count])
            {
                gc.SetWidth(card_width);
                gc.Location = new Point(
                    Width - (int)(1.1 * card_width),
                    Height / 2 - gc.Height / 2);
            }
        }

        private void GameScreen_SizeChanged(object sender, EventArgs e)
        {
            UpdateCardLocations();
        }
    }
}
