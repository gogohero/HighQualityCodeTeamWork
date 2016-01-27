namespace Poker.TestingAlgorithms
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Interfaces;

    public static class CardPowerCalculator
    {
        public static void CompareAllSetsOfCardsOnTheBoard(IList<IParticipant> players) 
        {
            foreach (var player in players.Where(p => !p.HasFolded))
            {
                GetCurrentStrengthOfCards(player.Hand);
            }

            foreach (var player in players)
            {
                if (players.Any(p => p.Hand.Strength > player.Hand.Strength))
                {
                    player.WinsRound = false;
                }
                else if (players.Where(p => p.Hand.Strength == player.Hand.Strength).Any(p => p.Hand.HighCard.Rank < player.Hand.HighCard.Rank))
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
            IList<ICard> cards = participantVisibleHand.CurrentCards;
            if (!cards.Any())
            {
                return;
            }

            // ---------------------------------------------------------
            // check for straight, flush, straight flush and royal flush

            cards = cards.OrderBy(c => c.Rank).ToList();
            IDictionary<int, IList<int>> sequentialCards = new Dictionary<int, IList<int>>();
            int spades = 0;
            int diamonds = 0;
            int clubs = 0;
            int hearts = 0;
            ICard highestInSequenceWithoutPairs = new Card(0, 'S');
            HandStrengthEnum strengthWithoutPairs = HandStrengthEnum.HighCard;

            for (int i = 0; i < cards.Count; i++)
            {
                sequentialCards.Add(i, new List<int>());
                for (int j = i; j < cards.Count - 1; j++)
                {
                    if (j != cards.Count - 2)
                    {
                        if (cards[j].Rank + 1 == cards[j + 1].Rank)
                        {
                            sequentialCards[i].Add(cards[j].Rank);
                        }
                        else
                        {
                            sequentialCards[i].Add(cards[j].Rank);
                            break;
                        }
                    }
                    else
                    {
                        if (cards[j].Rank + 1 == cards[j + 1].Rank)
                        {
                            sequentialCards[i].Add(cards[j].Rank);
                            sequentialCards[i].Add(cards[j + 1].Rank);
                        }
                        else if (cards[j].Rank - 1 == cards[j - 1].Rank)
                        {
                            sequentialCards[i].Add(cards[j].Rank);
                            break;
                        }
                    }
                }
            }

            int highestSequenceOfRanksDetected = 0;
            for (int i = 0; i < sequentialCards.Keys.Count; i++)
            {
                if (sequentialCards[i].Count >= highestSequenceOfRanksDetected)
                {
                    highestSequenceOfRanksDetected = sequentialCards[i].Count;
                    highestInSequenceWithoutPairs = new Card(sequentialCards[i].Max(), 'S');
                }
            }

            for (int i = 0; i < cards.Count; i++)
            {
                switch (cards[i].Suit)
                {
                    case 'S':
                        spades += 1;
                        break;
                    case 'D':
                        diamonds += 1;
                        break;
                    case 'C':
                        clubs += 1;
                        break;
                    case 'H':
                        hearts += 1;
                        break;
                }
            }

            int maximumSameSuitCards = Math.Max(
                Math.Max(diamonds, spades),
                Math.Max(hearts, clubs));

            // royal flush check -> if not check straight flush -> if not check flush -> if not check straight
            if (highestSequenceOfRanksDetected >= 5 && maximumSameSuitCards >= 5)
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
            else if (maximumSameSuitCards >= 5)
            {
                strengthWithoutPairs = HandStrengthEnum.Flush;
            }
            else if (highestSequenceOfRanksDetected >= 5)
            {
                strengthWithoutPairs = HandStrengthEnum.Straight;
            }

            // ----------------------------------------------------------
            // begin search for pairs, two pairs, three of a kind, full house, four of a kind
            IList<ICard> differentCards = new List<ICard>();

            foreach (var card in cards)
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
                int pairedCardsCounter = cards.Count(card => card.Rank == differentCards[i].Rank);

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
                        strengthPairs = HandStrengthEnum.Pair;
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
                if (participantVisibleHand.CurrentCards[0].Rank >= participantVisibleHand.CurrentCards[1].Rank)
                {
                    participantVisibleHand.HighCard = participantVisibleHand.CurrentCards[0];
                }
                else
                {
                    participantVisibleHand.HighCard = participantVisibleHand.CurrentCards[1];
                }
            }
        }
    }
}