namespace Poker.TestingAlgorithms
{
    using System.Collections.Generic;
    using System.Linq;

    using Interfaces;

    public static class CardPowerCalculator
    {
        public static void CompareAllSetsOfCardsOnTheBoard(IList<IParticipant> players) 
        {
            foreach (var player in players)
            {
                GetCurrentStrengthOfCards(player.Hand);
            }

            foreach (var player in players)
            {
                if (players.Any(p => p.Hand.Strength > player.Hand.Strength))
                {
                    player.WinsRound = false;
                }
                else if (players.Where(p => p.Hand.Strength == player.Hand.Strength).Any(p => p.Hand.HighCard.Rank > player.Hand.HighCard.Rank))
                {
                    player.WinsRound = false;
                }
                else
                {
                    player.WinsRound = true;
                }
            }
        }

        // method for setting the strength of any participant current visible hand (his cards in hand + facing up cards from the board)
        public static void GetCurrentStrengthOfCards(IHand participantVisibleHand)
        {
            // getting initial 2 cards in hand
            ICard[] startingTwoCards = 
                 {
                    participantVisibleHand.CurrentCards[0],
                    participantVisibleHand.CurrentCards[1]
                 };

            // ---------------------------------------------------------
            // check for straigth, flush, straigth flush and royal flush

            participantVisibleHand.CurrentCards = participantVisibleHand.CurrentCards.OrderBy(c => c.Rank).ToList();
            int sequentialCards = 0;
            int sameSuites = 0;
            ICard highestInSequenceWithoutPairs = new Card(0, 'S');
            HandStrengthEnum strengthWithoutPairs = HandStrengthEnum.HighCard;

            for (int i = 1; i < participantVisibleHand.CurrentCards.Count - 1; i++)
            {
                bool foundHigherSequential = false;

                // cannot escape the checks for first and last element, otherwise they are skipped
                if (i == 1)
                {
                    if (participantVisibleHand.CurrentCards[i - 1].Rank + 1 == participantVisibleHand.CurrentCards[i].Rank)
                    {
                        sequentialCards += 1;
                    }
                    if (participantVisibleHand.CurrentCards[i - 1].Suit == participantVisibleHand.CurrentCards[i].Suit)
                    {
                        sameSuites += 1;
                    }
                }
                else if (i == participantVisibleHand.CurrentCards.Count - 2)
                {
                    if (participantVisibleHand.CurrentCards[i].Rank + 1 == participantVisibleHand.CurrentCards[i + 1].Rank)
                    {
                        sequentialCards += 1;
                        foundHigherSequential = true;
                    }
                    if (participantVisibleHand.CurrentCards[i].Suit == participantVisibleHand.CurrentCards[i].Suit)
                    {
                        sameSuites += 1;
                    }
                }
                if (participantVisibleHand.CurrentCards[i].Rank + 1
                        == participantVisibleHand.CurrentCards[i + 1].Rank
                        || participantVisibleHand.CurrentCards[i].Rank - 1
                        == participantVisibleHand.CurrentCards[i - 1].Rank)
                {
                    sequentialCards += 1;
                    highestInSequenceWithoutPairs = participantVisibleHand.CurrentCards[i];
                    if (participantVisibleHand.CurrentCards[i].Rank + 1 == participantVisibleHand.CurrentCards[i + 1].Rank)
                    {
                        foundHigherSequential = true;
                    }
                    if (participantVisibleHand.CurrentCards[i].Suit
                        == participantVisibleHand.CurrentCards[i + 1].Suit)
                    {
                        sameSuites += 1;
                    }
                }
                else if (participantVisibleHand.CurrentCards[i].Suit
                         == participantVisibleHand.CurrentCards[i + 1].Suit
                         || participantVisibleHand.CurrentCards[i].Suit
                         == participantVisibleHand.CurrentCards[i - 1].Suit)
                {
                    sameSuites += 1;
                    if (highestInSequenceWithoutPairs.Rank < participantVisibleHand.CurrentCards[i].Rank)
                    {
                        highestInSequenceWithoutPairs = participantVisibleHand.CurrentCards[i];
                    }
                }

                if (foundHigherSequential)
                {
                    highestInSequenceWithoutPairs = participantVisibleHand.CurrentCards[i + 1];
                }
            }

            // royal flush check -> if not check straight flush -> if not check flush -> if not check straigth
            if (sequentialCards >= 5 && sameSuites >= 5)
            {
                if (highestInSequenceWithoutPairs.Rank == 12)
                {
                    strengthWithoutPairs = HandStrengthEnum.RoyalFlush;
                }
                else
                {
                    strengthWithoutPairs = HandStrengthEnum.StraightFlush;
                }
            }
            else if (sameSuites >= 5 && (int)participantVisibleHand.Strength < (int)HandStrengthEnum.Flush)
            {
                strengthWithoutPairs = HandStrengthEnum.Flush;
            }
            else if (sequentialCards >= 5 && (int)participantVisibleHand.Strength < (int)HandStrengthEnum.Straight)
            {
                strengthWithoutPairs = HandStrengthEnum.Straight;
            }

            // ----------------------------------------------------------
            // begin search for pairs, two pairs, three of a kind, full house, four of a kind
            IList<ICard> differentCards = new List<ICard>();

            foreach (var card in participantVisibleHand.CurrentCards)
            {
                if (differentCards.Any(c => c.Rank == card.Rank))
                {
                    continue;
                }

                differentCards.Add(card);
            }

            HandStrengthEnum strengthPairs = HandStrengthEnum.HighCard;
            ICard highestCardPairs = new Card(0, 'S');

            // Check for pair, two pair, three of a kind, full house or four of a kind
            for (int i = 0; i < differentCards.Count; i++)
            {
                int pairedCardsCounter = participantVisibleHand.CurrentCards.Count(card => card.Rank == differentCards[i].Rank);

                if (pairedCardsCounter > 1)
                {
                    // four of a kind check
                    if (pairedCardsCounter == 4)
                    {
                        strengthPairs = HandStrengthEnum.FourOfAKind;
                        highestCardPairs = differentCards[i];
                        break;
                    }


                    // check full house -> if not check two pair -> if not results in simple pair
                    if (pairedCardsCounter == 3 
                        && (strengthPairs == HandStrengthEnum.Pair
                        || strengthPairs == HandStrengthEnum.TwoPair)
                        && strengthPairs < HandStrengthEnum.FourOfAKind)
                    {
                        strengthPairs = HandStrengthEnum.FullHouse;
                        highestCardPairs = differentCards[i];
                    }
                    else if (pairedCardsCounter == 3 && (int)strengthPairs < pairedCardsCounter)
                    {
                        strengthPairs = HandStrengthEnum.ThreeOfAKind;
                        highestCardPairs = differentCards[i];
                    }
                    else if (pairedCardsCounter == 2
                            && strengthPairs == HandStrengthEnum.ThreeOfAKind
                            && strengthPairs < HandStrengthEnum.FourOfAKind)
                    {
                        strengthPairs = HandStrengthEnum.FullHouse;
                    }
                    else if (strengthPairs == HandStrengthEnum.Pair)
                    {
                        strengthPairs = HandStrengthEnum.TwoPair;
                        if (highestCardPairs.Rank < differentCards[i].Rank)
                        {
                            highestCardPairs = differentCards[i];
                        }
                    }
                    else if(strengthPairs < HandStrengthEnum.Pair)
                    {
                        strengthPairs= HandStrengthEnum.Pair;
                        highestCardPairs = differentCards[i];
                    }
                }
            }

            if (strengthPairs > strengthWithoutPairs)
            {
                participantVisibleHand.Strength = strengthPairs;
                participantVisibleHand.HighCard = highestCardPairs;
            }
            else if (strengthWithoutPairs > strengthPairs)
            {
                participantVisibleHand.Strength = strengthWithoutPairs;
                participantVisibleHand.HighCard = highestInSequenceWithoutPairs;
            }
            else
            {
                participantVisibleHand.Strength = HandStrengthEnum.HighCard;
                if (highestCardPairs.Rank >= highestInSequenceWithoutPairs.Rank)
                {
                    participantVisibleHand.HighCard = highestCardPairs;
                }
                else
                {
                    participantVisibleHand.HighCard = highestInSequenceWithoutPairs;
                }
            }
        }
    }
}