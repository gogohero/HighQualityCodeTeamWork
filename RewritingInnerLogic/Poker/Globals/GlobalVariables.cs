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
        public static int BigBlind = GlobalConstants.StartingBigBlind;

        public static int SmallBlind = GlobalConstants.StartingSmallBlind;

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

        public static Point BoardCardsPlace = new Point(300, 180);
    }
}
