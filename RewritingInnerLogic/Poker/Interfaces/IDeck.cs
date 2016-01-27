namespace Poker.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface IDeck
    /// </summary>
    public interface IDeck
    {
        /// <summary>
        /// Gets the cards.
        /// </summary>
        /// <value>The cards.</value>
        ICard[] Cards { get; }

        /// <summary>
        /// Deals the specified players.
        /// </summary>
        /// <param name="players">The players.</param>
        /// <param name="cardsOnBoard">The cards on board.</param>
        void Deal(IList<IParticipant> players, ICard[] cardsOnBoard);
    }
}