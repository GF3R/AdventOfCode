using System.Text.RegularExpressions;
using AdventOfCode.Days;
using NUnit.Framework;

namespace AdventOfCode.Test._2023;

using Twenty23.Days;

public class Day9Tests : BaseDayTest<Day9Solver>
{
    private const string TestInput1 = @"0 3 6 9 12 15
1 3 6 10 15 21
10 13 16 21 30 45";

    [TestCase(TestInput1, 114)]
    public void Part1Tests(string input, int output)
    {
        Part1(input, output);
    }

    [TestCase(TestInput1, 6)]
    public void Part2Tests(string input, int output)
    {
        Part2(input, output);
    }

}