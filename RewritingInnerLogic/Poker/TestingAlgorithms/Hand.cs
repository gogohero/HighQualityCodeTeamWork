namespace Poker.TestingAlgorithms
{
    using System.Collections.Generic;
    using Poker.Interfaces;

    /// <summary>
    /// Class Hand.
    /// </summary>
    public class Hand : IHand
    {
        /// <summary>
        /// The _current cards
        /// </summary>
        private IList<ICard> currentCards;

        /// <summary>
        /// Initializes a new instance of the <see cref="Hand"/> class.
        /// </summary>
        public Hand()
        {
            this.currentCards = new List<ICard>();
        }

        /// <summary>
        /// Gets or sets the high card.
        /// </summary>
        /// <value>The high card.</value>
        public ICard HighCard { get; set; }

        /// <summary>
        /// Gets or sets the current cards at the hand.
        /// </summary>
        /// <value>The current cards.</value>
        public IList<ICard> CurrentCards
        {
            get
            {
                return this.currentCards;
            }

            set
            {
                this.currentCards = value;
            }
        }

        /// <summary>
        /// Gets or sets the strength of the hand.
        /// </summary>
        /// <value>The strength of the hand.</value>
        public HandStrengthEnum Strength { get; set; }
    }
}