namespace Poker
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
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

        private Random randomBehavior = new Random();

        private int currentHighestBet;

        private IList<IParticipant> players;

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

            this.CurrentTurnPart = TurnParts.BeginGame;
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
            this.buttonSmallBlind.Visible = false;

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
            this.players[0] = new Player("Player", GlobalConstants.PlayerPlaceOnBoard);
            this.players[1] = new Bot("Bot 1", GlobalConstants.Bot1PlaceOnBoard);
            this.players[2] = new Bot("Bot 2", GlobalConstants.Bot2PlaceOnBoard);
            this.players[3] = new Bot("Bot 3", GlobalConstants.Bot3PlaceOnBoard);
            this.players[4] = new Bot("Bot 4", GlobalConstants.Bot4PlaceOnBoard);
            this.players[5] = new Bot("Bot 5", GlobalConstants.Bot5PlaceOnBoard);

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

            this.currentHighestBet = this.bigBlind;

            this.timer.Stop();
            this.time = 60;

            this.CurrentTurnPart = TurnParts.Flop;

            this.currentHighestBet = this.bigBlind;

            this.CheckForEndOfGame();

            this.CleanDeckAndPlayerHands();

            this.deck.Deal(this.players, this.cardsOnBoard);

            this.timer.Start();

            if (this.players[0].IsInGame)
            {
                this.EnableUserButtons();
            }

            this.PostDealActions();
        }

        private void PostDealActions()
        {   
            // Display changes to each card to ensure correctness
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
                card.PictureBox.Update();
            }

            foreach (var player in this.players)
            {
                player.SetFlagsForNewTurn();
                player.ChipsPlaced = 0;
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

                    Task showWinners = new Task(() => MessageBox.Show(winnersOutput.ToString()));
                    showWinners.Start();
                    showWinners.Wait();
                }
                else
                {
                    int winnerIndex = 0;
                    for (int i = 0; i < this.players.Count; i++)
                    {
                        if (this.players[i].WinsRound)
                        {
                            winnerIndex = i;
                        }
                    }
                    this.players[winnerIndex].Chips += potSplit;
                    Task showWinner =
                        new Task(() => MessageBox.Show($"{this.players[winnerIndex].Name} wins the round with {this.players[winnerIndex].Hand.Strength}"));
                    showWinner.Start();
                    showWinner.Wait();
                }
            }        
        }

        private void Turns()
        {
            if (this.CurrentTurnPart == TurnParts.BeginGame)
            {
                this.BeginRound();
            }
            if (this.CurrentTurnPart == TurnParts.End && int.Parse(this.textBoxPot.Text) > 0)
            {
                CardPowerCalculator.CompareAllSetsOfCardsOnTheBoard(this.players.Where(p => !p.HasFolded && p.IsInGame).ToArray());
                foreach (var player in this.players)
                {
                    foreach (var currentCard in player.Hand.CurrentCards)
                    {
                        currentCard.IsFacingUp = true;
                        currentCard.PictureBox.Update();
                    }
                }
                this.ProcessRoundWinnings();

                this.BeginRound();
            }
            else if (this.players.Count(p => p.HasFolded) ==
                5 - this.players.Count(p => !p.IsInGame || p.IsAllIn))
            {
                int pot = int.Parse(this.textBoxPot.Text);
                this.players.First(p => !p.HasFolded && p.IsInGame).Chips += pot;
                Task showWinner =
                        new Task(() => MessageBox.Show($"{this.players.First(p => !p.HasFolded && p.IsInGame).Name} wins the round and takes {pot} chips"));
                showWinner.Start();
                showWinner.Wait();

                this.BeginRound();
            }

            if (this.players[0].HasActed)
            {
                this.timer.Stop();
                this.time = 60;
                for (int i = 1; i < this.players.Count; i++)
                {
                    if (!this.players[i].HasActed && this.players[i].IsInGame)
                    {
                        Task showBotOnTurn = new Task(
                            () =>
                                {
                                    MessageBox.Show($"{this.players[i].Name}'s turn");
                                });
                        showBotOnTurn.Start();

                        bool anyoneActedNoCheck =
                            this.players.Any(
                                p => p.HasRaised || p.HasCalled || p.IsAllIn || this.CurrentTurnPart == TurnParts.Flop);

                        this.players[i].PlayTurn(
                            ref this.currentHighestBet,
                            this.players.Count(p => !p.HasFolded),
                            !anyoneActedNoCheck,
                            this.CurrentTurnPart,
                            this.randomBehavior);
                        if (this.players[i].HasRaised)
                        {
                            for (int j = i - 1; j >= 0; j--)
                            {
                                this.players[j].ResetFlags();
                            }
                            foreach (var player in this.players.Where(p => p.HasChecked))
                            {
                                player.ResetFlags();
                            }
                        }
                        else if (this.players[i].HasCalled)
                        {
                            foreach (var player in this.players.Where(p => p.HasChecked))
                            {
                                player.ResetFlags();
                            }
                        }

                        int potValue = this.players.Sum(player => player.ChipsPlaced);

                        this.textBoxPot.Text = potValue.ToString();

                        showBotOnTurn.Wait();
                    }
                    this.timer.Start();
                }
                if(this.players.ToList().TrueForAll(p => p.HasActed))
                {
                    foreach (var player in this.players)
                    {
                        player.ResetFlags();
                    }
                    this.CurrentTurnPart++;

                    if (this.CurrentTurnPart == TurnParts.Turn)
                    {
                        foreach (var player in this.players)
                        {
                            this.cardsOnBoard[3].IsFacingUp = true;
                            this.cardsOnBoard[3].PictureBox.Update();

                            player.Hand.CurrentCards.Add(this.cardsOnBoard[3]);
                        }
                    }
                    else if (this.CurrentTurnPart == TurnParts.River)
                    {
                        foreach (var player in this.players)
                        {
                            this.cardsOnBoard[4].IsFacingUp = true;
                            this.cardsOnBoard[4].PictureBox.Update();

                            player.Hand.CurrentCards.Add(this.cardsOnBoard[4]);
                        }
                        this.CurrentTurnPart++;
                    }
                }             
            }
        }

        private void UpdateTick(object sender, object e)
        {
            Debug.WriteLine(this.currentHighestBet);
            Debug.WriteLine(this.CurrentTurnPart);
            this.Turns();

            foreach (var player in this.players)
            {
                player.Controls["ChipsBox"].Text = $"{player.Name} Chips: {player.Chips}";
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
                this.progressBarTimer.Value = (int)(this.time * 5);
            }
        }

        // enable and disable UI buttons
        // -----------------------------
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

        // button methods/events added by default from win forms
        // -----------------------------------------------------
        private void ButtonFoldClick(object sender, EventArgs e)
        {
            this.players[0].Fold();
            this.DisableUserButtons();
        }

        private void ButtonCheckClick(object sender, EventArgs e)
        {
            this.players[0].Check();
        }

        private void ButtonCallClick(object sender, EventArgs e)
        {
            if (this.players[0].Chips > this.currentHighestBet)
            {
                this.players[0].Call(ref this.currentHighestBet);
            }
            else
            {
                this.players[0].AllIn(ref this.currentHighestBet);
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
                    this.players[0].Raise(int.Parse(this.textBoxRaise.Text),
                                                    ref this.currentHighestBet);
                }
                else
                {
                    this.players[0].AllIn(ref this.currentHighestBet);
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
                this.buttonSmallBlind.Visible = true;
            }
            else
            {
                this.textBoxBigBlind.Visible = false;
                this.textBoxSmallBlind.Visible = false;
                this.buttonBigBlind.Visible = false;
                this.buttonSmallBlind.Visible = false;
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