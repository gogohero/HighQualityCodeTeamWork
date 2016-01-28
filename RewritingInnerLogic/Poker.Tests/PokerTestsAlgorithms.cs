using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Poker.Globals;
using Poker.Models;

namespace Poker.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using Poker.Enumerations;
    using Poker.Interfaces;
    using Poker.Models.Cards;
    using Poker.Models.Entities;
    using Poker.PowerCalculator;

    [TestClass]
    public class PokerTestsAlgorithms
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
        public void TestingAlgorithm_Calculate_Card_Power_FullHousePairFirst_ShouldPass()
        {
            ICard card1 = new Card(10, 'S');
            ICard card2 = new Card(10, 'S');
            ICard card3 = new Card(12, 'S');
            ICard card4 = new Card(12, 'S');
            ICard card5 = new Card(12, 'S');
            IHand hand = new Hand();
            IList<ICard> combination = new List<ICard>() { card1, card2, card3, card4, card5 };
            hand.CurrentCards = combination;
            CardPowerCalculator.GetCurrentStrengthOfCards(hand);
            Assert.AreEqual(HandStrengthEnum.FullHouse, hand.Strength);
        }

        [TestMethod]
        public void TestingAlgorithm_Calculate_Card_Power_FullHouseThreeOfAKindFirst_ShouldPass()
        {
            ICard card1 = new Card(11, 'S');
            ICard card2 = new Card(11, 'S');
            ICard card3 = new Card(7, 'S');
            ICard card4 = new Card(7, 'S');
            ICard card5 = new Card(7, 'S');
            IHand hand = new Hand();
            IList<ICard> combination = new List<ICard>() { card1, card2, card3, card4, card5 };
            hand.CurrentCards = combination;
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
            IList<ICard> combination = new List<ICard>() { card1, card2, card3, card4, card5 };
            hand.CurrentCards = combination;
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
            IList<ICard> combination = new List<ICard>() { card1, card2, card3, card4, card5 };
            hand.CurrentCards = combination;
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
            IList<ICard> combination = new List<ICard>() { card1, card2, card3, card4, card5 };
            hand.CurrentCards = combination;
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
            IList<ICard> combination = new List<ICard>() { card1, card2, card3, card4, card5 };
            hand.CurrentCards = combination;
            CardPowerCalculator.GetCurrentStrengthOfCards(hand);
            Assert.AreEqual(HandStrengthEnum.Flush, hand.Strength);
        }

        [TestMethod]
        public void TestingAlgorithm_Calculate_Card_Power_FlushNotSequential_ShouldPass()
        {
            ICard card1 = new Card(12, 'S');
            ICard card2 = new Card(9, 'S');
            ICard card3 = new Card(5, 'H');
            ICard card4 = new Card(10, 'H');
            ICard card5 = new Card(5, 'S');
            ICard card6 = new Card(9, 'S');
            ICard card7 = new Card(2, 'S');
            IHand hand = new Hand();
            IList<ICard> combination = new List<ICard>() { card1, card2, card3, card4, card5, card6, card7 };
            hand.CurrentCards = combination;
            CardPowerCalculator.GetCurrentStrengthOfCards(hand);
            Assert.AreEqual(HandStrengthEnum.Flush, hand.Strength);
        }

        [TestMethod]
        public void TestingAlgorithm_Calculate_Card_Power_Straight_ShouldPass()
        {
            ICard card1 = new Card(12, 'S');
            ICard card2 = new Card(11, 'C');
            ICard card3 = new Card(10, 'H');
            ICard card4 = new Card(9, 'S');
            ICard card5 = new Card(8, 'S');
            IHand hand = new Hand();
            IList<ICard> combination = new List<ICard>() { card1, card2, card3, card4, card5 };
            hand.CurrentCards = combination;
            CardPowerCalculator.GetCurrentStrengthOfCards(hand);
            Assert.AreEqual(HandStrengthEnum.Straight, hand.Strength);
        }

        [TestMethod]
        public void TestingAlgorithm_Calculate_Card_Power_HighCard_ShouldPass()
        {
            ICard card1 = new Card(2, 'S');
            ICard card2 = new Card(11, 'C');
            ICard card3 = new Card(6, 'H');
            ICard card4 = new Card(12, 'S');
            ICard card5 = new Card(8, 'S');
            IHand hand = new Hand();
            IList<ICard> combination = new List<ICard>() { card1, card2, card3, card4, card5 };
            hand.CurrentCards = combination;
            CardPowerCalculator.GetCurrentStrengthOfCards(hand);
            Assert.AreEqual(HandStrengthEnum.HighCard, hand.Strength);
        }

        [TestMethod]
        public void TestingAlgorithm_Calculate_Card_Power_StraightFlush_ShouldPass()
        {
            ICard card1 = new Card(11, 'S');
            ICard card2 = new Card(10, 'S');
            ICard card3 = new Card(9, 'S');
            ICard card4 = new Card(8, 'S');
            ICard card5 = new Card(7, 'S');
            IHand hand = new Hand();
            IList<ICard> combination = new List<ICard>() { card1, card2, card3, card4, card5 };
            hand.CurrentCards = combination;
            CardPowerCalculator.GetCurrentStrengthOfCards(hand);
            Assert.AreEqual(HandStrengthEnum.StraightFlush, hand.Strength);
        }

        [TestMethod]
        public void TestingAlgorithm_Calculate_HighCard_FullHouseThreeOfAKindFirst_ShouldPass()
        {
            ICard card1 = new Card(11, 'S');
            ICard card2 = new Card(11, 'S');
            ICard card3 = new Card(7, 'S');
            ICard card4 = new Card(7, 'S');
            ICard card5 = new Card(7, 'S');
            IHand hand = new Hand();
            IList<ICard> combination = new List<ICard>() { card1, card2, card3, card4, card5 };
            hand.CurrentCards = combination;
            CardPowerCalculator.GetCurrentStrengthOfCards(hand);
            Assert.AreEqual(card3.Rank, hand.HighCard.Rank);
        }

        [TestMethod]
        public void TestingAlgorithm_Calculate_HighCard_FullHousePairFirst_ShouldPass()
        {
            ICard card1 = new Card(11, 'S');
            ICard card2 = new Card(11, 'S');
            ICard card3 = new Card(11, 'S');
            ICard card4 = new Card(7, 'S');
            ICard card5 = new Card(7, 'S');
            IHand hand = new Hand();
            IList<ICard> combination = new List<ICard>() { card1, card2, card3, card4, card5 };
            hand.CurrentCards = combination;
            CardPowerCalculator.GetCurrentStrengthOfCards(hand);
            Assert.AreEqual(card1.Rank, hand.HighCard.Rank);
        }

        [TestMethod]
        public void TestingAlgorithm_Calculate_HighCard_TwoPair_ShouldPass()
        {
            ICard card1 = new Card(11, 'S');
            ICard card2 = new Card(11, 'S');
            ICard card3 = new Card(8, 'D');
            ICard card4 = new Card(7, 'S');
            ICard card5 = new Card(7, 'S');
            IHand hand = new Hand();
            IList<ICard> combination = new List<ICard>() { card1, card2, card3, card4, card5 };
            hand.CurrentCards = combination;
            CardPowerCalculator.GetCurrentStrengthOfCards(hand);
            Assert.AreEqual(card1.Rank, hand.HighCard.Rank);
        }

        [TestMethod]
        public void TestingAlgorithm_Calculate_HighCard_ThreeOfAKind_ShouldPass()
        {
            ICard card1 = new Card(11, 'S');
            ICard card2 = new Card(12, 'S');
            ICard card3 = new Card(7, 'D');
            ICard card4 = new Card(7, 'S');
            ICard card5 = new Card(7, 'S');
            IHand hand = new Hand();
            IList<ICard> combination = new List<ICard>() { card1, card2, card3, card4, card5 };
            hand.CurrentCards = combination;
            CardPowerCalculator.GetCurrentStrengthOfCards(hand);
            Assert.AreEqual(card3.Rank, hand.HighCard.Rank);
        }

        [TestMethod]
        public void TestingAlgorithm_Calculate_HighCard_Pair_ShouldPass()
        {
            ICard card1 = new Card(11, 'S');
            ICard card2 = new Card(12, 'S');
            ICard card3 = new Card(8, 'D');
            ICard card4 = new Card(7, 'S');
            ICard card5 = new Card(7, 'S');
            IHand hand = new Hand();
            IList<ICard> combination = new List<ICard>() { card1, card2, card3, card4, card5 };
            hand.CurrentCards = combination;
            CardPowerCalculator.GetCurrentStrengthOfCards(hand);
            Assert.AreEqual(card4.Rank, hand.HighCard.Rank);
        }

        [TestMethod]
        public void TestingAlgorithm_Calculate_HighCard_Flush_ShouldPass()
        {
            ICard card1 = new Card(11, 'S');
            ICard card2 = new Card(12, 'S');
            ICard card3 = new Card(8, 'S');
            ICard card4 = new Card(7, 'S');
            ICard card5 = new Card(7, 'S');
            IHand hand = new Hand();
            IList<ICard> combination = new List<ICard>() { card1, card2, card3, card4, card5 };
            hand.CurrentCards = combination;
            CardPowerCalculator.GetCurrentStrengthOfCards(hand);
            Assert.AreEqual(card2.Rank, hand.HighCard.Rank);
        }

        [TestMethod]
        public void TestingAlgorithm_Calculate_HighCard_Straight_ShouldPass()
        {
            ICard card1 = new Card(11, 'S');
            ICard card2 = new Card(12, 'D');
            ICard card3 = new Card(10, 'S');
            ICard card4 = new Card(9, 'S');
            ICard card5 = new Card(8, 'S');
            IHand hand = new Hand();
            IList<ICard> combination = new List<ICard>() { card1, card2, card3, card4, card5 };
            hand.CurrentCards = combination;
            CardPowerCalculator.GetCurrentStrengthOfCards(hand);
            Assert.AreEqual(card2.Rank, hand.HighCard.Rank);
        }

        [TestMethod]
        public void TestingAlgorithm_Calculate_HighCard_StraightFlush_ShouldPass()
        {
            ICard card1 = new Card(11, 'S');
            ICard card2 = new Card(7, 'S');
            ICard card3 = new Card(10, 'S');
            ICard card4 = new Card(9, 'S');
            ICard card5 = new Card(8, 'S');
            IHand hand = new Hand();
            IList<ICard> combination = new List<ICard>() { card1, card2, card3, card4, card5 };
            hand.CurrentCards = combination;
            CardPowerCalculator.GetCurrentStrengthOfCards(hand);
            Assert.AreEqual(card1.Rank, hand.HighCard.Rank);
        }

        [TestMethod]
        public void TestingAlgorithm_Calculate_HighCard_FourOfAKind_ShouldPass()
        {
            ICard card1 = new Card(11, 'S');
            ICard card2 = new Card(11, 'S');
            ICard card3 = new Card(11, 'S');
            ICard card4 = new Card(11, 'S');
            ICard card5 = new Card(8, 'S');
            IHand hand = new Hand();
            IList<ICard> combination = new List<ICard>() { card1, card2, card3, card4, card5 };
            hand.CurrentCards = combination;
            CardPowerCalculator.GetCurrentStrengthOfCards(hand);
            Assert.AreEqual(card1.Rank, hand.HighCard.Rank);
        }

        [TestMethod]
        public void TestingAlgorithm_Calculate_HighCard_RoyalFlush_ShouldPass()
        {
            ICard card1 = new Card(11, 'S');
            ICard card2 = new Card(10, 'S');
            ICard card3 = new Card(12, 'S');
            ICard card4 = new Card(9, 'S');
            ICard card5 = new Card(8, 'S');
            IHand hand = new Hand();
            IList<ICard> combination = new List<ICard>() { card1, card2, card3, card4, card5 };
            hand.CurrentCards = combination;
            CardPowerCalculator.GetCurrentStrengthOfCards(hand);
            Assert.AreEqual(card3.Rank, hand.HighCard.Rank);
        }

        [TestMethod]
        public void TestingAlgorithm_Calculate_HighCard_HighCardPower_ShouldPass()
        {
            ICard card1 = new Card(4, 'S');
            ICard card2 = new Card(7, 'H');
            ICard card3 = new Card(6, 'S');
            ICard card4 = new Card(2, 'S');
            ICard card5 = new Card(3, 'S');
            IHand hand = new Hand();
            IList<ICard> combination = new List<ICard>() { card1, card2, card3, card4, card5 };
            hand.CurrentCards = combination;
            CardPowerCalculator.GetCurrentStrengthOfCards(hand);
            Assert.AreEqual(card2.Rank, hand.HighCard.Rank);
        }

        [TestMethod]
        public void TestingAlgorithm_Calculate_HighCardPower_2()
        {
            ICard card1 = new Card(4, 'H');
            ICard card2 = new Card(7, 'S');
            ICard card3 = new Card(6, 'S');
            ICard card4 = new Card(2, 'S');
            ICard card5 = new Card(3, 'S');
            IHand hand = new Hand();
            IList<ICard> combination = new List<ICard>() { card1, card2, card3, card4, card5 };
            hand.CurrentCards = combination;
            CardPowerCalculator.GetCurrentStrengthOfCards(hand);
            Assert.AreEqual(card2.Rank, hand.HighCard.Rank);
        }

        [TestMethod]
        public void TestingAlgorithm_Check_Deck_InitializesCardsCorrectly_ShouldPass()
        {
            IDeck deck = new Deck();
            int differentCardsCounter = deck.Cards.Count(card => deck.Cards.Count(c => c.Rank == card.Rank && c.Suit == card.Suit) == 1);
            Assert.AreEqual(52, differentCardsCounter);
        }

        [TestMethod]
        public void Test_Deck_Shuffle_Randomness()
        {
            Deck one = new Deck();
            one.Shuffle();
            Deck two = new Deck();
            two.Shuffle();
            Assert.IsFalse(two.Cards[2].Rank == one.Cards[2].Rank 
                                && two.Cards[5].Rank == one.Cards[5].Rank
                                 && two.Cards[7].Rank == one.Cards[7].Rank
                                  && two.Cards[15].Rank == one.Cards[15].Rank
                                   && two.Cards[50].Rank == one.Cards[50].Rank
                                    && two.Cards[45].Rank == one.Cards[45].Rank
                                     && two.Cards[35].Rank == one.Cards[35].Rank);
        }

        [TestMethod]
        public void TestDeck_Length()
        {
            Deck deck = new Deck();
            Assert.AreEqual(deck.Cards.Length, 52);
        }

        [TestMethod]
        public void Test_Global_Constants()
        {
            Assert.AreEqual(GlobalConstants.StartingBigBlind, 500);
            Assert.AreEqual(GlobalConstants.StartingChips, 10000);
            Assert.AreEqual(GlobalConstants.StartingSmallBlind, 250);
        }

        [TestMethod]
        public void Test_Bot_Name()
        {
            Bot bot = new Bot("go6o", GlobalVariables.Bot2PlaceOnBoard);
            Assert.AreEqual("go6o", bot.Name);
        }

        [TestMethod]
        public void Test_Bot_Chips()
        {
            Bot bot = new Bot("go6o", GlobalVariables.Bot2PlaceOnBoard);
            Assert.AreEqual(GlobalConstants.StartingChips, bot.Chips);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Testing_Card_Rank_Exception()
        {
            ICard card1 = new Card(33, 'S');
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Card_Suit_Exception()
        {
            ICard card = new Card(12, 'M');
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Card_Suit_Letter_Case_Exception()
        {
            ICard card = new Card(12, 's');
        }
    }
}
