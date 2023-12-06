using System.Text.RegularExpressions;
using AdventOfCode.Days;
using NUnit.Framework;

namespace AdventOfCode.Test._2023;

using Twenty23.Days;

public class Day6Tests : BaseDayTest<Day6Solver>
{
    private const string TestInput = @"Time:      7  15   30
Distance:  9  40  200";

    [TestCase(TestInput, 288)]
    public void Part1Tests(string input, int output)
    {
        Part1(input, output);
    }

    [TestCase(TestInput, 71503)]
    public void Part2Tests(string input, int output)
    {
        Part2(input, output);
    }
}