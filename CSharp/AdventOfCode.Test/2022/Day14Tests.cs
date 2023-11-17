using AdventOfCode.Days;
using NUnit.Framework;

namespace AdventOfCode.Test;

using Twenty22.Days;

public class Day14Tests : BaseDayTest<Day14Solver>
{
    private const string TestInput = @"498,4 -> 498,6 -> 496,6
503,4 -> 502,4 -> 502,9 -> 494,9";

    [TestCase(TestInput, 24)]
    public void Day14Part1Tests(string input, int output)
    {
        Part1(input, output);
    }

    [TestCase(TestInput, 93)]
    public void Day14Part2Tests(string input, int output)
    {
        Part2(input, output);
    }
}