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

        private decimal time = 60M;

        private int bigBlind = 500;

        private int smallBlind = 250;

        private TurnParts CurrentTurnPart;

        #endregion

        public PokerGameForm()
        {
            this.InitializeComponent();

            this.InitializeControls();

            this.InitializePlayers();

            this.InitializeDeck();

            this.cardsOnBoard = new ICard[5];

            this.currentHighestBet = this.bigBlind;

            this.CurrentTurnPart = TurnParts.End;
        }

        private void InitializeControls()
        {
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
            this.DisableUserButtons();

            this.CurrentTurnPart = TurnParts.Flop;

            this.CheckForEndOfGame();

            this.CleanDeckAndPlayerHands();

            this.deck.Deal(this.players, this.cardsOnBoard);

            if (this.players[0].IsInGame)
            {
                this.EnableUserButtons();
            }
        }

        private void PostDealActions()
        {   
            // Display changes to each card
            foreach (var card in this.deck.Cards)
            {
                card.PictureBox.Update();
            }
        }

        private void CleanDeckAndPlayerHands()
        {
            this.textBoxPot.Text = "0";

            foreach (var card in this.deck.Cards)
            {
                card.IsFacingUp = false;
                card.PictureBox.Visible = false;
            }

            foreach (var player in this.players)
            {
                player.ResetFlags();
                player.Hand.CurrentCards.Clear();
            }
        }

        private void CheckForEndOfGame()
        {
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

        private void ProcessRoundWinnings()
        {
            if (this.players.Any(p => p.WinsRound))
            {
                int potSplit = int.Parse(this.textBoxPot.Text) / this.players.Count(p => p.WinsRound);
                if (this.players.Count(p => p.WinsRound) > 1)
                {
                    StringBuilder winnersOutput = new StringBuilder();
                    foreach (var player in this.players.Where(p => p.WinsRound))
                    {
                        player.Chips += potSplit;
                        winnersOutput.Append($"{player.Name}, ");
                    }

                    winnersOutput.Remove(winnersOutput.Length - 3, 2);
                    winnersOutput.Append($"are tied for the pot and split {this.textBoxPot.Text}");

                    MessageBox.Show(winnersOutput.ToString());
                }
                else
                {
                    IParticipant winner = this.players.Where(p => p.WinsRound).ToArray()[0];
                    winner.Chips += int.Parse(this.textBoxPot.Text);

                    MessageBox.Show($"{winner.Name} wins the round with {winner.Hand.Strength}");
                }
            }
        }

        private void Turns()
        {
            if (this.CurrentTurnPart == TurnParts.End && int.Parse(this.textBoxPot.Text) > 0)
            {
                this.ProcessRoundWinnings();

                this.BeginRound();
            }
            else if (this.CurrentTurnPart == TurnParts.End)
            {
                this.BeginRound();
            }

            if (this.players[0].HasActed || !this.players[0].IsInGame)
            {
                this.timer.Stop();
                this.time = 60;
                for (int i = 1; i < this.players.Length; i++)
                {
                    if (!this.players[i].HasActed && this.players[i].IsInGame)
                    {
                        Task showBotOnTurn = new Task(() =>
                                            {
                                                MessageBox.Show($"{this.players[i].Name}'s turn");
                                            });
                        showBotOnTurn.Start();

                        this.players[i].PlayTurn();
                        if (this.players[i].HasRaised)
                        {
                            this.players.Where(p => p != this.players[i]).ToList().ForEach(p => p.ResetFlags());
                        }
                        showBotOnTurn.Wait();
                    }
                }
                foreach (var player in this.players)
                {
                    player.ResetFlags();
                }
                this.timer.Start();
            }
        }

        private void UpdateTick(object sender, object e)
        {
            this.Turns();
            foreach (var player in this.players)
            {
                player.Controls["ChipsBox"].Text = $"{player.Name} Chips: {player.Chips}";
            }

            int potValue = this.players.Sum(player => player.ChipsPlaced);

            this.textBoxPot.Text = potValue.ToString();
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
                this.progressBarTimer.Value = (int)(this.time * 5);
            }
        }

        private void DisableUserButtons()
        {
            this.buttonCall.Enabled = false;
            this.buttonFold.Enabled = false;
            this.buttonCheck.Enabled = false;
            this.buttonRaise.Enabled = false;
        }

        private void EnableUserButtons()
        {
            this.buttonCall.Enabled = true;
            this.buttonFold.Enabled = true;
            this.buttonCheck.Enabled = true;
            this.buttonRaise.Enabled = true;
        }

        private void ButtonFoldClick(object sender, EventArgs e)
        {
            this.players[0].Fold();
            this.DisableUserButtons();
        }

        private void ButonCheckClick(object sender, EventArgs e)
        {
            this.players[0].Check();
        }

        private void ButtonCallClick(object sender, EventArgs e)
        {
            if (this.players[0].Chips > this.currentHighestBet)
            {
                this.players[0].Call(this.currentHighestBet);
            }
            else
            {
                this.players[0].AllIn();
                this.DisableUserButtons();
            }
        }

        private void ButtonRaiseClick(object sender, EventArgs e)
        {
            int raiseValue = 0;
            try
            {
                raiseValue = int.Parse(this.textBoxRaise.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("This is a number only field!");
                this.textBoxRaise.Text = (this.currentHighestBet * 2).ToString();
            }
            if (raiseValue >= this.currentHighestBet * 2)
            {
                if (this.players[0].Chips > raiseValue)
                {
                    this.players[0].Raise(int.Parse(this.textBoxRaise.Text));
                    this.currentHighestBet = int.Parse(this.textBoxRaise.Text);
                }
                else
                {
                    this.players[0].AllIn();
                    this.DisableUserButtons();
                }
            }
            else
            {
                MessageBox.Show("Raise amount must be twice as big as the current highest bet!");
                this.textBoxRaise.Text = (this.currentHighestBet * 2).ToString();
            }
        }

        private void ButtonAddClick(object sender, EventArgs e)
        {
            int addAmount = 0;

            try
            {
                addAmount = int.Parse(this.textBoxAdd.Text);
                if (addAmount > 1000000)
                {
                    addAmount = 0;
                    throw new OverflowException();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Add chips field is numbers only!");
            }
            catch (OverflowException)
            {
                MessageBox.Show("The maximum add amount is 1 000 000");
                this.textBoxAdd.Text = "1000000";
            }

            foreach (var player in this.players)
            {
                player.Chips += addAmount;
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