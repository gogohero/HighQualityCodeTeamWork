namespace Poker.TestingAlgorithms
{
    using System.Collections.Generic;

    using Poker.Interfaces;
    public class Hand : IHand
    {
        private IList<ICard> currentCards;

        public Hand()
        {
            this.currentCards = new List<ICard>();
        }

        public ICard HighCard { get; set; }

        public IList<ICard> CurrentCards {
            get
            {
                return this.currentCards;
            }

            set
            {
                this.currentCards = value;
            }
        }

        public HandStrengthEnum Strength { get; set; }
    }
}