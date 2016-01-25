using System.Windows.Forms;
namespace Poker.Models
{
    using System.Threading.Tasks;

    public class Player : Participant
    {

        public Player(string name, int placeOnBoard)
            : base(name, placeOnBoard)
        {

        }

        public override void PlayTurn()
        {
        }
    }
}
