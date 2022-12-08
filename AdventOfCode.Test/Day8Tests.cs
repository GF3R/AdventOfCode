using AdventOfCode.Days;
using NUnit.Framework;

namespace AdventOfCode.Test;

public class Day8Tests
{
    private const string TestInput = @"30373
25512
65332
33549
35390
";

    private static readonly string FullInput = File.ReadAllText("./Inputs/Input8.txt");

    [TestCase(TestInput, 21)]
    public void Day8Part1Tests(string input, int ouptut)
    {
        var day8Solver = new Day8Solver();
        var result = day8Solver.Solve(input);
        Assert.AreEqual(ouptut, result);
    }

    [TestCase(TestInput, 8)]
    [TestCase(null, 291840)]
    public void Day8Part2Tests(string? input, int ouptut)
    {
        input ??= FullInput;
        var day8Solver = new Day8Solver();
        var result = day8Solver.SolvePart2(input);
        Assert.AreEqual(ouptut, result);
    }
}