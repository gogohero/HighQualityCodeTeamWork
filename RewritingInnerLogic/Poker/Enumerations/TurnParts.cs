// *********************************************************************************************
// Assembly         : Poker
// Created          : 01-28-2016
//
// Last Modified On : 01-28-2016
// *********************************************************************************************
// <copyright file="TurnParts.cs" company="Date"> Copyright ©  2015 </copyright>
// *********************************************************************************************
namespace Poker.Enumerations
{
    /// <summary>
    /// Enumeration for turns in a classic Poker game. 
    /// First is negative because it is the beginning of the game.
    /// </summary>
    public enum TurnParts
    {
        /// <summary>
        /// The beginning of the game
        /// </summary>
        BeginGame = -1,

        /// <summary>
        /// Represents the post deal turn
        /// </summary>
        PreFlop,

        /// <summary>
        /// The flop where 3 cards are turned upside
        /// </summary>
        Flop,

        /// <summary>
        /// The turn where 1 card is turned up after players placed their bets during the flop turn
        /// </summary>
        Turn,

        /// <summary>
        /// The river where all 5 cards on the board are facing up. 
        /// Bets stop here and winners are evaluated
        /// </summary>
        River
    }
}
