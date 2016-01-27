namespace Poker.Enumerations
{
    /// <summary>
    /// Enumeration for the strengths of hands
    /// </summary>
    public enum HandStrengthEnum
    {
        /// <summary>
        /// The high card
        /// </summary>
        HighCard = 0,

        /// <summary>
        /// The pair
        /// </summary>
        Pair = 50,

        /// <summary>
        /// The two pair
        /// </summary>
        TwoPair = 80,

        /// <summary>
        /// The three of a kind
        /// </summary>
        ThreeOfAKind = 110,

        /// <summary>
        /// The straight
        /// </summary>
        Straight = 170,

        /// <summary>
        /// The flush
        /// </summary>
        Flush = 200,

        /// <summary>
        /// The full house
        /// </summary>
        FullHouse = 300,

        /// <summary>
        /// The four of a kind
        /// </summary>
        FourOfAKind = 350,

        /// <summary>
        /// The straight flush
        /// </summary>
        StraightFlush = 400,

        /// <summary>
        /// The royal flush
        /// </summary>
        RoyalFlush = 500
    }
}