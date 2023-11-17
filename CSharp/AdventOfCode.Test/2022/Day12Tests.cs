using AdventOfCode.Days;
using NUnit.Framework;

namespace AdventOfCode.Test;

using Twenty22.Days;

public class Day12Tests
{
    private const string TestInput = @"Sabqponm
abcryxxl
accszExk
acctuvwj
abdefghi";

    private static readonly string? FullInput = File.ReadAllText("./Inputs/Input12.txt");

    [TestCase(TestInput, 31, true)]
    [TestCase(TestInput, 29, false)]
    [TestCase(null, 500, false)]
    [TestCase(null, 504, true)]
    public void Day12Part1Tests(string? input, int ouptut, bool fromStart = true)
    {
        var day12Solver = new Day12Solver();
        input ??= FullInput;
        var result = day12Solver.Solve(input, fromStart);
        Assert.AreEqual(ouptut, result);
    }
}