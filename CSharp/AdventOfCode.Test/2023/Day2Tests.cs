using System.Text.RegularExpressions;
using AdventOfCode.Days;
using NUnit.Framework;

namespace AdventOfCode.Test._2023;

using Twenty23.Days;

public class Day2Tests : BaseDayTest<Day2Solver>
{
    private const string TestInput = @"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green";
    
    [TestCase(TestInput, 8)]
    public void Part1Tests(string input, int output)
    {
        Part1(input, output);
    }

    [TestCase(TestInput, 2286)]
    public void Part2Tests(string input, int output)
    {
        Part2(input, output);
    }
}
