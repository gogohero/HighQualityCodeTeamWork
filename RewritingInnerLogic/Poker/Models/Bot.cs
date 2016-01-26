using System.Windows.Forms;
namespace Poker.Models
{
    using System;
    using System.Threading.Tasks;

    using Poker.Interfaces;
    public class Bot : Participant
    {

        public Bot(string name, int placeOnBoard)
            : base(name, placeOnBoard)
        {

        }

        public override void PlayTurn()
        {
            
        }
    }
}
