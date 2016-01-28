namespace Poker.Models.Entities
{
    using System.Drawing;

    /// <summary>
    /// Class Player.
    /// </summary>
    public class Player : Participant
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="placeOnBoard">The place on board.</param>
        public Player(string name, Point placeOnBoard)
            : base(name, placeOnBoard)
        {
        }
    }
}
