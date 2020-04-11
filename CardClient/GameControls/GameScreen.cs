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
using GameLibrary.Messages;

namespace CardClient.GameControls
{
    public partial class GameScreen : UserControl
    {
        List<Hand> player_hands;

        List<List<GameCard>> player_hand_controls;

        const int num_players = 4;
        int player_position = -1;
        int current_player = -1;

        int game_id = -1;

        public GameScreen()
        {
            InitializeComponent();

            // Setup the player hands
            player_hands = new List<Hand>();
            for (int i = 0; i < num_players; ++i)
            {
                player_hands.Add(new Hand());
            }

            // Define the player hand controls
            player_hand_controls = new List<List<GameCard>>();

            // Update the hands and the card locations
            UpdateHands();
        }

        public void SetGameID(int game_id)
        {
            this.game_id = game_id;
        }

        public void UpdateFromStatus(MsgGameStatus status)
        {
            if (status.game_id != game_id)
            {
                return;
            }

            player_hands = status.hands;

            for (int i = 0; i < status.players.Count; ++i)
            {
                if (status.players[i].Equals(Network.GameComms.GetPlayer()))
                {
                    player_position = i;
                    break;
                }
            }

            current_player = status.current_player;

            UpdateHands();
            UpdateCardLocations();
        }


        public void UpdateHands()
        {
            // Don't update if the current player is undefined
            if (player_position < 0) return;

            // Define a boolean for a card update needed
            bool update_needed = false;

            for (int i = 0; i < player_hands.Count; ++i)
            {
                // Add the player lists if required
                if (player_hand_controls.Count <= i)
                {
                    player_hand_controls.Add(new List<GameCard>());
                    update_needed = true;
                }

                // Add the cards as required
                for (int j = 0; j < player_hands[i].cards.Count; ++j)
                {
                    Card card = player_hands[i].cards[j];

                    GameCard game_card;

                    if (j < player_hand_controls[i].Count)
                    {
                        game_card = player_hand_controls[i][j];

                        if (game_card.base_card != card)
                        {
                            game_card.SetCard(c: card);
                            update_needed = true;
                        }
                    }
                    else
                    {
                        game_card = new GameCard(card: card);
                        Controls.Add(game_card);
                        player_hand_controls[i].Add(game_card);
                        game_card.Click += onGameCardClick;
                        update_needed = true;
                    }

                    game_card.SetFaceShown(i == player_position);
                }

                while (player_hand_controls[i].Count > player_hands[i].cards.Count)
                {
                    int end_i = player_hand_controls[i].Count - 1;
                    GameCard gc = player_hand_controls[i][end_i];
                    Controls.Remove(gc);
                    player_hand_controls[i].RemoveAt(end_i);
                    update_needed = true;
                }
            }

            if (update_needed)
            {
                UpdateCardLocations();
            }
        }

        public void onGameCardClick(object sender, EventArgs e)
        {
            if (sender is GameCard)
            {
                GameCard gc = (GameCard)sender;
                Console.WriteLine(string.Format(
                    "Card {0:s} clicked",
                    gc.base_card.ToString()));

                for (int i = 0; i < player_hands[player_position].cards.Count; ++i)
                {
                    if (gc.base_card == player_hands[player_position].cards[i])
                    {
                        Network.GameComms.SendMessage(new MsgGamePlay()
                        {
                            action = GameActions.CardPlay,
                            card = gc.base_card,
                            game_id = 0
                        });

                        //player_hands[player_position].cards.RemoveAt(i);
                        UpdateHands();
                        break;
                    }
                }
            }
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
            if (player_position < 0) return;

            int card_width = Width / 16;
            int center_x = Width / 2;

            UpdateCardHorizontal(
                player_position,
                is_top: false);
            UpdateCardHorizontal(
                (player_position + 2) % player_hand_controls.Count,
                is_top: true);

            foreach (GameCard gc in player_hand_controls[(player_position + 1) % player_hand_controls.Count])
            {
                gc.SetWidth(card_width);
                gc.Location = new Point(
                    (int)(0.1 * card_width),
                    Height / 2 - gc.Height / 2);
            }

            foreach (GameCard gc in player_hand_controls[(player_position + 3) % player_hand_controls.Count])
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
