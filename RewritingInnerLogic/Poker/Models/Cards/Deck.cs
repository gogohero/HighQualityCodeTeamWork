
namespace Poker.Models.Cards
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;

    using Poker.Interfaces;

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
        /// Deals the specified players.
        /// </summary>
        /// <param name="players">The players.</param>
        /// <param name="cardsOnBoard">The cards on board.</param>
        public void Deal(IList<IParticipant> players, ICard[] cardsOnBoard)
        {
            this.Shuffle();
            int toTakeFromDeckIndex = 0;
            foreach (IParticipant player in players.Where(p => p.IsInGame))
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

            Point boardCardsPosition = new Point(300, 180);
            int positionCardChangeX = boardCardsPosition.X;
            for (int i = 0; i < 5; i++)
            {
                cardsOnBoard[i] = this.Cards[toTakeFromDeckIndex];
                cardsOnBoard[i].PictureBox.Visible = true;
                toTakeFromDeckIndex += 1;
                Point location = new Point(positionCardChangeX, boardCardsPosition.Y);
                cardsOnBoard[i].PictureBox.Location = location;
                positionCardChangeX += 90;

                if (i < 3)
                {
                    cardsOnBoard[i].IsFacingUp = true;
                }
                cardsOnBoard[i].PictureBox.Update();
                Thread.Sleep(300);
            }
            foreach (var player in players)
            {
                player.Hand.CurrentCards.Add(cardsOnBoard[0]);
                player.Hand.CurrentCards.Add(cardsOnBoard[1]);
                player.Hand.CurrentCards.Add(cardsOnBoard[2]);
            }
            if (players[0].IsInGame)
            {
                players[0].Hand.CurrentCards[0].IsFacingUp = true;
                players[0].Hand.CurrentCards[1].IsFacingUp = true;
            }
        }

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
        /// Initializes the deck.
        /// </summary>
        /// <returns>ICard[].</returns>
        private ICard[] InitializeDeck()
        {
            ICard[] cards = new ICard[52];
            int cardCounter = 0;

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
           
            return cards;
        }
    }
}