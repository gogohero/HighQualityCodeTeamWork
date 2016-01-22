namespace Poker.Interfaces
{

    public interface ICard
    {
        int Rank { get; set; }

        char Suit { get; set; }
    }
}