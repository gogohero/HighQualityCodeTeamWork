// ***********************************************************************
// Assembly         : Poker
// Author           : Jani
// Created          : 01-28-2016
//
// Last Modified By : Jani
// *********************************************************************************************
// Assembly         : Poker
// Created          : 01-28-2016
//
// Last Modified On : 01-28-2016
// *********************************************************************************************
// <copyright file="GlobalConstants.cs" company="Date"> Copyright ©  2015 </copyright>
// *********************************************************************************************
namespace Poker.Globals
{
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

        /// <summary>
        /// The raise text
        /// </summary>
        public const string RaiseText = "Raised - ";

        /// <summary>
        /// The call text
        /// </summary>
        public const string CallText = "Called - ";

        /// <summary>
        /// The check text
        /// </summary>
        public const string CheckText = "Checked";

        /// <summary>
        /// All in text
        /// </summary>
        public const string AllInText = "ALL IN!";

        /// <summary>
        /// The fold text
        /// </summary>
        public const string FoldText = "Folded";

        /// <summary>
        /// The out of chips text
        /// </summary>
        public const string OutOfChipsText = "Out of chips";   
    }
}