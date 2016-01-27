namespace Poker.Models.Entities
{
    using System.Drawing;

    public class Player : Participant
    {
        public Player(string name, Point placeOnBoard)
            : base(name, placeOnBoard)
        {
        }
    }
}
