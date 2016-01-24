namespace Poker.Interfaces
{
    using System.Drawing;
    using System.Windows.Forms;

    public interface ICard
    {
        int Rank { get; set; }

        char Suit { get; set; }

        bool IsFacingUp { get; set; }

        Image FrontImage { get; set; }

        Image BackImage { get; set; }

        PictureBox PictureBox { get; set; }
    }
}