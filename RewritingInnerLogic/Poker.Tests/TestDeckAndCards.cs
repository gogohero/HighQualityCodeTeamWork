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

    [TestClass]
    public class TestDeckAndCards
    {
        private IDeck deck;

        private IList<IParticipant> players;

        private ICard[] cardsOnBoard;

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

        [TestMethod]
        public void TestDeck_Deals_Correctly()
        {
            this.deck.Deal(this.players, this.cardsOnBoard);
            Assert.IsTrue(this.players.ToList().TrueForAll(p => p.Hand.CurrentCards.Count == 2) 
                            && this.cardsOnBoard.Count(c => c != null) == 5);
        }
    }
}