
namespace Poker.Constants
{
    using System;
    using System.Drawing;

    using Poker.Enumerations;

    /// <summary>
    /// The class who initialize global constants for starting chips and blinds.
    /// </summary>
    public static class GlobalConstants
    {
        /// <summary>
        /// Initialize global constant for starting chips.
        /// </summary>
        public const int StartingChips = 10000;

        /// <summary>
        /// Initialize global constant for starting big blind.
        /// </summary>
        public const int StartingBigBlind = 500;

        /// <summary>
        /// Initialize global constant for starting small blind.
        /// </summary>
        public const int StartingSmallBlind = 250;

        public const string RaiseText = "Raised - ";

        public const string CallText = "Called - ";

        public const string CheckText = "Checked";

        public const string AllInText = "ALL IN!";

        public const string FoldText = "Folded";

        public const string OutOfChipsText = "Out of chips";

        public static int BigBlind = StartingBigBlind;

        public static int SmallBlind = StartingSmallBlind;

        public static int CurrentHighestBet = 0;

        public static decimal TimeForPlayerTurn = 60M;

        public static TurnParts CurrentTurnPart = TurnParts.BeginGame;

        public static Random RandomBehaviorForBots = new Random();

        public static Point PlayerPlaceOnBoard = new Point(360, 340);

        public static Point Bot1PlaceOnBoard = new Point(120, 280);

        public static Point Bot2PlaceOnBoard = new Point(120, 130);

        public static Point Bot3PlaceOnBoard = new Point(300, 30);

        public static Point Bot4PlaceOnBoard = new Point(780, 130);

        public static Point Bot5PlaceOnBoard = new Point(780, 280);
    }
}