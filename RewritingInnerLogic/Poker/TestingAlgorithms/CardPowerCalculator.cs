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
            foreach (var player in players.Where(p => !p.HasFolded && p.IsInGame))
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
            IList<ICard> cards = participantVisibleHand.CurrentCards;

            // ---------------------------------------------------------
            // check for straight, flush, straight flush and royal flush

            cards = cards.OrderBy(c => c.Rank).ToList();
            int sequentialCards = 0;
            int spades = 0;
            int diamonds = 0;
            int clubs = 0;
            int hearts = 0;
            ICard highestInSequenceWithoutPairs = new Card(0, 'S');
            HandStrengthEnum strengthWithoutPairs = HandStrengthEnum.HighCard;

            for (int i = 1; i < cards.Count - 1; i++)
            {
                bool foundHigherSequential = false;

                // cannot escape the checks for first and last element, otherwise they are skipped
                if (i == 1)
                {
                    if (cards[i - 1].Rank + 1 == cards[i].Rank)
                    {
                        sequentialCards += 1;
                    }
                }
                else if (i == cards.Count - 2)
                {
                    if (cards[i].Rank + 1 == cards[i + 1].Rank)
                    {
                        sequentialCards += 1;
                        foundHigherSequential = true;
                    }
                }
                if (cards[i].Rank + 1
                        == cards[i + 1].Rank
                        || cards[i].Rank - 1
                        == cards[i - 1].Rank)
                {
                    sequentialCards += 1;
                    highestInSequenceWithoutPairs = cards[i];
                    if (cards[i].Rank + 1 == cards[i + 1].Rank)
                    {
                        foundHigherSequential = true;
                    }
                }

                if (foundHigherSequential)
                {
                    highestInSequenceWithoutPairs = cards[i + 1];
                }
                else
                {
                    sequentialCards = 0;
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
            if (sequentialCards >= 5 && maximumSameSuitCards >= 5)
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
            else if (sequentialCards >= 5)
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