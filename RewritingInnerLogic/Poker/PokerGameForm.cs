using Poker.Globals;

namespace Poker
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Poker.Enumerations;
    using Poker.Interfaces;
    using Poker.Models.Cards;
    using Poker.Models.Entities;
    using Poker.PowerCalculator;

    using Timer = System.Windows.Forms.Timer;

    public partial class PokerGameForm : Form
    {
        private readonly ICard[] cardsOnBoard;

        readonly Timer timer = new Timer();

        readonly Timer Updates = new Timer();

        private IList<IParticipant> players;

        private IDeck deck;

        public PokerGameForm()
        {
            this.InitializeComponent();

            this.InitializeControls();

            this.InitializePlayers();

            this.InitializeDeck();

            this.cardsOnBoard = new ICard[5];

            GlobalVariables.CurrentHighestBet = GlobalConstants.StartingBigBlind;
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

            this.textBoxRaise.Text = (GlobalVariables.BigBlind * 2).ToString();
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
            this.players[0] = new Player("Player", GlobalVariables.PlayerPlaceOnBoard);
            this.players[1] = new Bot("Bot 1", GlobalVariables.Bot1PlaceOnBoard);
            this.players[2] = new Bot("Bot 2", GlobalVariables.Bot2PlaceOnBoard);
            this.players[3] = new Bot("Bot 3", GlobalVariables.Bot3PlaceOnBoard);
            this.players[4] = new Bot("Bot 4", GlobalVariables.Bot4PlaceOnBoard);
            this.players[5] = new Bot("Bot 5", GlobalVariables.Bot5PlaceOnBoard);

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
            GlobalVariables.CurrentHighestBet = GlobalVariables.BigBlind;

            GlobalVariables.TimeForPlayerTurn = 60M;

            GlobalVariables.CurrentTurnPart = TurnParts.Flop;

            this.DisableUserButtons();

            this.UpdateTextBoxes();

            this.timer.Stop();

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

        private void UpdateTextBoxes()
        {
            foreach (var player in this.players)
            {
                player.Controls["ChipsBox"].Text = $"{player.Name} Chips: {player.Chips}";
                player.Controls["ChipsBox"].Update();
                player.Controls["StatusBox"].Text = string.Empty;
                player.Controls["StatusBox"].Update();
            }

            this.textBoxPot.Text = "0";
            this.textBoxPot.Update();
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
            foreach (var player in this.players)
            {
                player.SetFlagsForNewTurn();
                player.ChipsPlaced = 0;
                player.Hand.CurrentCards.Clear();
            }

            foreach (var card in this.deck.Cards)
            {
                card.IsFacingUp = false;
                card.PictureBox.Visible = false;
                card.PictureBox.Update();
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

                    winnersOutput.Remove(winnersOutput.Length - 2, 2);
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
            if (GlobalVariables.CurrentTurnPart == TurnParts.BeginGame)
            {
                this.BeginRound();
            }
            if (GlobalVariables.CurrentTurnPart == TurnParts.End && int.Parse(this.textBoxPot.Text) > 0)
            {
                CardPowerCalculator.CompareAllSetsOfCardsOnTheBoard
                    (this.players.Where(p => !p.HasFolded 
                     || p.IsAllIn).ToArray());
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
            else if (this.players.Count == this.players.Count(p => p.HasFolded || !p.IsInGame || p.IsAllIn))
            {
                CardPowerCalculator.CompareAllSetsOfCardsOnTheBoard
                    (this.players.Where(p => !p.HasFolded
                     || p.IsAllIn).ToArray());

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

            if (this.players[0].HasActed || !this.players[0].IsInGame)
            {
                this.timer.Stop();
                GlobalVariables.TimeForPlayerTurn = 60M;
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
                                             p => p.HasRaised 
                                            || p.HasCalled 
                                            || p.IsAllIn 
                                            || GlobalVariables.CurrentTurnPart == TurnParts.Flop);
                        if (this.players[i] is IBot)
                        {
                            ((IBot)this.players[i]).PlayTurn(
                                ref GlobalVariables.CurrentHighestBet,
                                this.players.Count(p => !p.HasFolded),
                                !anyoneActedNoCheck,
                                GlobalVariables.CurrentTurnPart,
                                GlobalVariables.RandomBehaviorForBots);
                        }

                        if (this.players[i].HasRaised)
                        {
                            for (int j = 0; j < this.players.Count; j++)
                            {
                                if (j != i)
                                {
                                    this.players[j].ResetFlags();
                                }                           
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
                        this.textBoxPot.Update();

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
                    GlobalVariables.CurrentTurnPart++;

                    if (GlobalVariables.CurrentTurnPart == TurnParts.Turn)
                    {
                        foreach (var player in this.players)
                        {
                            player.PreviouslyCalled = 0;
                            this.cardsOnBoard[3].IsFacingUp = true;
                            this.cardsOnBoard[3].PictureBox.Update();

                            player.Hand.CurrentCards.Add(this.cardsOnBoard[3]);
                        }
                    }
                    else if (GlobalVariables.CurrentTurnPart == TurnParts.River)
                    {
                        foreach (var player in this.players)
                        {
                            this.cardsOnBoard[4].IsFacingUp = true;
                            this.cardsOnBoard[4].PictureBox.Update();

                            player.Hand.CurrentCards.Add(this.cardsOnBoard[4]);
                        }
                        GlobalVariables.CurrentTurnPart++;
                    }
                }             
            }
        }

        private void UpdateTick(object sender, object e)
        {
            Debug.WriteLine(GlobalVariables.CurrentHighestBet);
            Debug.WriteLine(GlobalVariables.CurrentTurnPart);
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

            if (GlobalVariables.TimeForPlayerTurn > 0)
            {
                GlobalVariables.TimeForPlayerTurn -= 0.2M;
                this.progressBarTimer.Value = (int)(GlobalVariables.TimeForPlayerTurn * 5);
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
            if (this.players[0].Chips > GlobalVariables.CurrentHighestBet)
            {
                this.players[0].Call(GlobalVariables.CurrentHighestBet);
            }
            else
            {
                this.players[0].AllIn(ref GlobalVariables.CurrentHighestBet);
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
                this.textBoxRaise.Text = (GlobalVariables.CurrentHighestBet * 2).ToString();
            }
            if (raiseValue >= GlobalVariables.CurrentHighestBet * 2)
            {
                if (this.players[0].Chips > raiseValue)
                {
                    this.players[0].Raise(int.Parse(this.textBoxRaise.Text),
                                                    ref GlobalVariables.CurrentHighestBet);
                }
                else
                {
                    this.players[0].AllIn(ref GlobalVariables.CurrentHighestBet);
                    this.DisableUserButtons();
                }
            }
            else
            {
                MessageBox.Show("Raise amount must be twice as big as the current highest bet!");
                this.textBoxRaise.Text = (GlobalVariables.CurrentHighestBet * 2).ToString();
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
            this.textBoxBigBlind.Text = GlobalVariables.BigBlind.ToString();
            this.textBoxSmallBlind.Text = GlobalVariables.SmallBlind.ToString();
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
                this.textBoxSmallBlind.Text = GlobalVariables.SmallBlind.ToString();
                return;
            }

            if (!int.TryParse(this.textBoxSmallBlind.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                this.textBoxSmallBlind.Text = GlobalVariables.SmallBlind.ToString();
                return;
            }

            if (int.Parse(this.textBoxSmallBlind.Text) > 100000)
            {
                MessageBox.Show("The maximum of the Small Blind is 100 000 $");
                this.textBoxSmallBlind.Text = GlobalVariables.SmallBlind.ToString();
            }

            if (int.Parse(this.textBoxSmallBlind.Text) < 250)
            {
                MessageBox.Show("The minimum of the Small Blind is 250 $");
            }

            if (int.Parse(this.textBoxSmallBlind.Text) >= 250 && int.Parse(this.textBoxSmallBlind.Text) <= 100000)
            {
                GlobalVariables.SmallBlind = int.Parse(this.textBoxSmallBlind.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }

        private void ButtonBigBlindClick(object sender, EventArgs e)
        {
            int parsedValue;
            if (this.textBoxBigBlind.Text.Contains(",") || this.textBoxBigBlind.Text.Contains("."))
            {
                MessageBox.Show("The Big Blind can be only round number !");
                this.textBoxBigBlind.Text = GlobalVariables.BigBlind.ToString();
                return;
            }

            if (!int.TryParse(this.textBoxSmallBlind.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                this.textBoxSmallBlind.Text = GlobalVariables.BigBlind.ToString();
                return;
            }

            if (int.Parse(this.textBoxBigBlind.Text) > 200000)
            {
                MessageBox.Show("The maximum of the Big Blind is 200 000");
                this.textBoxBigBlind.Text = GlobalVariables.BigBlind.ToString();
            }

            if (int.Parse(this.textBoxBigBlind.Text) < 500)
            {
                MessageBox.Show("The minimum of the Big Blind is 500 $");
            }

            if (int.Parse(this.textBoxBigBlind.Text) >= 500 && int.Parse(this.textBoxBigBlind.Text) <= 200000)
            {
                GlobalVariables.BigBlind = int.Parse(this.textBoxBigBlind.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }

        private void LayoutChange(object sender, LayoutEventArgs e)
        {
            if (this.players != null)
            {
                if (this.players[0].Chips <= GlobalVariables.CurrentHighestBet * 2)
                {
                    this.buttonRaise.Text = "All In";
                   // this.buttonRaise.Update();
                }
                else
                {
                    this.buttonRaise.Text = "Raise";
                }
            }
        }

        private void PokerGameForm_Load(object sender, EventArgs e)
        {
        }
    }
}