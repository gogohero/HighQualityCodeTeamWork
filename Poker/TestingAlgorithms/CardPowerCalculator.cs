﻿namespace Poker.TestingAlgorithms
{
    using System.Collections.Generic;
    using System.Linq;

    using Interfaces;

    public static class CardPowerCalculator
    {
        public static void CompareAllSetsOfCardsOnTheBoard(IDictionary<Participant, IEnumerable<ICard>> eachPlayerVisibleCards) 
        {
        }

        // method for setting the strength of any participant current visible hand (his cards in hand + facing up cards from the board)
        public static void GetCurrentStrengthOfCards(IHand participantVisibleHand)
        {
            ICard[] startingTwoCards = 
                 {
                    participantVisibleHand.CurrentCards[0],
                    participantVisibleHand.CurrentCards[1]
                 };

            IList<ICard> differentCards = new List<ICard>();

            foreach (var card in participantVisibleHand.CurrentCards)
            {
                if (differentCards.Any(c => c.Rank == card.Rank))
                {
                    continue;
                }

                differentCards.Add(card);
            }

            // Check for pair, two pair, three of a kind, full house or four of a kind
            for (int i = 0; i < differentCards.Count; i++)
            {
                int pairedCardsCounter = participantVisibleHand.CurrentCards.Count(card => card.Rank == differentCards[i].Rank);

                if (pairedCardsCounter > 1)
                {
                    // four of a kind check
                    if (pairedCardsCounter == 4)
                    {
                        participantVisibleHand.Strength = HandStrengthEnum.FourOfAKind;
                        participantVisibleHand.HighCard = differentCards[i];
                        break;
                    }

                    // three of a kind check
                    if (pairedCardsCounter == 3 && (int)participantVisibleHand.Strength < pairedCardsCounter)
                    {
                        participantVisibleHand.Strength  = HandStrengthEnum.ThreeOfAKind;
                        participantVisibleHand.HighCard = differentCards[i];
                    }

                    // check full house -> if not check two pair -> if not results in simple pair
                    if (pairedCardsCounter == 2 
                        && participantVisibleHand.Strength == HandStrengthEnum.ThreeOfAKind 
                        && (int)participantVisibleHand.Strength < (int)HandStrengthEnum.FourOfAKind)
                    {
                        participantVisibleHand.Strength = HandStrengthEnum.FullHouse;
                    }
                    else if (participantVisibleHand.Strength == HandStrengthEnum.Pair)
                    {
                        participantVisibleHand.Strength = HandStrengthEnum.TwoPair;
                        if (participantVisibleHand.HighCard.Rank < differentCards[i].Rank)
                        {
                            participantVisibleHand.HighCard = differentCards[i];
                        }
                    }
                    else
                    {
                        participantVisibleHand.Strength = HandStrengthEnum.Pair;
                        participantVisibleHand.HighCard = differentCards[i];
                    }
                }
            }

            // check for straigth, flush, straigth flush and royal flush
            participantVisibleHand.CurrentCards = participantVisibleHand.CurrentCards.OrderBy(c => c.Rank).ToList();
            int sequentialCards = 0;
            int sameSuites = 0;
            ICard highestInSequence = null;

            for (int i = 1; i < participantVisibleHand.CurrentCards.Count - 1; i++)
            {
                if (participantVisibleHand.CurrentCards[i].Rank + 1 == participantVisibleHand.CurrentCards[i + 1].Rank
                    || participantVisibleHand.CurrentCards[i].Rank - 1 == participantVisibleHand.CurrentCards[i - 1].Rank)
                {
                    sequentialCards += 1;
                    highestInSequence = participantVisibleHand.CurrentCards[i];
                    if (participantVisibleHand.CurrentCards[i].Suit == participantVisibleHand.CurrentCards[i + 1].Suit)
                    {
                        sameSuites += 1;
                    }
                }
                else if (participantVisibleHand.CurrentCards[i].Suit == participantVisibleHand.CurrentCards[i + 1].Suit
                        || participantVisibleHand.CurrentCards[i].Suit == participantVisibleHand.CurrentCards[i - 1].Suit)
                {
                    sameSuites += 1;
                    if (highestInSequence.Rank < participantVisibleHand.CurrentCards[i].Rank)
                    {
                        highestInSequence = participantVisibleHand.CurrentCards[i];
                    }
                }

                if (sequentialCards < 4 && sameSuites < 4)
                {
                    highestInSequence = participantVisibleHand.CurrentCards[i + 1];
                }
            }

            // royal flush check -> if not check straight flush -> if not check flush -> if not check straigth
            if (sequentialCards >= 5 && sameSuites >= 5)
            {
                if (highestInSequence.Rank == 12)
                {
                    participantVisibleHand.Strength = HandStrengthEnum.RoyalFlush;
                    participantVisibleHand.HighCard = highestInSequence;
                }
                else
                {
                    participantVisibleHand.Strength = HandStrengthEnum.StraightFlush;
                    participantVisibleHand.HighCard = highestInSequence;
                }
            }
            else if (sameSuites >= 5 && (int)participantVisibleHand.Strength < (int)HandStrengthEnum.Flush)
            {
                participantVisibleHand.Strength = HandStrengthEnum.Flush;
                participantVisibleHand.HighCard = highestInSequence;
            }
            else if (sequentialCards >= 5 && (int)participantVisibleHand.Strength < (int)HandStrengthEnum.Straight)
            {
                participantVisibleHand.Strength = HandStrengthEnum.Straight;
                participantVisibleHand.HighCard = highestInSequence;
            }
            else if ((int)participantVisibleHand.Strength < (int)HandStrengthEnum.Pair)
            {
                // last check is if no winning set of cards has been found -> set the highest card to the highest in the whole hand (in player hand and board)
                participantVisibleHand.HighCard = startingTwoCards[0].Rank > startingTwoCards[1].Rank
                                                      ? startingTwoCards[0]
                                                      : startingTwoCards[1];
                participantVisibleHand.Strength = HandStrengthEnum.HighCard;
            }
        }
    }
}