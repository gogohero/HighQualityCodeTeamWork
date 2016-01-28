
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

     
    }
}