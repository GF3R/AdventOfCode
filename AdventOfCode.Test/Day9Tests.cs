using AdventOfCode.Days;
using NUnit.Framework;

namespace AdventOfCode.Test;

public class Day9Tests
{
    private const string TestInput = @"R 4
U 4
L 3
D 1
R 4
D 1
L 5
R 2
";

    private const string TestInput2 = @"R 5
U 8
L 8
D 3
R 17
D 10
L 25
U 20";

    private static readonly string? FullInput = File.ReadAllText("./Inputs/Input9.txt");

    [TestCase(TestInput, 13)]
    [TestCase(null, 6197)]
    public void Day9Part1Tests(string? input, int ouptut)
    {
        var day9Solver = new Day9Solver();
        input ??= FullInput;

        var result = day9Solver.Solve(input, 2);
        Assert.AreEqual(ouptut, result);
    }

    [TestCase(TestInput2, 36)]
    [TestCase(null, 2562)]
    public void Day9Part2Tests(string? input, int ouptut)
    {
        input ??= FullInput;
        var day9Solver = new Day9Solver();
        var result = day9Solver.Solve(input,10);
        Assert.AreEqual(ouptut, result);
    }
}