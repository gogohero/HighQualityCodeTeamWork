using System.Drawing;

namespace Poker
{
    using System.Windows.Forms;

    using Poker.Constants;
    using Poker.Interfaces;
    using Poker.TestingAlgorithms;

    public abstract class Participant : IParticipant
    {
        private int StartingChips = GlobalConstants.StartingChips;

        private int chipsPlaced;
        //private int currentChips;
        //private double powerHand;
        private readonly Panel panel;

        protected Participant(Panel panel)
        {
            this.panel = panel;
            this.Chips = StartingChips;
        }

        public int Chips { get; set; }

        public IHand Hand { get; set; }

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
                if (this.Chips <= 0)
                {
                    return false;
                }

                return true;
            }
        }

        //public double Type { get; set; }

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

        //public int Raise { get; set; }

        //public double PowerHand { get; set; }

        //public bool Turn { get; set; }

        //public bool FoldedTurn { get; set; }

        //public bool Folded { get; set; }

        public Panel Panel
        {
            get
            {
                return this.panel;
            }
        }

        //public virtual void PlayTurn()
        //{
        //    if (!this.IsInGame || this.HasFolded)
        //    {
        //        return;
        //    }

        //    if (this.HasFolded || this.HasChecked)
        //    {
        //        return;
        //    }
        //}

        public virtual void Call(int callAmount)
        {
            this.ChipsPlaced -= callAmount;
            this.HasCalled = true;
        }

        public virtual void Raise(int raiseAmount)
        {
            if (this.Chips >= raiseAmount)
            {
                this.ChipsPlaced -= raiseAmount;
                this.HasRaised = true;
            }
        }

        public virtual void Check()
        {
            this.HasChecked = true;
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

        internal void SetupPanel(Point location)
        {
            this.Panel.Location = location;
            this.Panel.BackColor = Color.Transparent;
            this.Panel.Height = 150;
            this.Panel.Width = 180;
            this.Panel.Visible = false;
        }
    }
}
