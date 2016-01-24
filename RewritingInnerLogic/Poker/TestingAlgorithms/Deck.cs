namespace Poker.TestingAlgorithms
{
    using Poker.Interfaces;

    public class Deck : IDeck
    {
        public Deck()
        {
            this.Cards = this.InitializeDeck();
        }

        public ICard[] Cards { get; }

        public void Shuffle()
        {
            throw new System.NotImplementedException();
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