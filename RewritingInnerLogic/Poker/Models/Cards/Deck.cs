namespace Poker.Models.Cards
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;

    using Poker.Globals;
    using Poker.Interfaces;
    using Poker.Models.Entities;

    /// <summary>
    /// Class Deck.
    /// </summary>
    public class Deck : IDeck
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Deck"/> class.
        /// </summary>
        public Deck()
        {
            this.Cards = this.InitializeDeck();
        }

        /// <summary>
        /// Gets or sets the cards array.
        /// </summary>
        /// <value>The cards.</value>
        public ICard[] Cards { get; set; }

        /// <summary>
        /// Shuffle the deck with Fisher-Yates shuffle algorithm
        /// </summary>
        public void Shuffle()
        {
            Random rnd = new Random();
            int cardsLength = this.Cards.Length;
            while (cardsLength > 1)
            {
                cardsLength--;
                int nextCard = rnd.Next(cardsLength + 1);
                ICard value = this.Cards[nextCard];
                this.Cards[nextCard] = this.Cards[cardsLength];
                this.Cards[cardsLength] = value;
            }
        }

        /// <summary>
        /// Deals the specified players.
        /// </summary>
        /// <param name="players">The players.</param>
        /// <param name="cardsOnBoard">The cards on board.</param>
        public void Deal(IList<IParticipant> players, ICard[] cardsOnBoard)
        {
            this.Shuffle();

            int toTakeFromDeckIndex = 0;

            // deal cards to players
            foreach (IParticipant player in players.Where(p => p.IsInGame))
            {
                this.DealCardsToPlayers(player, ref toTakeFromDeckIndex);
            }

            // place cards on board
            Point boardCardsPosition = GlobalVariables.BoardCardsPlace;
            int positionCardChangeX = boardCardsPosition.X;
            for (int i = 0; i < 5; i++)
            {
                this.DealCardsOnBoard(
                    cardsOnBoard, 
                    i, 
                    ref toTakeFromDeckIndex, 
                    boardCardsPosition, 
                    ref positionCardChangeX);
            }

            // turn the player cards up
            foreach (var player in players.Where(p => p.IsInGame && p is Player))
            {
                player.Hand.CurrentCards[0].IsFacingUp = true;
                player.Hand.CurrentCards[1].IsFacingUp = true;
            }               
        }

        /// <summary>
        /// Places 5 cards on the board facing down.
        /// </summary>
        /// <param name="cardsOnBoard">An array of 5 which will be filled with the proper cards</param>
        /// <param name="i">the index in the array of cards to be filled</param>
        /// <param name="toTakeFromDeckIndex">the index from which to take a card from the deck and place it on board</param>
        /// <param name="boardCardsPosition">the position on which to draw the card</param>
        /// <param name="positionCardChangeX">the position that changes for the next card to be drawn at</param>
        private void DealCardsOnBoard(
            ICard[] cardsOnBoard,
            int i,
            ref int toTakeFromDeckIndex,
            Point boardCardsPosition,
            ref int positionCardChangeX)
        {
            cardsOnBoard[i] = this.Cards[toTakeFromDeckIndex];
            cardsOnBoard[i].PictureBox.Visible = true;
            toTakeFromDeckIndex += 1;
            Point location = new Point(positionCardChangeX, boardCardsPosition.Y);
            cardsOnBoard[i].PictureBox.Location = location;
            positionCardChangeX += 90;

            cardsOnBoard[i].PictureBox.Update();
            Thread.Sleep(300);
        }

        /// <summary>
        /// Deals 2 cards to a player.
        /// </summary>
        /// <param name="player">The player to be dealt cards to</param>
        /// <param name="toTakeFromDeckIndex">The index in the deck of the card to be dealt to player</param>
        private void DealCardsToPlayers(IParticipant player, ref int toTakeFromDeckIndex)
        {
            player.Hand.CurrentCards.Add(this.Cards[toTakeFromDeckIndex]);
            player.Hand.CurrentCards[0].PictureBox.Location = player.PlaceOnBoard;
            player.Hand.CurrentCards[0].PictureBox.Visible = true;
            player.Hand.CurrentCards[0].PictureBox.Update();
            Thread.Sleep(300);
            toTakeFromDeckIndex += 1;

            player.Hand.CurrentCards.Add(this.Cards[toTakeFromDeckIndex]);
            player.Hand.CurrentCards[1].PictureBox.Location = new Point(player.PlaceOnBoard.X + 75, player.PlaceOnBoard.Y);
            player.Hand.CurrentCards[1].PictureBox.Visible = true;
            player.Hand.CurrentCards[1].PictureBox.Update();
            Thread.Sleep(300);
            toTakeFromDeckIndex += 1;
        }

        /// <summary>
        /// Initializes the deck.
        /// </summary>
        /// <returns>An array of 52 initialized different cards.</returns>
        private ICard[] InitializeDeck()
        {
            ICard[] cards = new ICard[52];
            int cardCounter = 0;

            this.InitilializeEachCard(cards, cardCounter);

            this.SetCorrectImagesToCards(cards);
           
            return cards;
        }

        /// <summary>
        /// Sets each card of the deck an image for the front and back side of the card.
        /// </summary>
        /// <param name="cards">the deck of cards (array) to be set images at</param>
        private void SetCorrectImagesToCards(ICard[] cards)
        {
            foreach (var card in cards)
            {
                string path;
                string ending = string.Empty;
                switch (card.Suit)
                {
                    case 'S':
                        ending = "_of_spades.png";
                        break;
                    case 'D':
                        ending = "_of_diamonds.png";
                        break;
                    case 'H':
                        ending = "_of_hearts.png";
                        break;
                    case 'C':
                        ending = "_of_clubs.png";
                        break;
                }

                if (card.Rank != 12)
                {
                    path = (card.Rank + 2) + ending;
                }
                else
                {
                    path = "1" + ending;
                }

                card.FrontImage = Image.FromFile(@"..\..\..\Poker\Resources\Cards\" + path);
                card.BackImage = Image.FromFile(@"..\..\..\Poker\Resources\Assets\Back\Back.png");
                card.PictureBox = new PictureBox
                                      {
                                          Image = card.BackImage,
                                          Height = 120,
                                          Width = 70,
                                          SizeMode = PictureBoxSizeMode.StretchImage
                                      };
            }
        }

        /// <summary>
        /// Initializes each card in an array of cards. Each card will be different
        /// </summary>
        /// <param name="cards">Array of cards to initialize. Will result in 52 different cards</param>
        /// <param name="cardCounter">The index in the array to initialize</param>
        private void InitilializeEachCard(ICard[] cards, int cardCounter)
        {
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    switch (j)
                    {
                        case 0:
                            cards[cardCounter] = new Card(i, 'S');
                            break;
                        case 1:
                            cards[cardCounter] = new Card(i, 'H');
                            break;
                        case 2:
                            cards[cardCounter] = new Card(i, 'D');
                            break;
                        case 3:
                            cards[cardCounter] = new Card(i, 'C');
                            break;
                    }

                    cardCounter += 1;
                }
            }
        }
    }
}