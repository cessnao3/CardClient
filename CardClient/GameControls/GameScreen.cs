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
using GameLibrary.Games;
using GameLibrary.Messages;

namespace CardClient.GameControls
{
    public partial class GameScreen : UserControl
    {
        List<Hand> player_hands;

        List<GamePlayer> players;
        List<List<GameCard>> player_hand_controls = new List<List<GameCard>>();
        List<GameCard> center_pool_cards = new List<GameCard>();
        List<Label> player_labels = new List<Label>();

        const int num_players = 4;
        int player_position = -1;
        int current_player_turn = -1;

        int game_id = -1;

        public GameScreen()
        {
            InitializeComponent();

            // Setup the player hands and other per-player items
            player_hands = new List<Hand>();
            for (int i = 0; i < num_players; ++i)
            {
                // Define the hands
                player_hands.Add(new Hand());

                // Setup the center-pool cards
                GameCard center_card = new GameCard();
                center_card.SetFaceShown(true);
                center_pool_cards.Add(center_card);
                Controls.Add(center_card);

                // Setup the player labels
                Label l = new Label()
                {
                    Text = string.Empty,
                    BackColor = Color.PaleGreen,
                    Font = new Font(FontFamily.GenericMonospace, 9),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Visible = false
                };
                player_labels.Add(l);
                Controls.Add(l);
            }

            // Update the hands and the card locations
            UpdateHands();
        }

        /// <summary>
        /// Defines the game ID for the current game window
        /// </summary>
        /// <param name="game_id">The Game ID to apply to the current game window</param>
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

            for (int i = 0; i < status.center_pool.Count; ++i)
            {
                center_pool_cards[i].SetCard(c: status.center_pool[i]);
            }

            players = status.players;

            current_player_turn = status.current_player;

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
                            card = gc.base_card,
                            game_id = 0,
                            player = players[player_position]
                        });
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

        private int CardHeight()
        {
            return (int)(CardWidth() * GameCard.HeightRatio());
        }

        private string PlayerString(int player_loc)
        {
            GamePlayer p = players[player_loc];
            return string.Format(
                "{0:}{1:} {2:  0}",
                p.CapitalizedName().Substring(0, Math.Min(3, p.name.Length)),
                (current_player_turn == player_loc) ? "*" : " ",
                0);
        }

        private Size LabelSize()
        {
            return new Size((int)(1.1 * CardWidth()), 18);
        }

        private void UpdateCardHorizontal(
            int player_loc,
            bool is_top)
        {
            List<GameCard> cards = player_hand_controls[player_loc];

            int card_incr = CardWidth() + 2;
            int x_center = Width / 2;
            int x_loc = x_center - card_incr * cards.Count / 2;

            int y_loc;
            if (is_top)
            {
                y_loc = (int)(CardHeight() * 0.1);
            }
            else
            {
                y_loc = (int)(Height - CardHeight() * 1.1);
            }

            Label l = player_labels[player_loc];
            l.Size = LabelSize();
            l.Location = new Point(
                x_loc,
                y_loc + (int)(is_top ? (1.1 * CardHeight()) : (-0.1 * CardHeight() - l.Height)));
            l.Text = PlayerString(player_loc);
            l.Visible = true;

            foreach (GameCard gc in player_hand_controls[player_loc])
            {
                gc.SetWidth(CardWidth());
                gc.Location = new Point(
                    x_loc,
                    y_loc);
                x_loc += card_incr;
            }

            // Increment a modified card length for the center pool / played cards
            y_loc += (int)((is_top ? 1 : -1) * CardHeight() * 1.1);

            GameCard pool_card = center_pool_cards[player_loc];
            pool_card.SetWidth(CardWidth());
            pool_card.Location = new Point(
                x_center,
                y_loc);
        }

        private void UpdateCardVertical(
            int player_loc,
            bool is_left)
        {
            List<GameCard> cards = player_hand_controls[player_loc];

            int y_center = Height / 2 - (int) (CardHeight() / 2.0);

            int card_incr = Height / 2 / cards.Count;
            int y_loc = y_center - card_incr * cards.Count / 2;

            int x_loc;
            if (is_left)
            {
                x_loc = (int)(0.1 * CardWidth());
            }
            else
            {
                x_loc = (int)(Width - 1.1 * CardWidth());
            }

            Label l = player_labels[player_loc];
            l.Size = LabelSize();
            l.Location = new Point(
                is_left ? x_loc : Width - l.Width,
                y_loc - l.Height - (int)(0.1 * CardHeight()));
            l.Text = PlayerString(player_loc);
            l.Visible = true;

            foreach (GameCard gc in cards)
            {
                gc.SetWidth(CardWidth());
                gc.Location = new Point(
                    x_loc,
                    y_loc);
                y_loc += card_incr;
            }

            x_loc += (is_left ? 1 : -1) * (int)(CardWidth() * 1.1);

            GameCard pool_card = center_pool_cards[player_loc];
            pool_card.SetWidth(CardWidth());
            pool_card.Location = new Point(
                x_loc,
                y_center);
        }

        private void UpdateCardLocations()
        {
            if (player_position < 0) return;

            UpdateCardHorizontal(
                player_position,
                is_top: false);
            UpdateCardHorizontal(
                (player_position + 2) % player_hand_controls.Count,
                is_top: true);

            UpdateCardVertical(
                (player_position + 1) % player_hand_controls.Count,
                is_left: true);

            UpdateCardVertical(
                (player_position + 3) % player_hand_controls.Count,
                is_left: false);
        }

        private void GameScreen_SizeChanged(object sender, EventArgs e)
        {
            UpdateCardLocations();
        }
    }
}
