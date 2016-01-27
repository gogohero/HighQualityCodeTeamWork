namespace Poker.Interfaces
{
    using System.Collections.Generic;
    using Poker.TestingAlgorithms;

    /// <summary>
    /// Interface IHand
    /// </summary>
    public interface IHand
    {
        /// <summary>
        /// Gets or sets the high card.
        /// </summary>
        /// <value>The high card.</value>
        ICard HighCard { get; set; }

        /// <summary>
        /// Gets or sets the current cards at the hand.
        /// </summary>
        /// <value>The current cards.</value>
        IList<ICard> CurrentCards { get; set; }

        /// <summary>
        /// Gets or sets the strength of the hand.
        /// </summary>
        /// <value>The strength of the hand.</value>
        HandStrengthEnum Strength { get; set; }
    }
}