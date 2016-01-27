namespace Poker.Models
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Poker.Enumerations;

    public class Player : Participant
    {
        public Player(string name, int placeOnBoard)
            : base(name, placeOnBoard)
        {
        }

        // Not needed -> leave it empty
        public override void PlayTurn(
            ref int currentHighestBet,
            int playersNotFolded,
            bool canCheck,
            TurnParts currentPartOfTurn,
            Random randomBehavior)
        {
        }
    }
}
