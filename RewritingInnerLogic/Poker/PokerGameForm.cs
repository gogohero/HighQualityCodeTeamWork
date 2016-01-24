namespace Poker
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Poker.Constants;
    using Poker.Enumerations;
    using Poker.Interfaces;
    using Poker.Models;
    using Poker.TestingAlgorithms;

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

        private int currentPlayerIndex = 0;

        #endregion

        public PokerGameForm()
        {
            this.InitializeComponent();

            this.InitializePlayers();

            this.deck = new Deck();

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

            this.timer.Interval = 500;
            this.timer.Tick += this.TimerTick;
            this.timer.Start();
            this.Updates.Interval = 100;
            this.Updates.Tick += this.UpdateTick;
            this.Updates.Start();

            this.textBoxRaise.Text = (this.bigBlind * 2).ToString();
            this.progressBarTimer.Maximum = 120;
        }

        private void InitializePlayers()
        {
            this.players = new Participant[6];
            // Assigning players
            this.players[0] = new Player("Gosho");
            this.players[1] = new Bot("Bot 1");
            this.players[2] = new Bot("Bot 2");
            this.players[3] = new Bot("Bot 3");
            this.players[4] = new Bot("Bot 4");
            this.players[5] = new Bot("Bot 5");

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

        private void EndRound()
        {
            this.players.ToList().ForEach(p => p.Hand.CurrentCards.Clear());
            this.deck.Deal(this.players, this.cardsOnBoard);

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

        private void Turn()
        {
            foreach (var player in this.players)
            {
                player.Controls["ChipsBox"].Text = string.Format($"{player.Name} Chips: {player.Chips}");
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
                this.time -= 0.5M;
                this.progressBarTimer.Value = (int)(2*this.time);
            }
        }

        private void UpdateTick(object sender, object e)
        {
            foreach (var player in this.players)
            {
                player.Hand.CurrentCards.ToList().Where(h => h != null).ToList().ForEach(c => this.Controls.Add(c.PictureBox));
            }
        }

        private void ButtonFoldClick(object sender, EventArgs e)
        {
            //this.playerStatus.Text = "Fold";
            //this.players[Players.Player].Turn = false;
            //this.players[Players.Player].FoldedTurn = true;
            //await this.Turns();
        }

        private void ButonCheckClick(object sender, EventArgs e)
        {
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
            //this.Rules(0, 1, "Player", this.players[Players.Player]);
            //if (this.players[Players.Player].CurrentChips >= this.call)
            //{
            //    this.players[Players.Player].CurrentChips -= this.call;
            //    this.textBoxChips.Text = "Chips : " + this.players[Players.Player].CurrentChips.ToString();
            //    if (this.textBoxPot.Text != string.Empty)
            //    {
            //        this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + this.call).ToString();
            //    }
            //    else
            //    {
            //        this.textBoxPot.Text = this.call.ToString();
            //    }

            //    this.players[Players.Player].Turn = false;
            //    this.playerStatus.Text = "Call " + this.call;
            //    this.players[Players.Player].Call = this.call;
            //}
            //else if (this.players[Players.Player].CurrentChips <= this.call && this.call > 0)
            //{
            //    this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + this.players[Players.Player].CurrentChips).ToString();
            //    this.playerStatus.Text = "All in " + this.players[Players.Player].CurrentChips;
            //    this.players[Players.Player].CurrentChips = 0;
            //    this.textBoxChips.Text = "Chips : " + this.players[Players.Player].CurrentChips.ToString();
            //    this.players[Players.Player].Turn = false;
            //    this.buttonFold.Enabled = false;
            //    this.players[Players.Player].Call = this.players[Players.Player].CurrentChips;
            //}

            //await this.Turns();
        }

        private void ButtonRaiseClick(object sender, EventArgs e)
        {
            
        }

        private void ButtonAddClick(object sender, EventArgs e)
        {
           
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