namespace Poker.TestingAlgorithms
{
    using System.Collections.Generic;

    using Poker.Interfaces;
    public class Hand : IHand
    {
        private IList<ICard> currentCards;

        public ICard HighCard { get; set; }

        public IList<ICard> CurrentCards {
            get
            {
                if (this.currentCards == null)
                {
                    this.currentCards = new List<ICard>();
                }

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