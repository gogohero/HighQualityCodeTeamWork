namespace Poker.Interfaces
{
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Interface ICard that hold properties for everything that have the card.
    /// </summary>
    public interface ICard
    {
        /// <summary>
        /// Gets or sets the rank of the card.
        /// </summary>
        int Rank { get; set; }

        /// <summary>
        /// Gets or sets the suite of the card.
        /// </summary>
        char Suit { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is facing up or facing down. (True - facing up) / (False - facing down).
        /// </summary>
        bool IsFacingUp { get; set; }

        /// <summary>
        /// Gets or sets the front side image of the card.
        /// </summary>
        Image FrontImage { get; set; }

        /// <summary>
        /// Gets or sets the backside image of the card.
        /// </summary>
        Image BackImage { get; set; }

        /// <summary>
        /// Gets or sets the picture box of the card.
        /// </summary>
        PictureBox PictureBox { get; set; }
    }
}