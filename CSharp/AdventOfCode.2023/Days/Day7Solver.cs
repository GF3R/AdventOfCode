namespace AdventOfCode.Twenty23.Days
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode.Days;
    using Core;

    public class Day7Solver : BaseSolver
    {
        public override object SolvePart1(string input)
        {
            var lines = input.Split("\n");
            var hands = lines.Select(l => new Hand(l)).ToList();
            var orderedHands = hands.OrderByDescending(h => h.HandType).ThenByDescending(h => h.Cards[0]).ThenByDescending(h => h.Cards[1]).ThenByDescending(h => h.Cards[2]).ThenByDescending(h => h.Cards[3]).ThenByDescending(h => h.Cards[4]).ToArray();
            var sum = 0;
            var totalHands = orderedHands.Length;

            for (int i = 0; i < totalHands; i++)
            {
                sum += (totalHands - i) * orderedHands[i].BidAmount;
            }

            return sum;
        }

        public override object SolvePart2(string input)
        {
            var lines = input.Split("\n");
            var hands = lines.Select(l => new Hand(l)).ToList();
            foreach (var hand in hands)
            {
                hand.HandType = hand.GetBestPermuationWithoutJokers();
                hand.Cards = hand.Cards.Select(c => c == Card.Jack ? Card.JackAsJoker : c).ToArray();
            }
            
            var orderedHands = hands.OrderByDescending(h => h.HandType).ThenByDescending(h => h.Cards[0]).ThenByDescending(h => h.Cards[1]).ThenByDescending(h => h.Cards[2]).ThenByDescending(h => h.Cards[3]).ThenByDescending(h => h.Cards[4]).ToArray();
            var sum = 0;
            var totalHands = orderedHands.Length;

            for (int i = 0; i < totalHands; i++)
            {
                sum += (totalHands - i) * orderedHands[i].BidAmount;
            }

            return sum;
        }
    }

    public class Hand
    {
        public Card[] Cards { get; set; }
        
        public HandType HandType { get; set; }

        public int BidAmount { get; set; }

        public Hand(string input)
        {
            var splitted = input.Split(" ");
            this.BidAmount = int.Parse(splitted[1]);
            var cardsArray = splitted[0].ToCharArray();
            this.Cards = cardsArray.Select(c => CharToCardMapping[c]).ToArray();
            this.HandType = GetHandType();
        }

        private Hand(Card[] card)
        {
            this.Cards = card;
            this.HandType = GetHandType();
        }

        public HandType GetBestPermuationWithoutJokers()
        {
            if (this.Cards.All(c => c != Card.Jack))
            {
                return this.HandType;
            }
            
            var numberOfJokers = this.Cards.Count(c => c == Card.Jack);
            if (numberOfJokers >= 4)
            {
                return HandType.FiveOfAKind;
            }
            
            //247231119 - 246894760
            var list = new List<Hand>(); 
            var cards = this.Cards.ToArray();
            GetAllPermuationsWithoutJokers(cards, list);
            return list.Max(h => h.HandType);
        }

        public List<Hand> GetAllPermuationsWithoutJokers(Card[] cards, List<Hand> hands)
        {
            if (!cards.Any(j => j == Card.Jack))
            {
                return hands;
            }
            
            for(int i = 0; i < cards.Length; i++)
            {
                if (cards[i] == Card.Jack)
                {
                    foreach(Card card in Enum.GetValues(typeof(Card)))
                    {
                        if(card == Card.Jack)
                        {
                            continue;
                        }
                        
                        var newCards = cards.ToArray();
                        newCards[i] = card;
                        hands.Add(new Hand(newCards));
                        GetAllPermuationsWithoutJokers(newCards, hands);
                    }
                }
              
            }

            return hands;
        }
        
        
        public HandType GetHandType()
        {
            var cardGroups = Cards.GroupBy(c => c).ToList();

            var numberOfPairs = cardGroups.Count(c => c.Count() == 2);
            var numberOfThreeOfAKind = cardGroups.Count(c => c.Count() == 3);
            var numberOfFourOfAKind = cardGroups.Count(c => c.Count() == 4);
            var numberOfFiveOfAKind = cardGroups.Count(c => c.Count() == 5);
            
            if (numberOfFiveOfAKind == 1)
            {
                return HandType.FiveOfAKind;
            }

            if (numberOfFourOfAKind == 1)
            {
                return HandType.FourOfAKind;
            }

            if (numberOfThreeOfAKind == 1 && numberOfPairs == 1)
            {
                return HandType.FullHouse;
            }

            if (numberOfThreeOfAKind == 1)
            {
                return HandType.ThreeOfAKind;
            }

            if (numberOfPairs == 2)
            {
                return HandType.TwoPair;
            }

            if (numberOfPairs == 1)
            {
                return HandType.OnePair;
            }

            return HandType.HighCard;
        }

        private readonly Dictionary<char, Card> CharToCardMapping = new Dictionary<char, Card>()
        {
            { 'A', Card.Ace },
            { '2', Card.Two },
            { '3', Card.Three },
            { '4', Card.Four },
            { '5', Card.Five },
            { '6', Card.Six },
            { '7', Card.Seven },
            { '8', Card.Eight },
            { '9', Card.Nine },
            { 'T', Card.Ten },
            { 'J', Card.Jack },
            { 'Q', Card.Queen },
            { 'K', Card.King }
        };

    }

    public enum Card
    {
        JackAsJoker = 1,
        Ace = 14,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13
    }

    public enum HandType
    {
        FiveOfAKind = 7,
        FourOfAKind = 6,
        FullHouse = 5,
        ThreeOfAKind = 4,
        TwoPair = 3,
        OnePair = 2,
        HighCard = 1
    }
}
