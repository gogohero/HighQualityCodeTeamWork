namespace Poker.Interfaces
{
    using System.Collections.Generic;

    using Poker.TestingAlgorithms;

    public interface IHand
    {
        ICard HighCard { get; set; }

        IList<ICard> CurrentCards { get; set; }

        HandStrengthEnum Strength { get; set; }
    }
}