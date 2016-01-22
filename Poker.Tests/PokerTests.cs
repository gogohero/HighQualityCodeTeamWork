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
        public void TestingAlgorithm_Calculate_Card_Power_FourOfAKind_ShouldPass()
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

        [TestMethod]
        public void TestingAlgorithm_Calculate_Card_Power_RoyalFlush_ShouldPass()
        {
            ICard aceOfSpades = new Card(12, 'S');
            ICard kingOfSpades = new Card(11, 'S');
            ICard queenOfSpades = new Card(10, 'S');
            ICard jokerOfSpades = new Card(9, 'S');
            ICard tenOfSpades = new Card(8, 'S');
            IHand hand = new Hand();
            IList<ICard> royalFlush = new List<ICard>() { aceOfSpades, kingOfSpades, queenOfSpades, jokerOfSpades, tenOfSpades };
            hand.CurrentCards = royalFlush;
            CardPowerCalculator.GetCurrentStrengthOfCards(hand);
            Assert.AreEqual(HandStrengthEnum.RoyalFlush, hand.Strength);
        }

        [TestMethod]
        public void TestingAlgorithm_Calculate_Card_Power_FullHouse_ShouldPass()
        {
            ICard card1 = new Card(12, 'S');
            ICard card2 = new Card(12, 'D');
            ICard card3 = new Card(12, 'H');
            ICard card4 = new Card(10, 'S');
            ICard card5 = new Card(10, 'C');
            IHand hand = new Hand();
            IList<ICard> fullHouse = new List<ICard>() { card1, card2, card3, card4, card5 };
            hand.CurrentCards = fullHouse;
            CardPowerCalculator.GetCurrentStrengthOfCards(hand);
            Assert.AreEqual(HandStrengthEnum.FullHouse, hand.Strength);
        }

        [TestMethod]
        public void TestingAlgorithm_Calculate_Card_Power_ThreeOfAKind_ShouldPass()
        {
            ICard card1 = new Card(12, 'S');
            ICard card2 = new Card(12, 'D');
            ICard card3 = new Card(12, 'H');
            ICard card4 = new Card(10, 'S');
            ICard card5 = new Card(9, 'C');
            IHand hand = new Hand();
            IList<ICard> fullHouse = new List<ICard>() { card1, card2, card3, card4, card5 };
            hand.CurrentCards = fullHouse;
            CardPowerCalculator.GetCurrentStrengthOfCards(hand);
            Assert.AreEqual(HandStrengthEnum.ThreeOfAKind, hand.Strength);
        }

        [TestMethod]
        public void TestingAlgorithm_Calculate_Card_Power_TwoPair_ShouldPass()
        {
            ICard card1 = new Card(12, 'S');
            ICard card2 = new Card(9, 'D');
            ICard card3 = new Card(12, 'H');
            ICard card4 = new Card(10, 'S');
            ICard card5 = new Card(9, 'C');
            IHand hand = new Hand();
            IList<ICard> fullHouse = new List<ICard>() { card1, card2, card3, card4, card5 };
            hand.CurrentCards = fullHouse;
            CardPowerCalculator.GetCurrentStrengthOfCards(hand);
            Assert.AreEqual(HandStrengthEnum.TwoPair, hand.Strength);
        }

        [TestMethod]
        public void TestingAlgorithm_Calculate_Card_Power_Pair_ShouldPass()
        {
            ICard card1 = new Card(12, 'S');
            ICard card2 = new Card(9, 'D');
            ICard card3 = new Card(5, 'H');
            ICard card4 = new Card(10, 'S');
            ICard card5 = new Card(9, 'C');
            IHand hand = new Hand();
            IList<ICard> fullHouse = new List<ICard>() { card1, card2, card3, card4, card5 };
            hand.CurrentCards = fullHouse;
            CardPowerCalculator.GetCurrentStrengthOfCards(hand);
            Assert.AreEqual(HandStrengthEnum.Pair, hand.Strength);
        }

        [TestMethod]
        public void TestingAlgorithm_Calculate_Card_Power_Flush_ShouldPass()
        {
            ICard card1 = new Card(12, 'S');
            ICard card2 = new Card(9, 'S');
            ICard card3 = new Card(5, 'S');
            ICard card4 = new Card(10, 'S');
            ICard card5 = new Card(9, 'S');
            IHand hand = new Hand();
            IList<ICard> fullHouse = new List<ICard>() { card1, card2, card3, card4, card5 };
            hand.CurrentCards = fullHouse;
            CardPowerCalculator.GetCurrentStrengthOfCards(hand);
            Assert.AreEqual(HandStrengthEnum.Flush, hand.Strength);
        }

        [TestMethod]
        public void TestingAlgorithm_Calculate_Card_Power_Straigth_ShouldPass()
        {
            ICard card1 = new Card(12, 'S');
            ICard card2 = new Card(11, 'C');
            ICard card3 = new Card(10, 'H');
            ICard card4 = new Card(9, 'S');
            ICard card5 = new Card(8, 'S');
            IHand hand = new Hand();
            IList<ICard> fullHouse = new List<ICard>() { card1, card2, card3, card4, card5 };
            hand.CurrentCards = fullHouse;
            CardPowerCalculator.GetCurrentStrengthOfCards(hand);
            Assert.AreEqual(HandStrengthEnum.Straight, hand.Strength);
        }

        [TestMethod]
        public void TestingAlgorithm_Calculate_Card_Power_HighCard_ShouldPass()
        {
            ICard card1 = new Card(12, 'S');
            ICard card2 = new Card(11, 'C');
            ICard card3 = new Card(6, 'H');
            ICard card4 = new Card(9, 'S');
            ICard card5 = new Card(8, 'S');
            IHand hand = new Hand();
            IList<ICard> fullHouse = new List<ICard>() { card1, card2, card3, card4, card5 };
            hand.CurrentCards = fullHouse;
            CardPowerCalculator.GetCurrentStrengthOfCards(hand);
            Assert.AreEqual(HandStrengthEnum.HighCard, hand.Strength);
        }

        [TestMethod]
        public void TestingAlgorithm_Calculate_Card_Power_StraigthFlush_ShouldPass()
        {
            ICard card1 = new Card(11, 'S');
            ICard card2 = new Card(10, 'S');
            ICard card3 = new Card(9, 'S');
            ICard card4 = new Card(8, 'S');
            ICard card5 = new Card(7, 'S');
            IHand hand = new Hand();
            IList<ICard> fullHouse = new List<ICard>() { card1, card2, card3, card4, card5 };
            hand.CurrentCards = fullHouse;
            CardPowerCalculator.GetCurrentStrengthOfCards(hand);
            Assert.AreEqual(HandStrengthEnum.StraightFlush, hand.Strength);
        }
    }
}
