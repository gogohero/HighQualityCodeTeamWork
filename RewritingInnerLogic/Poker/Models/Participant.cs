using System.Drawing;

namespace Poker
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Poker.Constants;
    using Poker.Enumerations;
    using Poker.Interfaces;
    using Poker.TestingAlgorithms;

    public abstract class Participant : IParticipant
    {
        private int StartingChips = GlobalConstants.StartingChips;

        private int chipsPlaced;

        protected Participant(string name, int placeOnBoard)
        {
            this.Chips = StartingChips;
            this.Name = name;
            this.Controls = new Dictionary<string, Control>();
            this.Hand = new Hand();

            this.SetupBoardPlace(placeOnBoard);
        }

        public Dictionary<string, Control> Controls { get; set; }

        public string Name { get; set; }

        public int Chips { get; set; }

        public IHand Hand { get; set; }

        public Point PlaceOnBoard { get; set; }

        public bool IsAllIn { get; set; }

        public bool HasActed
        {
            get
            {
                return this.HasCalled || this.HasChecked || this.HasRaised || this.HasFolded || this.IsAllIn;
            }
        }

        public bool HasFolded { get; set; }

        public bool HasChecked { get; set; }

        public bool HasRaised { get; set; }

        public bool HasCalled { get; set; }

        public bool WinsRound { get; set; }

        public bool IsInGame
        {
            get
            {
                return this.Chips > 0;
            }
        }

        public int ChipsPlaced
        {
            get
            {
                return this.chipsPlaced;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Chips placed value cannot be negative");
                }

                this.chipsPlaced = value;
            }
        }

        public virtual void Call(ref int currentHighestBet)
        {
            this.Chips -= currentHighestBet;
            this.ChipsPlaced += currentHighestBet;
            this.HasCalled = true;
            this.Controls["StatusBox"].Text = "Called: " + currentHighestBet;
            this.Controls["ChipsBox"].Text = this.Chips.ToString();
        }

        public virtual void Raise(int raiseAmount, ref int currentHighestBet)
        {
            if (raiseAmount > currentHighestBet)
            {
                currentHighestBet = raiseAmount;
            }
            this.Chips -= currentHighestBet;
            this.ChipsPlaced += currentHighestBet;
            this.HasRaised = true;
            this.Controls["StatusBox"].Text = "Raised: " + raiseAmount;
            this.Controls["ChipsBox"].Text = this.Chips.ToString();
        }

        public void Fold()
        {
            this.HasFolded = true;
            this.Hand.CurrentCards[0].PictureBox.Visible = false;
            this.Hand.CurrentCards[0].PictureBox.Update();
            this.Hand.CurrentCards[1].PictureBox.Visible = false;
            this.Hand.CurrentCards[1].PictureBox.Update();
            this.Controls["StatusBox"].Text = "Folded";
        }

        public virtual void AllIn(ref int currentHighestBet)
        {
            if (this.Chips > currentHighestBet)
            {
                currentHighestBet = this.Chips;
            }
            this.Controls["StatusBox"].Text = "ALL IN!";
            this.IsAllIn = true;
            this.ChipsPlaced += this.Chips;
            this.Chips = 0;
            this.Controls["ChipsBox"].Text = this.Chips.ToString();
        }

        public virtual void Check()
        {
            this.HasChecked = true;
            this.Controls["StatusBox"].Text = "Checked";
        }

        public virtual void ResetFlags()
        {
            this.HasCalled = false;
            this.HasChecked = false;
            this.HasRaised = false;
        }

        public abstract void PlayTurn(ref int currentHighestBet, int playersNotFolded, bool canCheck, TurnParts currentPartOfTurn, Random randomBehavior);


        public virtual void SetFlagsForNewTurn()
        {
            this.ResetFlags();
            this.ChipsPlaced = 0;
            this.HasFolded = false;
            this.WinsRound = false;
            this.IsAllIn = false;
        }

        private void SetupBoardPlace(int placeOnBoard)
        {
            switch (placeOnBoard)
            {
                case 1:
                    this.PlaceOnBoard = new Point(360, 340);
                    break;
                case 2:
                    this.PlaceOnBoard = new Point(120, 280);
                    break;
                case 3:
                    this.PlaceOnBoard = new Point(120, 130);
                    break;
                case 4:
                    this.PlaceOnBoard = new Point(300, 30);
                    break;
                case 5:
                    this.PlaceOnBoard = new Point(780, 130);
                    break;
                case 6:
                    this.PlaceOnBoard = new Point(780, 280);
                    break;
            }
        }
    }
}
