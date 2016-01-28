// *********************************************************************************************
// Assembly         : Poker
// Created          : 01-28-2016
//
// Last Modified On : 01-28-2016
// *********************************************************************************************
// <copyright file="IDeck.cs" company="Date"> Copyright ©  2015 </copyright>
// *********************************************************************************************
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

        /// <summary>
        /// Shuffles this instance.
        /// </summary>
        void Shuffle();
    }
}