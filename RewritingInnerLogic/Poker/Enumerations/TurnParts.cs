
namespace Poker.Enumerations
{
    public enum TurnParts
    {
        /// <summary>
        /// The beginning of the game
        /// </summary>
        BeginGame = -1,

        /// <summary>
        /// The flop where 3 cards are dealt
        /// </summary>
        Flop,

        /// <summary>
        /// The turn where 1 card is turned up after players placed their bets during the flop turn
        /// </summary>
        Turn,

        /// <summary>
        /// The river where all 5 cards on the board are facing up. Bets stop here
        /// </summary>
        River,

        /// <summary>
        /// The end where the winner is evaluated and gets the chips from the pot
        /// </summary>
        End
    }
}
