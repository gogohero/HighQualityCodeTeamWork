using System.Drawing;

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
            this.Type = -1;
            this.currentChips = StartingChips;
            this.Call = 0;
            this.powerHand = 0;
            this.Turn = false;
            this.FoldedTurn = false;
            this.Folded = false;
            this.isInGame = true;

        }

        public double Type { get; set; }

        public int Call { get; set; }

        public int Raise { get; set; }

        public bool Turn { get; set; }

        public bool FoldedTurn { get; set; }

        public bool Folded { get; set; }

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
