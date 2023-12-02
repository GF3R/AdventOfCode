namespace AdventOfCode.Twenty23.Days
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core;

    public class Day1Solver : BaseSolver
    {
        private static readonly Dictionary<string, char> numbers = new Dictionary<string, char>
        {
            {"one", '1'},
            {"two", '2'},
            {"three", '3'},
            {"four", '4'},
            {"five", '5'},
            {"six", '6'},
            {"seven", '7'},
            {"eight", '8'},
            {"nine", '9'},
        };
        
        public override object SolvePart1(string input)
        {
            var lines = input.Split("\n");
            return GetSumOfLines(lines);
        }


        public override object SolvePart2(string input)
        {
            var lines = input.Split("\n");
            var replacedLines = PlaceNumbersForWords(lines);

            return GetSumOfLines(replacedLines.ToArray());
        }

        private static List<string> PlaceNumbersForWords(string[] lines)
        {
            var replacedLines = new List<string>();
            foreach (var line in lines)
            {
                var newLine = line;
                foreach (var number in numbers)
                {
                    int index;
                    do
                    {
                        index = newLine.IndexOf(number.Key, StringComparison.InvariantCultureIgnoreCase);
                        if (index == -1)
                        {
                            continue;
                        }

                        // we insert at +1 to fix cases like "eighTwo" so we have eight2wo and then e8ight2wo and not eigh2two
                        newLine = newLine.Insert(index + 1, number.Value.ToString());
                    } while (index != -1);
                }

                replacedLines.Add(newLine);
            }

            return replacedLines;
        }

        private static object GetSumOfLines(string[] lines)
        {

            var linesAsNumbers = new List<int>();
            foreach (var chars in lines.Select(l => l.ToCharArray()))
            {
                char first = 'x';
                char last = 'x';
                foreach (var c in chars)
                {
                    if (int.TryParse(c.ToString(), out var result))
                    {
                        if (first == 'x')
                        {
                            first = c;
                        }

                        last = c;
                    }
                }

                linesAsNumbers.Add(int.Parse(string.Join("", first, last)));
            }

            return linesAsNumbers.Sum();
        }
    }
}
