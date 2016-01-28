// *********************************************************************************************
// Assembly         : Poker
// Created          : 01-28-2016
//
// Last Modified On : 01-28-2016
// *********************************************************************************************
// <copyright file="GlobalVariables.cs" company="Date"> Copyright ©  2015 </copyright>
// *********************************************************************************************
namespace Poker.Globals
{
    using System;
    using System.Drawing;
    using Poker.Enumerations;

    /// <summary>
    /// Holds all variables in the game that need only one instance.
    /// </summary>
    public static class GlobalVariables
    {
        /// <summary>
        /// The big blind
        /// </summary>
        public static int BigBlind = GlobalConstants.StartingBigBlind;

        /// <summary>
        /// The small blind
        /// </summary>
        public static int SmallBlind = GlobalConstants.StartingSmallBlind;

        /// <summary>
        /// The current highest bet
        /// </summary>
        public static int CurrentHighestBet = 0;

        /// <summary>
        /// The time for player turn
        /// </summary>
        public static decimal TimeForPlayerTurn = 60M;

        /// <summary>
        /// The current turn part
        /// </summary>
        public static TurnParts CurrentTurnPart = TurnParts.BeginGame;

        /// <summary>
        /// The random behavior for bots
        /// </summary>
        public static Random RandomBehaviorForBots = new Random();

        /// <summary>
        /// The player place on board
        /// </summary>
        public static Point PlayerPlaceOnBoard = new Point(360, 340);

        /// <summary>
        /// The bot1 place on board
        /// </summary>
        public static Point Bot1PlaceOnBoard = new Point(120, 280);

        /// <summary>
        /// The bot2 place on board
        /// </summary>
        public static Point Bot2PlaceOnBoard = new Point(120, 130);

        /// <summary>
        /// The bot3 place on board
        /// </summary>
        public static Point Bot3PlaceOnBoard = new Point(300, 30);

        /// <summary>
        /// The bot4 place on board
        /// </summary>
        public static Point Bot4PlaceOnBoard = new Point(780, 130);

        /// <summary>
        /// The bot5 place on board
        /// </summary>
        public static Point Bot5PlaceOnBoard = new Point(780, 280);

        /// <summary>
        /// The board cards place
        /// </summary>
        public static Point BoardCardsPlace = new Point(300, 180);
    }
}
