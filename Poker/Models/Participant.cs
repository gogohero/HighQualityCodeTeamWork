namespace Poker
{
    using System.Windows.Forms;

    public abstract class Participant
    {
        private const int StartingChips = 10000;

        private double powerHand;
        private Panel panel;
    }
}
