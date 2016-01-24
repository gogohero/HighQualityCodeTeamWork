namespace Poker.Interfaces
{
    public interface IParticipant
    {
        string Name { get; set; }

        int Chips { get; set; }

        IHand Hand { get; set; }

        bool HasFolded { get; set; }

        bool HasCalled { get; set; }

        bool HasRaised { get; set; }

        bool HasChecked { get; set; }
    }
}