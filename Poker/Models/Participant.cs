namespace Poker
{
    using System;
    using System.Windows.Forms;

    public abstract class Participant
    {
        private const int StartingChips = 10000;

        private int call;
        private int currentChips;
        private bool isInGame;
        private double powerHand;
        private readonly Panel panel;


        protected Participant(Panel panel)
        {
            this.panel = panel;
            this.currentChips = StartingChips;
            this.powerHand = 0;
            this.isInGame = true;

        }

        public int CurrentChips
        {
            get
            {
                return this.currentChips;
            }

            set
            {
                this.currentChips = value;
            }
        }

        public Panel Panel
        {
            get
            {
                return this.panel;
            }
        }

        public double PowerHand { get; set; }

        public bool IsInGame
        {
            get
            {
                return this.isInGame;
            }

             set
            {
                this.isInGame = value;
            }
        }

        private void CheckIsParticipantInGame()
        {
            if (this.currentChips <= 0)
            {
                this.IsInGame = false;
            }

            this.IsInGame = true;
        }
    }
}
