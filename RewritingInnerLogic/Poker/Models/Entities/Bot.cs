namespace Poker.Models.Entities
{
    using System;
    using System.Drawing;

    using Poker.Enumerations;
    using Poker.Interfaces;
    using Poker.PowerCalculator;

    /// <summary>
    /// Class Bot.
    /// </summary>
    public class Bot : Participant, IBot
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Bot"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="placeOnBoard">The place on board.</param>
        public Bot(string name, Point placeOnBoard)
            : base(name, placeOnBoard)
        {
        }

        /// <summary>
        /// Bot makes calculations and decides how to play his turn based on several factors
        /// </summary>
        /// <param name="currentHighestBet">The current highest bet on the board.</param>
        /// <param name="playersNotFolded">The number of players not folded.</param>
        /// <param name="canCheck">if set to <c>true</c> the bot [can check] 
        /// (true only if no one has called or raised before him and the turn part is past the flop).</param>
        /// <param name="currentPartOfTurn">The current part of the turn.</param>
        /// <param name="randomBehavior">The random behavior that's one of the factors in the bots decision making.</param>
        public void PlayTurn(ref int currentHighestBet, int playersNotFolded, bool canCheck, TurnParts currentPartOfTurn, Random randomBehavior)
        {
            CardPowerCalculator.GetCurrentStrengthOfCards(this.Hand);
            int feelingLucky = randomBehavior.Next(0, 100);
            int bluff = randomBehavior.Next(0, 10);
            int turnPartFactor = (int)currentPartOfTurn * 40;

            if (this.CheckShouldRaise(playersNotFolded, turnPartFactor, feelingLucky, bluff))
            {
                this.Raise(currentHighestBet * 2, ref currentHighestBet);
            }
            else if (this.CheckShouldCall(playersNotFolded, turnPartFactor, feelingLucky, bluff))
            {
                this.Call(currentHighestBet);
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
        /// <param name="playersNotFolded">The players not folded.</param>
        /// <param name="turnPartFactor">The turn part factor.</param>
        /// <param name="feelingLucky">The feeling lucky.</param>
        /// <param name="bluff">The bluff.</param>
        /// <returns><c>true</c> if current highest bet * 2 is smaller than bot current chips 
        /// and the bluff is bigger than 7,  <c>false</c> otherwise.</returns>
        private bool CheckShouldRaise(int playersNotFolded, int turnPartFactor, int feelingLucky, int bluff)
        {
            if (((int)this.Hand.Strength + feelingLucky + turnPartFactor) + (playersNotFolded * 10) > 200)
            {
                return true;
            }

            return bluff > 7;
        }

        /// <summary>
        /// Checks the should call.
        /// </summary>
        /// <param name="playersNotFolded">The players not folded.</param>
        /// <param name="turnPartFactor">The turn part factor.</param>
        /// <param name="feelingLucky">The feeling lucky.</param>
        /// <param name="bluff">The bluff.</param>
        /// <returns><c>true</c> if current highest bet * 2 is smaller than bot current chips 
        /// and the bluff is bigger than 5, <c>false</c> otherwise.</returns>
        private bool CheckShouldCall(int playersNotFolded, int turnPartFactor, int feelingLucky, int bluff)
        {
            if (((int)this.Hand.Strength + feelingLucky + turnPartFactor) 
                + (playersNotFolded * 10) > 150)
            {
                return true;
            }

            return bluff > 5;
        }
    }
}
