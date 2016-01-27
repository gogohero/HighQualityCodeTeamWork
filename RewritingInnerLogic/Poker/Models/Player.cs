namespace Poker.Models
{
    using System;
    using System.Drawing;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Poker.Enumerations;

    public class Player : Participant
    {
        public Player(string name, Point placeOnBoard)
            : base(name, placeOnBoard)
        {
        }
    }
}
