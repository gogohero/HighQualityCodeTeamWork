// ***********************************************************************
// Assembly         : Poker
// Created          : 01-24-2016
// ***********************************************************************
// <copyright file="Card.cs" company="Date">Copyright ©  2015</copyright>
// ***********************************************************************
namespace Poker.Models.Cards
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    using Poker.Interfaces;

    /// <summary>
    /// Class Card.
    /// </summary>
    public class Card : ICard
    {
        /// <summary>
        /// The rank of the card.
        /// </summary>
        private int rank;

        /// <summary>
        /// The suit of the card.
        /// </summary>
        private char suit;

        /// <summary>
        /// The picture box of the card.
        /// </summary>
        private PictureBox pictureBox;

        /// <summary>
        /// Initializes a new instance of the <see cref="Card"/> class.
        /// </summary>
        /// <param name="rank">The rank.</param>
        /// <param name="suit">The suit.</param>
        public Card(int rank, char suit)
        {
            this.Rank = rank;
            this.Suit = suit;
        }

        /// <summary>
        /// Gets or sets the rank of the card.
        /// </summary>
        /// <value>The rank.</value>
        /// <exception cref="System.ArgumentException">Card rank can only be an integer between 0 and 12, representing the cards from 2 to Ace</exception>
        public int Rank
        {
            get
            {
                return this.rank;
            }

            set
            {
                if (value < 0 || value > 12)
                {
                    throw new ArgumentException("Card rank can only be an integer between 0 and 12, representing the cards from 2 to Ace");
                }

                this.rank = value;
            }
        }

        /// <summary>
        /// Gets or sets the suite of the card.
        /// </summary>
        /// <value>The suit.</value>
        /// <exception cref="System.ArgumentException">Card suit can only be a character, one of C, S, D or H, representing Clubs, Spades, Diamonds and Hearts</exception>
        public char Suit
        {
            get
            {
                return this.suit;
            }

            set
            {
                if (value != 'C' && value != 'S' && value != 'D' && value != 'H')
                {
                    throw new ArgumentException("Card suit can only be a character, one of C, S, D or H, representing Clubs, Spades, Diamonds and Hearts");
                }

                this.suit = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether is facing up or facing down. (True - facing up) / (False - facing down).
        /// </summary>
        /// <value><c>true</c> if this instance is facing up; otherwise, <c>false</c>.</value>
        public bool IsFacingUp { get; set; }

        /// <summary>
        /// Gets or sets the front side image of the card.
        /// </summary>
        /// <value>The front image.</value>
        public Image FrontImage { get; set; }

        /// <summary>
        /// Gets or sets the backside image of the card.
        /// </summary>
        /// <value>The back image.</value>
        public Image BackImage { get; set; }

        /// <summary>
        /// Gets or sets the picture box of the card.
        /// </summary>
        /// <value>The picture box.</value>
        public PictureBox PictureBox
        {
            get
            {
                if (this.IsFacingUp)
                {
                    this.pictureBox.Image = this.FrontImage;
                    return this.pictureBox;
                }
                else
                {
                    this.pictureBox.Image = this.BackImage;
                    return this.pictureBox;
                }
            }

            set
            {
                this.pictureBox = value;
            }
        }
    }
}