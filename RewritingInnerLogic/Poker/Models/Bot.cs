namespace Poker.Models
{
    using System;
    using Poker.Enumerations;
    using Poker.TestingAlgorithms;

    /// <summary>
    /// Class Bot.
    /// </summary>
    public class Bot : Participant
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Bot"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="placeOnBoard">The place on board.</param>
        public Bot(string name, int placeOnBoard)
            : base(name, placeOnBoard)
        {
        }

        /// <summary>
        /// Plays the turn.
        /// </summary>
        /// <param name="currentHighestBet">The current highest bet.</param>
        /// <param name="playersNotFolded">The players not folded.</param>
        /// <param name="canCheck">if set to <c>true</c> [can check].</param>
        /// <param name="currentPartOfTurn">The current part of turn.</param>
        /// <param name="randomBehavior">The random behavior.</param>
        public override void PlayTurn(ref int currentHighestBet, int playersNotFolded, bool canCheck, TurnParts currentPartOfTurn, Random randomBehavior)
        {
            CardPowerCalculator.GetCurrentStrengthOfCards(this.Hand);
            int feelingLucky = randomBehavior.Next(0, 100);
            int bluff = randomBehavior.Next(0, 10);
            int turnPartFactor = (int)currentPartOfTurn * 50;

            if (this.CheckShouldRaise(currentHighestBet, playersNotFolded, turnPartFactor, feelingLucky, bluff))
            {
                this.Raise(currentHighestBet * 2, ref currentHighestBet);
            }
            else if (this.CheckShouldCall(currentHighestBet, playersNotFolded, turnPartFactor, feelingLucky, bluff))
            {
                this.Call(ref currentHighestBet);
            }
            else if (canCheck)
            {
                this.Check();
            }
            else
            {
                this.Fold();
            }
        }

        /// <summary>
        /// Checks that should it raise.
        /// </summary>
        /// <param name="currentHighestBet">The current highest bet.</param>
        /// <param name="playersNotFolded">The players not folded.</param>
        /// <param name="turnPartFactor">The turn part factor.</param>
        /// <param name="feelingLucky">The feeling lucky.</param>
        /// <param name="bluff">The bluff.</param>
        /// <returns><c>true</c> if current highest bet * 2 is smaller than bot current chips and  the bluff is bigger than 8,  <c>false</c> otherwise.</returns>
        private bool CheckShouldRaise(int currentHighestBet, int playersNotFolded, int turnPartFactor, int feelingLucky, int bluff)
        {
            if (currentHighestBet * 2 < this.Chips)
            {
                if (((int)this.Hand.Strength + feelingLucky + turnPartFactor) + (playersNotFolded * 10) > 200)
                {
                    return true;
                }
            }

            return bluff > 8;
        }

        /// <summary>
        /// Checks the should call.
        /// </summary>
        /// <param name="currentHighestBet">The current highest bet.</param>
        /// <param name="playersNotFolded">The players not folded.</param>
        /// <param name="turnPartFactor">The turn part factor.</param>
        /// <param name="feelingLucky">The feeling lucky.</param>
        /// <param name="bluff">The bluff.</param>
        /// <returns><c>true</c> if current highest bet * 2 is smaller than bot current chips and  the bluff is bigger than 6, <c>false</c> otherwise.</returns>
        private bool CheckShouldCall(int currentHighestBet, int playersNotFolded, int turnPartFactor, int feelingLucky, int bluff)
        {
            if (currentHighestBet * 2 < this.Chips)
            {
                if (((int)this.Hand.Strength + feelingLucky + turnPartFactor) + (playersNotFolded * 10) > 130)
                {
                    return true;
                }
            }

            return bluff > 6;
        }
    }
}
