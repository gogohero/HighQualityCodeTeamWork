using System.Windows.Forms;
namespace Poker.Models
{
    using System;
    using System.Threading.Tasks;

    using Poker.Enumerations;
    using Poker.Interfaces;
    using Poker.TestingAlgorithms;

    public class Bot : Participant
    {
        public Bot(string name, int placeOnBoard)
            : base(name, placeOnBoard)
        {
        }

        public override void PlayTurn(ref int currentHighestBet, int playersNotFolded, bool canCheck, TurnParts currentPartOfTurn, Random randomBehavior)
        {
            CardPowerCalculator.GetCurrentStrengthOfCards(this.Hand);
            int feelingLucky = randomBehavior.Next(0, 100);
            int bluff = randomBehavior.Next(0, 10);
            int turnPartFactor = (int)currentPartOfTurn * 50;

            if (this.CheckShouldRaise(currentHighestBet, playersNotFolded, turnPartFactor, feelingLucky, bluff))
            {
                this.Raise(currentHighestBet*2, ref currentHighestBet);
            }
            else if (this.CheckShouldCall(currentHighestBet, playersNotFolded, turnPartFactor, feelingLucky,                                                                            bluff))
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

        private bool CheckShouldRaise(int currentHighestBet, int playersNotFolded, int turnPartFactor, int feelingLucky, int bluff)
        {
            if ((currentHighestBet) * 2 < this.Chips)
            {
                if (((int)this.Hand.Strength + feelingLucky + turnPartFactor) + (playersNotFolded * 10) > 200)
                {
                    return true;
                }
            }
            return bluff > 8;
        }

        private bool CheckShouldCall(int currentHighestBet, int playersNotFolded, int turnPartFactor, int feelingLucky, int bluff)
        {
            if ((currentHighestBet) * 2 < this.Chips)
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
