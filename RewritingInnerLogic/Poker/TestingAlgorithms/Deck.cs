namespace Poker.TestingAlgorithms
{
    using Poker.Interfaces;
    public class Deck : IDeck
    {
        public Deck(ICard[] cards)
        {
            this.Cards = Cards;
        }

        public ICard[] Cards { get; }

        public void Shuffle(ICard[] cards)
        {
            throw new System.NotImplementedException();
        }
    }
}