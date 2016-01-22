using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Poker.Tests
{
    using System.Collections.Generic;

    using Poker.Interfaces;
    using Poker.TestingAlgorithms;

    [TestClass]
    public class PokerTests
    {
        [TestMethod]
        public void TestingAlgorithm_Calculate_Card_Power()
        {
            ICard aceOfSpades = new Card(12, 'S');
            ICard aceOfClubs = new Card(12, 'C');
            ICard aceOfDiamonds = new Card(12, 'D');
            ICard aceOfHearths = new Card(12, 'H');
            IHand hand = new Hand();
            IList<ICard> fourOfAKind = new List<ICard>() { aceOfSpades, aceOfClubs, aceOfDiamonds, aceOfHearths };
            hand.CurrentCards = fourOfAKind;
            CardPowerCalculator.GetCurrentStrengthOfCards(hand);
            Assert.AreEqual(HandStrengthEnum.FourOfAKind, hand.Strength);
        }
    }
}
