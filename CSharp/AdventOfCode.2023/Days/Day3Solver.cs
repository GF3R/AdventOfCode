namespace AdventOfCode.Twenty23.Days
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core;

    public class Day3Solver : BaseSolver
    {
        // Result1: 532445
        public override object SolvePart1(string input)
        {
            var lines = input.Split("\n");
            var grid = new ManualGrid(lines);

            var numbersWithAdjacentSymbols = grid.GetNumbersWithAdjacentSymbol();
            return numbersWithAdjacentSymbols.Sum(n => n.Number);
        }

        public override object SolvePart2(string input)
        {
            var lines = input.Split("\n");
            var grid = new ManualGrid(lines);

            var gearsWithExactlyTwoAdjacentSymbolsMultiplied = grid.GetGearNumber();
            return gearsWithExactlyTwoAdjacentSymbolsMultiplied;
        }
    }

    public class ManualGrid
    {
        public char[][] Grid { get; }
        public List<GridNumber> GridNumbers { get; set; }
        public List<GridSymbol> GridSymbols { get; }

        public ManualGrid(string[] lines)
        {
            this.Grid = new char[lines.Length][];

            this.GridNumbers = new List<GridNumber>();
            this.GridSymbols = new List<GridSymbol>();

            for (var i = 0; i < lines.Length; i++)
            {
                var trimmedLine = lines[i].Trim();
                this.Grid[i] = trimmedLine.ToCharArray();
                this.GetAllNumbers(trimmedLine, i);
            }

        }

        public GridNumber[] GetNumbersWithAdjacentSymbol()
        {
            var numbers = new List<GridNumber>();
            foreach (var gridNumber in GridNumbers)
            {
                if (gridNumber.IsAdjacentTo(GridSymbols))
                {
                    numbers.Add(gridNumber);
                }
            }

            return numbers.ToArray();
        }
        
        public int GetGearNumber()
        {
            var sum = 0;
            var gears = GridSymbols.Where(s => s.Symbol == '*');
            foreach (var gear in gears)
            {
                var numbers = GridNumbers.Where(n => n.IsAdjacentTo(gear)).ToArray();
                if (numbers.Length == 2)
                {
                    var firstNumber = numbers.First();
                    var secondNumber = numbers.Last();
                    sum += firstNumber.Number * secondNumber.Number;
                }
            }

            return sum;
        }

        private void GetAllNumbers(string line, int index)
        {
            string number = string.Empty;
            var numberCoordinates = new List<Coordinate>();
            for (int y = 0; y < line.Length; y++)
            {
                var character = line[y];
                if (int.TryParse(character.ToString(), out var _))
                {
                    number += character;
                    numberCoordinates.Add(new Coordinate(index, y));
                    if (y == line.Length - 1)
                    {
                        this.GridNumbers.Add(new GridNumber
                        {
                            Number = int.Parse(number),
                            Coordinates = numberCoordinates.ToArray()
                        });
                        number = string.Empty;
                        numberCoordinates.Clear();
                    }
                    continue;
                }

                if (character != '.')
                {
                    this.GridSymbols.Add(new GridSymbol
                    {
                        Symbol = character,
                        Coordinate = new Coordinate(index, y)
                    });
                }

                if (!string.IsNullOrEmpty(number))
                {
                    this.GridNumbers.Add(new GridNumber
                    {
                        Number = int.Parse(number),
                        Coordinates = numberCoordinates.ToArray()
                    });
                }

                number = string.Empty;
                numberCoordinates.Clear();
            }
        }
    }

    public class GridNumber
    {
        public int Number { get; set; }

        public Coordinate[] Coordinates { get; set; }

        public bool IsAdjacentTo(List<GridSymbol> gridSymbols)
        {
            return gridSymbols.Any(gs => this.Coordinates.Any(c => c.IsAdjacentToCoordinate(gs.Coordinate)));
        }
        
        public bool IsAdjacentTo(GridSymbol gridSymbol)
        {
            return this.Coordinates.Any(c => c.IsAdjacentToCoordinate(gridSymbol.Coordinate));
        }
    }

    public class GridSymbol
    {
        public char Symbol { get; set; }

        public Coordinate Coordinate { get; set; }
    }

    public class Coordinate
    {
        public Coordinate(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override string ToString()
        {
            return $"({this.X}, {this.Y})";
        }

        public override bool Equals(object obj)
        {
            if (obj is Coordinate coordinate)
            {
                return this.X == coordinate.X && this.Y == coordinate.Y;
            }

            return false;
        }

        protected bool Equals(Coordinate other)
        {
            return this.X == other.X && this.Y == other.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.X, this.Y);
        }

        public int X { get; }
        public int Y { get; }

        public bool IsAdjacentToCoordinate(Coordinate coordinate)
        {
            return Math.Abs(this.X - coordinate.X) <= 1 && Math.Abs(this.Y - coordinate.Y) <= 1;
        }
    }

}
