// *****************************************************************************************
// Assembly         : Poker
// Created          : 01-28-2016
//
// Last Modified On : 01-28-2016
// *****************************************************************************************
// <copyright file="PokerTestsAlgorithms.cs" company="Date"> Copyright ©  2015 </copyright>
// *****************************************************************************************


namespace Poker.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Poker.Enumerations;
    using Poker.Globals;
    using Poker.Interfaces;
    using Poker.Models.Cards;
    using Poker.Models.Entities;
    using Poker.Models;
    using Poker.PowerCalculator;

    /// <summary>
    /// Class PokerTestsAlgorithms.
    /// </summary>
    [TestClass]
    public class PokerTestsAlgorithms
    {
        /// <summary>
        /// Testings the algorithm_ calculate_ card_ power_ four of a kind_ should pass.
        /// </summary>
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

        /// <summary>
        /// Testings the algorithm_ calculate_ card_ power_ royal flush_ should pass.
        /// </summary>
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

        /// <summary>
        /// Testings the algorithm_ calculate_ card_ power_ full house pair first_ should pass.
        /// </summary>
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

        /// <summary>
        /// Testings the algorithm_ calculate_ card_ power_ full house three of a kind first_ should pass.
        /// </summary>
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

        /// <summary>
        /// Testings the algorithm_ calculate_ card_ power_ three of a kind_ should pass.
        /// </summary>
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

        /// <summary>
        /// Testings the algorithm_ calculate_ card_ power_ two pair_ should pass.
        /// </summary>
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

        /// <summary>
        /// Testings the algorithm_ calculate_ card_ power_ pair_ should pass.
        /// </summary>
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

        /// <summary>
        /// Testings the algorithm_ calculate_ card_ power_ flush_ should pass.
        /// </summary>
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

        /// <summary>
        /// Testings the algorithm_ calculate_ card_ power_ flush not sequential_ should pass.
        /// </summary>
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

        /// <summary>
        /// Testings the algorithm_ calculate_ card_ power_ straight_ should pass.
        /// </summary>
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

        /// <summary>
        /// Testings the algorithm_ calculate_ card_ power_ high card_ should pass.
        /// </summary>
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

        /// <summary>
        /// Testings the algorithm_ calculate_ card_ power_ straight flush_ should pass.
        /// </summary>
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

        /// <summary>
        /// Testings the algorithm_ calculate_ high card_ full house three of a kind first_ should pass.
        /// </summary>
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

        /// <summary>
        /// Testings the algorithm_ calculate_ high card_ full house pair first_ should pass.
        /// </summary>
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

        /// <summary>
        /// Testings the algorithm_ calculate_ high card_ two pair_ should pass.
        /// </summary>
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

        /// <summary>
        /// Testings the algorithm_ calculate_ high card_ three of a kind_ should pass.
        /// </summary>
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

        /// <summary>
        /// Testings the algorithm_ calculate_ high card_ pair_ should pass.
        /// </summary>
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

        /// <summary>
        /// Testings the algorithm_ calculate_ high card_ flush_ should pass.
        /// </summary>
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

        /// <summary>
        /// Testings the algorithm_ calculate_ high card_ straight_ should pass.
        /// </summary>
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

        /// <summary>
        /// Testings the algorithm_ calculate_ high card_ straight flush_ should pass.
        /// </summary>
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

        /// <summary>
        /// Testings the algorithm_ calculate_ high card_ four of a kind_ should pass.
        /// </summary>
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

        /// <summary>
        /// Testings the algorithm_ calculate_ high card_ royal flush_ should pass.
        /// </summary>
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

        /// <summary>
        /// Testings the algorithm_ calculate_ high card_ high card power_ should pass.
        /// </summary>
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

        /// <summary>
        /// Testings the algorithm_ calculate_ high card power_2.
        /// </summary>
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

        /// <summary>
        /// Testings the algorithm_ check_ deck_ initializes cards correctly_ should pass.
        /// </summary>
        [TestMethod]
        public void TestingAlgorithm_Check_Deck_InitializesCardsCorrectly_ShouldPass()
        {
            IDeck deck = new Deck();
            int differentCardsCounter = deck.Cards.Count(card => deck.Cards.Count(c => c.Rank == card.Rank && c.Suit == card.Suit) == 1);
            Assert.AreEqual(52, differentCardsCounter);
        }

        /// <summary>
        /// Test_s the deck_ shuffle_ randomness.
        /// </summary>
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

        /// <summary>
        /// Tests the length of the deck_.
        /// </summary>
        [TestMethod]
        public void TestDeck_Length()
        {
            Deck deck = new Deck();
            Assert.AreEqual(deck.Cards.Length, 52);
        }

        /// <summary>
        /// Test_s the global_ constants.
        /// </summary>
        [TestMethod]
        public void Test_Global_Constants()
        {
            Assert.AreEqual(GlobalConstants.StartingBigBlind, 500);
            Assert.AreEqual(GlobalConstants.StartingChips, 10000);
            Assert.AreEqual(GlobalConstants.StartingSmallBlind, 250);
        }

        /// <summary>
        /// Test_s the name of the bot_.
        /// </summary>
        [TestMethod]
        public void Test_Bot_Name()
        {
            Bot bot = new Bot("go6o", GlobalVariables.Bot2PlaceOnBoard);
            Assert.AreEqual("go6o", bot.Name);
        }

        /// <summary>
        /// Test_s the bot_ chips.
        /// </summary>
        [TestMethod]
        public void Test_Bot_Chips()
        {
            Bot bot = new Bot("go6o", GlobalVariables.Bot2PlaceOnBoard);
            Assert.AreEqual(GlobalConstants.StartingChips, bot.Chips);
        }
        /// <summary>
        /// Testing_s the card_ rank_ exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Testing_Card_Rank_Exception()
        {
            ICard card1 = new Card(33, 'S');
        }
        /// <summary>
        /// Test_s the card_ suit_ exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Card_Suit_Exception()
        {
            ICard card = new Card(12, 'M');
        }
        /// <summary>
        /// Test_s the card_ suit_ letter_ case_ exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Card_Suit_Letter_Case_Exception()
        {
            ICard card = new Card(12, 's');
        }
    }
}
