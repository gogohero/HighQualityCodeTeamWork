namespace Poker.Interfaces
{
    public interface IDeck
    {
        ICard[] Cards { get; }

        void Deal(IParticipant[] players, ICard[] cardsOnBoard);
    }
}