namespace Poker
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Poker.Constants;
    using Poker.Enumerations;
    using Poker.Interfaces;
    using Poker.Models;
    using Poker.TestingAlgorithms;

    using Timer = System.Windows.Forms.Timer;

    public partial class PokerGameForm : Form
    {
        #region Variables

        private int currentHighestBet;

        private IParticipant[] players;

        private IDeck deck;

        private ICard[] cardsOnBoard;

        readonly Timer timer = new Timer();

        readonly Timer Updates = new Timer();

        private Point boardCardsPosition = new Point(300, 180);

        private decimal time = 60M;

        private int bigBlind = 500;

        private int smallBlind = 250;

        private int playerOnTurnIndex = 0;

        #endregion

        public PokerGameForm()
        {
            this.InitializeComponent();

            this.InitializePlayers();

            this.InitializeDeck();

            this.cardsOnBoard = new ICard[5];

            this.currentHighestBet = this.bigBlind;

            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.textBoxPot.Enabled = false;
            this.textBoxChips.Enabled = false;
            this.textBoxBotChips1.Enabled = false;
            this.textBoxBotChips2.Enabled = false;
            this.textBoxBotChips3.Enabled = false;
            this.textBoxBotChips4.Enabled = false;
            this.textBoxBotChips5.Enabled = false;
            this.textBoxBigBlind.Visible = false;
            this.textBoxSmallBlind.Visible = false;
            this.buttonBigBlind.Visible = false;
            this.buttoneSmallBlind.Visible = false;

            this.timer.Interval = 200;
            this.timer.Tick += this.TimerTick;
            this.timer.Start();
            this.Updates.Interval = 100;
            this.Updates.Tick += this.UpdateTick;
            this.Updates.Start();

            this.textBoxRaise.Text = (this.bigBlind * 2).ToString();
            this.progressBarTimer.Value = 300;
        }

        private void InitializeDeck()
        {
            this.deck = new Deck();
            foreach (var card in this.deck.Cards)
            {
                this.Controls.Add(card.PictureBox);
            }
        }

        private void InitializePlayers()
        {
            this.players = new Participant[6];
            // Assigning players
            this.players[0] = new Player("Gosho", 1);
            this.players[1] = new Bot("Bot 1", 2);
            this.players[2] = new Bot("Bot 2", 3);
            this.players[3] = new Bot("Bot 3", 4);
            this.players[4] = new Bot("Bot 4", 5);
            this.players[5] = new Bot("Bot 5", 6);

            // Adding Chips display control to each player
            this.players[0].Controls.Add("ChipsBox", this.textBoxChips);
            this.players[1].Controls.Add("ChipsBox", this.textBoxBotChips1);
            this.players[2].Controls.Add("ChipsBox", this.textBoxBotChips2);
            this.players[3].Controls.Add("ChipsBox", this.textBoxBotChips3);
            this.players[4].Controls.Add("ChipsBox", this.textBoxBotChips4);
            this.players[5].Controls.Add("ChipsBox", this.textBoxBotChips5);

            // Adding Status control to each player
            this.players[0].Controls.Add("StatusBox", this.playerStatus);
            this.players[1].Controls.Add("StatusBox", this.bot1Status);
            this.players[2].Controls.Add("StatusBox", this.bot2Status);
            this.players[3].Controls.Add("StatusBox", this.bot3Status);
            this.players[4].Controls.Add("StatusBox", this.bot4Status);
            this.players[5].Controls.Add("StatusBox", this.bot5Status);
        }

        private void BeginRound()
        {
            this.playerOnTurnIndex = 0;
            foreach (var player in this.players)
            {
                player.ResetFlags();
                player.Hand.CurrentCards.ToList().ForEach(c => c.PictureBox.Visible = false);
                player.Hand.CurrentCards.Clear();
            }

            this.deck.Deal(this.players, this.cardsOnBoard);

            int positionCardChangeX = this.boardCardsPosition.X;
            foreach (ICard card in this.cardsOnBoard)
            {
                Point nextLocation = new Point(positionCardChangeX, this.boardCardsPosition.Y);
                positionCardChangeX += 90;
                card.PictureBox.Location = nextLocation;
            }

            if (this.players.Count(p => !(p is Player) && !p.IsInGame) == 5)
            {
                DialogResult dialogResult = MessageBox.Show(
                    "Would You Like To Play Again ?",
                    "You Won , Congratulations ! ",
                    MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Application.Restart();
                }
                else if (dialogResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }
        }

        private void TimerTick(object sender, object e)
        {
            if (this.progressBarTimer.Value <= 0)
            {
                this.players[0].HasFolded = true;
            }

            if (this.time > 0)
            {
                this.time -= 0.2M;
                this.progressBarTimer.Value -= 1;
            }
        }

        private async Task Turns()
        {
            if (this.players.Any(p => p.WinsRound) || this.players.Any(p => p.HasRaised))
            {
                int potSplit = int.Parse(this.textBoxPot.Text) / (this.players.Count(p => p.WinsRound) + 1)
                                + 1;
                foreach (var player in this.players)
                {
                    if (player.WinsRound)
                    {
                        player.Chips += potSplit;
                        MessageBox.Show($"{player.Name} wins round with {player.Hand.Strength}");
                    }
                }

                this.BeginRound();

                if (!this.players[this.playerOnTurnIndex].HasActed)
                {
                    this.players[this.playerOnTurnIndex].PlayTurn();
                }
                else
                {
                    this.playerOnTurnIndex += 1;
                }
            }
        }

        private async void UpdateTick(object sender, object e)
        {
            await this.Turns();
            foreach (var player in this.players)
            {
                player.Controls["ChipsBox"].Text = "Chips: " + player.Chips;
            }
        }

        private void ButtonFoldClick(object sender, EventArgs e)
        {
            this.players[0].HasFolded = true;

            //this.playerStatus.Text = "Fold";
            //this.players[Players.Player].Turn = false;
            //this.players[Players.Player].FoldedTurn = true;
            //await this.Turns();
        }

        private void ButonCheckClick(object sender, EventArgs e)
        {
            this.players[0].HasChecked = true;
            //if (this.call <= 0)
            //{
            //    this.players[Players.Player].Turn = false;
            //    this.playerStatus.Text = "Check";
            //}
            //else
            //{
            //    // playerStatus.Text = "All in " + Chips;
            //    this.bCheck.Enabled = false;
            //}

            //await this.Turns();
        }

        private void ButtonCallClick(object sender, EventArgs e)
        {
            this.players[0].Call(this.currentHighestBet);
        }

        private void ButtonRaiseClick(object sender, EventArgs e)
        {
            this.players[0].Raise(int.Parse(this.textBoxRaise.Text));
        }

        private void ButtonAddClick(object sender, EventArgs e)
        {
            foreach (var player in this.players)
            {
                player.Chips += int.Parse(this.textBoxAdd.Text);
            }
        }

        private void ButtonOptionsClick(object sender, EventArgs e)
        {
            this.textBoxBigBlind.Text = this.bigBlind.ToString();
            this.textBoxSmallBlind.Text = this.smallBlind.ToString();
            if (this.textBoxBigBlind.Visible == false)
            {
                this.textBoxBigBlind.Visible = true;
                this.textBoxSmallBlind.Visible = true;
                this.buttonBigBlind.Visible = true;
                this.buttoneSmallBlind.Visible = true;
            }
            else
            {
                this.textBoxBigBlind.Visible = false;
                this.textBoxSmallBlind.Visible = false;
                this.buttonBigBlind.Visible = false;
                this.buttoneSmallBlind.Visible = false;
            }
        }

        private void ButtonSmallBlindClick(object sender, EventArgs e)
        {
            int parsedValue;
            if (this.textBoxSmallBlind.Text.Contains(",") || this.textBoxSmallBlind.Text.Contains("."))
            {
                MessageBox.Show("The Small Blind can be only round number !");
                this.textBoxSmallBlind.Text = this.smallBlind.ToString();
                return;
            }

            if (!int.TryParse(this.textBoxSmallBlind.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                this.textBoxSmallBlind.Text = this.smallBlind.ToString();
                return;
            }

            if (int.Parse(this.textBoxSmallBlind.Text) > 100000)
            {
                MessageBox.Show("The maximum of the Small Blind is 100 000 $");
                this.textBoxSmallBlind.Text = this.smallBlind.ToString();
            }

            if (int.Parse(this.textBoxSmallBlind.Text) < 250)
            {
                MessageBox.Show("The minimum of the Small Blind is 250 $");
            }

            if (int.Parse(this.textBoxSmallBlind.Text) >= 250 && int.Parse(this.textBoxSmallBlind.Text) <= 100000)
            {
                this.smallBlind = int.Parse(this.textBoxSmallBlind.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }

        private void ButtonBigBlindClick(object sender, EventArgs e)
        {
            int parsedValue;
            if (this.textBoxBigBlind.Text.Contains(",") || this.textBoxBigBlind.Text.Contains("."))
            {
                MessageBox.Show("The Big Blind can be only round number !");
                this.textBoxBigBlind.Text = this.bigBlind.ToString();
                return;
            }

            if (!int.TryParse(this.textBoxSmallBlind.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                this.textBoxSmallBlind.Text = this.bigBlind.ToString();
                return;
            }

            if (int.Parse(this.textBoxBigBlind.Text) > 200000)
            {
                MessageBox.Show("The maximum of the Big Blind is 200 000");
                this.textBoxBigBlind.Text = this.bigBlind.ToString();
            }

            if (int.Parse(this.textBoxBigBlind.Text) < 500)
            {
                MessageBox.Show("The minimum of the Big Blind is 500 $");
            }

            if (int.Parse(this.textBoxBigBlind.Text) >= 500 && int.Parse(this.textBoxBigBlind.Text) <= 200000)
            {
                this.bigBlind = int.Parse(this.textBoxBigBlind.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }

        private void LayoutChange(object sender, LayoutEventArgs e)
        {
        }

        private void PokerGameForm_Load(object sender, EventArgs e)
        {

        }
    }
}