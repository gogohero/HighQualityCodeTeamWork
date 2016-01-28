// *************************************************************************************
// Assembly         : Poker
// Created          : 01-28-2016
//
// Last Modified On : 01-28-2016
// *************************************************************************************
// <copyright file="TestDeckAndCards.cs" company="Date"> Copyright ©  2015 </copyright>
// *************************************************************************************
namespace Poker.Tests
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Poker.Globals;
    using Poker.Interfaces;
    using Poker.Models.Cards;
    using Poker.Models.Entities;

    /// <summary>
    /// Class TestDeckAndCards.
    /// </summary>
    [TestClass]
    public class TestDeckAndCards
    {
        /// <summary>
        /// The deck
        /// </summary>
        private IDeck deck;

        /// <summary>
        /// The players
        /// </summary>
        private IList<IParticipant> players;

        /// <summary>
        /// The cards on board
        /// </summary>
        private ICard[] cardsOnBoard;

        /// <summary>
        /// Initializes the deck players and cards.
        /// </summary>
        [TestInitialize]
        public void InitializeDeckPlayersAndCards()
        {
            this.deck = new Deck();
            this.players = new List<IParticipant>();
            this.players.Add(new Player("Meco pug", GlobalVariables.PlayerPlaceOnBoard));
            this.players.Add(new Bot("Isuf", GlobalVariables.Bot1PlaceOnBoard));
            this.players.Add(new Bot("Georgi-Nikol", GlobalVariables.Bot2PlaceOnBoard));
            this.cardsOnBoard = new ICard[5];
        }

        /// <summary>
        /// Tests if the deck_ deals_ correctly.
        /// </summary>
        [TestMethod]
        public void TestDeck_Deals_Correctly()
        {
            this.deck.Deal(this.players, this.cardsOnBoard);
            Assert.IsTrue(this.players.ToList().TrueForAll(p => p.Hand.CurrentCards.Count == 2) 
                            && this.cardsOnBoard.Count(c => c != null) == 5);
        }
    }
}