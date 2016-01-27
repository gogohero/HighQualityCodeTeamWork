namespace Poker.Interfaces
{
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
        void Deal(IParticipant[] players, ICard[] cardsOnBoard);
    }
}