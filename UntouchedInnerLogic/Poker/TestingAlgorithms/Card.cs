namespace Poker.TestingAlgorithms
{
    using System;

    using Interfaces;
    public class Card : ICard
    {
        private int rank;
        private char suit;

        public Card(int rank, char suit)
        {
            this.Rank = rank;
            this.Suit = suit;
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
    }
}