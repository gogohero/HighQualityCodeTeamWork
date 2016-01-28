// *********************************************************************************************
// Assembly         : Poker
// Created          : 01-28-2016
//
// Last Modified On : 01-28-2016
// *********************************************************************************************
// <copyright file="IBot.cs" company="Date"> Copyright ©  2015 </copyright>
// *********************************************************************************************
namespace Poker.Interfaces
{
    using System;
    using Poker.Enumerations;

    /// <summary>
    /// Interface that every bot should implement. Inherits IParticipant to gain basic functionality. 
    /// Adds play turn method which makes calculations and decides how the bot will play his turn.
    /// </summary>
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