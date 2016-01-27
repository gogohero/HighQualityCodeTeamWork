namespace Poker.Interfaces
{
    using System;

    using Poker.Enumerations;

    public interface IBot : IParticipant
    {
        /// <summary>
        /// Plays the turn.
        /// </summary>
        /// <param name="currentHighestBet">The current highest bet.</param>
        /// <param name="playersNotFolded">The players not folded.</param>
        /// <param name="canCheck">if set to <c>true</c> [can check].</param>
        /// <param name="currentPartOfTurn">The current part of turn.</param>
        /// <param name="randomBehavior">The random behavior.</param>
        void PlayTurn(ref int currentHighestBet, int playersNotFolded, bool canCheck, TurnParts currentPartOfTurn, Random randomBehavior);
    }
}