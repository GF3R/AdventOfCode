namespace AdventOfCode.Twenty23.Days
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core;

    public class Day4Solver : BaseSolver
    {
        private Dictionary<int, List<Card>> PreCopiedCards = new Dictionary<int, List<Card>>();
        
        // Result1: 532445
        public override object SolvePart1(string input)
        {
            var lines = input.Split("\n");
            var cards = lines.Select(l => new Card(l)).ToList();
            return cards.Sum(c => c.GetScore());
        }

        public override object SolvePart2(string input)
        {
            var lines = input.Split("\n");
            var cards = lines.Select(l => new Card(l)).ToArray();
            var wins = CopyTilYouCantNoMore(cards, cards);
            return wins + cards.Length;
        }

        private int CopyTilYouCantNoMore(Card[] allCards, Card[] cards)
        {
            int totalWins = 0;
            for (int cardIndex = 0; cardIndex < cards.Length; cardIndex++)
            {
                var currentCard = cards[cardIndex];
                var numberOfWins = currentCard.GetNumberOfWins();
                if (numberOfWins == 0)
                {
                    continue;
                }
                totalWins += numberOfWins;
                
                var copies = new List<Card>();

                if (PreCopiedCards.TryGetValue(currentCard.CardNumber, out var card))
                {
                    copies = card;
                }
                else
                {
                    for (int i = currentCard.CardIndex + 1; i < currentCard.CardIndex + 1 + numberOfWins; i++)
                    {
                        if (i > allCards.Length)
                        {
                            break;
                        }

                        copies.Add(allCards[i].CreateCopy());
                    }

                    PreCopiedCards.Add(currentCard.CardNumber, copies);
                }

                if (copies.Count == 0)
                {
                    return totalWins;
                }

                totalWins += CopyTilYouCantNoMore(allCards, copies.ToArray());
            }

            return totalWins;
        }
    }

    public class Card
    {
        public int[] WinningNumbers { get; set; }
        
        public int[] Numbers { get; set; }
        
        public int CardNumber { get; set; }
        
        public int CardIndex => CardNumber - 1;

        // Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
        public Card(string line)
        {
            var splitted = line.Trim().Split(':');
            var numbers = splitted[1].Split('|');
            this.WinningNumbers = numbers[0].Trim().Split(' ').Where(s => !string.IsNullOrEmpty(s)).Select(int.Parse).ToArray();
            this.Numbers = numbers[1].Trim().Split(' ').Where(s => !string.IsNullOrEmpty(s)).Select(int.Parse).ToArray();
            this.CardNumber = int.Parse(splitted[0].Split("Card ").First(s => !string.IsNullOrEmpty(s)));
        }

        protected Card(Card card)
        {
            this.CardNumber = card.CardNumber;
            this.Numbers = card.Numbers.ToArray();
            this.WinningNumbers = card.WinningNumbers.ToArray();
        }
        
        public int GetScore()
        {
            var numbers = Numbers.Where(n => WinningNumbers.Contains(n));
            var count= numbers.Count();
            if(count == 0)
                return 0;
            
            return (int) Math.Pow(2, count-1);
        }
        
        public int GetNumberOfWins()
        {
            return this.Numbers.Count(n => this.WinningNumbers.Contains(n));
        }
        
        public Card CreateCopy()
        {
            return new Card(this);
        }
   
    }

}
