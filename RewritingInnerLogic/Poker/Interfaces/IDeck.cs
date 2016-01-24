namespace Poker.Interfaces
{
    public interface IDeck
    {
        ICard[] Cards { get; }

        void Shuffle();
    }
}