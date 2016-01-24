namespace Poker.TestingAlgorithms
{
    using System;
    using System.Collections.Generic;
    using System.Deployment.Application;
    using System.IO;
    using System.Linq;

    using Poker.Interfaces;

    public class Board
    {
        private int turn;

        private int pot;

        string[] allGameCardsImagesWithLocationsCollection = Directory.GetFiles(@"..\..\..\Poker\Resources\Assets\Cards\", "*.png", SearchOption.TopDirectoryOnly);

        public Board(IList<IParticipant> players, ICard[] deck)
        {            
            this.Deck = deck;
            this.Players = players;
        }

        public IEnumerable<ICard> Deck { get; }

        public IEnumerable<IParticipant> Players { get; }

        public BoardState State { get; set; }


        public int Turn
        {
            get
            {
                return this.turn;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Turn cannot be negative number");
                }

                this.turn = value;
            }
        }

        public void Update()
        {
            if (this.State == BoardState.Flop)
            {
                this.FlipCard(3);
            }
            else if (this.State == BoardState.Turn)
            {
                this.FlipCard(1);
            }
            else if (this.State == BoardState.River
                       && !this.Players.Any(p => p.HasRaised))
            {
                this.FlipCard(1);
            }
            else if (this.State == BoardState.End)
            {
                this.ProcessWinnings();
            }
        }

        public void AddPlayers(IParticipant player)
        {
            this.Players.ToList().Add(player);
        }

        public void RemovePlayers(IParticipant player)
        {
            this.Players.ToList().Remove(player);
        }

        private void ProcessWinnings()
        {

        }

        private void FlipCard(int cardsToFlip)
        {
        }

        private void DealInitialCards()
        {
        }

        private void Shuffle()
        {
        }

        private void LoadCard()
        {
        }
    }
}