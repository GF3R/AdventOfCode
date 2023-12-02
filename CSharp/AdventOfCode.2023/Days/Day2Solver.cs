namespace AdventOfCode.Twenty23.Days
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdventOfCode.Days;
    using Core;

    public class Day2Solver : BaseSolver
    {
        /*
         Result1: 2528
         Result2: 67363
        */
        public override object SolvePart1(string input)
        {
            int maxRed = 12;
            int maxGreen = 13;
            int maxBlue = 14;

            var lines = input.Split("\n");
            var games = lines.Select(LineToGame);
            var possibleGames = games.Where(g => g.IsGamePossible(maxRed, maxGreen, maxBlue));

            return possibleGames.Sum(g => g.GameNr);
        }

        public override object SolvePart2(string input)
        {
            var lines = input.Split("\n");
            var games = lines.Select(LineToGame);
            return games.Sum(g => g.PowerOfGame());
        }

        private static Game LineToGame(string line)
        {
            var splitted = line.Split(':');

            var setsOfCubes = splitted[1].Split(';');

            return new Game
            {
                GameNr = int.Parse(splitted[0].Split(" ")[1]),
                SubSets =  setsOfCubes.Select(setOfCube => LineToSubset(setOfCube.Trim())).ToList()
            };
        }

        private static SubSet LineToSubset(string line)
        {
            var cubes = line.Split(',');
            
            return new SubSet()
            {
                Cubes = cubes.Select(LineToCubes).ToList()
            };
        }

        private static SetOfCubes LineToCubes(string line)
        {
            var splitted = line.Split(' ');
            var numberOfCubes = int.Parse(splitted[0]);
            var color = splitted[1];
            
            return new SetOfCubes
            {
                NumberOfCubes = numberOfCubes,
                Color = color switch
                {
                    "red" => CubeColor.Red,
                    "green" => CubeColor.Green,
                    "blue" => CubeColor.Blue,
                    _ => throw new Exception("Unknown color")
                }
            };
        }

    }

    class Game
    {
        public int GameNr { get; set; }
        
        public List<SubSet> SubSets { get; set; }
        
        public bool IsGamePossible(int maxRed, int maxGreen, int maxBlue)
        {
            var toManyRed = SubSets.Any(c => c.NumberOfCubes(CubeColor.Red) > maxRed);
            var toManyGreen = SubSets.Any(c => c.NumberOfCubes(CubeColor.Green) > maxGreen);
            var toManyBlue = SubSets.Any(c => c.NumberOfCubes(CubeColor.Blue) > maxBlue);
            return !toManyRed && !toManyGreen && !toManyBlue;
        }

        public int PowerOfGame()
        {
            var red = SubSets.Max(s => s.CubesOfColorNeeded(CubeColor.Red));
            var green = SubSets.Max(s => s.CubesOfColorNeeded(CubeColor.Green));
            var blue = SubSets.Max(s => s.CubesOfColorNeeded(CubeColor.Blue));
            return red * green * blue;
        }
    }

    class SubSet
    {
        public List<SetOfCubes> Cubes { get; set; }

        public int NumberOfCubes(CubeColor color)
        {
            return Cubes.Where(c => c.Color == color).Sum(c => c.NumberOfCubes);
        }

        public int CubesOfColorNeeded(CubeColor color)
        {
            return Cubes.Where(c => c.Color == color).Sum(c => c.NumberOfCubes);
        }
    }

    class SetOfCubes
    {
        public int NumberOfCubes { get; set; }

        public CubeColor Color { get; set; }
    }

    enum CubeColor
    {
        Red,
        Blue,
        Green
    }
}
