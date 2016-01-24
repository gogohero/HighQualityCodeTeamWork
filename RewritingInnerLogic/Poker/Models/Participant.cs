using System.Drawing;

namespace Poker
{
    using System.Collections.Generic;
    using System.Windows.Forms;

    using Poker.Constants;
    using Poker.Interfaces;
    using Poker.TestingAlgorithms;

    public abstract class Participant : IParticipant
    {
        private int StartingChips = GlobalConstants.StartingChips;

        private int chipsPlaced;

        protected Participant(string name)
        {
            this.Chips = StartingChips;
            this.Name = name;
            this.Controls = new Dictionary<string, Control>();
            this.Hand = new Hand();
        }

        public Dictionary<string, Control> Controls { get; set; }

        public string Name { get; set; }

        public int Chips { get; set; }

        public IHand Hand { get; set; }

        public Point PlaceOnBoard { get; set; }

        public bool HasActed
        {
            get
            {
                return this.HasCalled || this.HasChecked || this.HasRaised;
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
                if (this.Chips - value <= 0)
                {
                    this.chipsPlaced = this.Chips;
                    this.Chips = 0;
                }
                else
                {
                    this.chipsPlaced = value;
                    this.Chips -= value;
                } 
            }
        }

        public virtual void Call(int callAmount)
        {
            this.ChipsPlaced -= callAmount;
            this.HasCalled = true;
            this.Controls["StatusBox"].Text = "Called: " + callAmount;
        }

        public virtual void Raise(int raiseAmount)
        {
            if (this.Chips >= raiseAmount)
            {
                this.ChipsPlaced -= raiseAmount;
                this.HasRaised = true;
                this.Controls["StatusBox"].Text = "Raised: " + raiseAmount;
            }
        }

        public virtual void Check()
        {
            this.HasChecked = true;
            this.Controls["StatusBox"].Text = "Checked";
        }

        public void ResetFlags()
        {
            this.HasCalled = false;
            this.HasChecked = false;
            this.HasRaised = false;
        }

        public void SetFlagsForNewTurn()
        {
            this.ResetFlags();
            this.HasFolded = false;
            this.WinsRound = false;
        }
    }
}
