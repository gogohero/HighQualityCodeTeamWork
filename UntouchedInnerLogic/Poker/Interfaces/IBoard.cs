namespace Poker.Interfaces
{
    using System.Collections.Generic;

    public interface IBoard
    {
        IList<ICard> Deck { get; }

        int Turn { get; set; }

        void Update(IEnumerable<IParticipant> players);
    }
}