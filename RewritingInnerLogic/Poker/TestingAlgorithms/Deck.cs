namespace Poker.TestingAlgorithms
{
    using System;
    using System.IO;

    using Poker.Interfaces;

    public class Deck : IDeck
    {
        public Deck()
        {
            this.Cards = this.InitializeDeck();
        }
    
        public ICard[] Cards { get; }

        private void Shuffle()
        {
            // Fisher-Yates shuffle
            // one of the most popular mathematically correct shuffle methods
            Random rnd = new Random();
            int n = this.Cards.Length;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                ICard value = this.Cards[k];
                this.Cards[k] = this.Cards[n];
                this.Cards[n] = value;
            }
        }

        public void Deal(IParticipant[] players, ICard[] cardsOnBoard)
        {
            this.Shuffle();
            int toTakeFromDeckIndex = 0;
            for (int i = 0; i < players.Length; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    players[i].Hand.CurrentCards.Add(this.Cards[toTakeFromDeckIndex]);
                    toTakeFromDeckIndex += 1;
                }
            }

            for (int i = 0; i < 5; i++)
            {
                cardsOnBoard[i] = this.Cards[toTakeFromDeckIndex];
                toTakeFromDeckIndex += 1;
            }
        }

        private ICard[] InitializeDeck()
        {
            ICard[] cards = new ICard[52];
            int cardCounter = 0;

            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    switch (j)
                    {
                        case 0:
                            cards[cardCounter] = new Card(i, 'S');
                            break;
                        case 1:
                            cards[cardCounter] = new Card(i, 'H');
                            break;
                        case 2:
                            cards[cardCounter] = new Card(i, 'D');
                            break;
                        case 3:
                            cards[cardCounter] = new Card(i, 'C');
                            break;
                    }
                    cardCounter += 1;
                }
            }

            return cards;
        }
    }
}