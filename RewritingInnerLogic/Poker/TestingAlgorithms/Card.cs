namespace Poker.TestingAlgorithms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    using Interfaces;
    public class Card : ICard
    {
        private int rank;
        private char suit;

        public Card(int rank, char suit, Image img, PictureBox picBox)
        {
            this.Rank = rank;
            this.Suit = suit;
            this.Image = img;
            this.PictureBox = picBox;
        }

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

        public bool IsFacingUp { get; set; }

        public Image Image { get; set; }

        public PictureBox PictureBox { get; set; }

        private void SetCorrectImage()
        {
            string path;
            string ending = "";
            switch (this.suit)
            {
                case 'S':
                    ending = "_of_spades.png";
                    break;
                case 'D':
                    ending = "_of_diamonds.png";
                    break;
                case 'H':
                    ending = "_of_hearts.png";
                    break;
                case 'C':
                    ending = "_of_clubs.png";
                    break;
            }

            if (this.rank != 12)
            {
                path = (this.rank + 2) + ending;

            }
            else
            {
                path = "1" + ending;
            }
        }
    }
}