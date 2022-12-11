using AdventOfCode.Days;
using NUnit.Framework;

namespace AdventOfCode.Test;

public class Day11Tests
{
    private const string TestInput = @"Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1";


    private static readonly string? FullInput = File.ReadAllText("./Inputs/Input11.txt");

    [TestCase(TestInput, 10605)]
    [TestCase(null, 67830)]
    public void Day11Part1Tests(string? input, int ouptut)
    {
        var day11Solver = new Day11Solver();
        input ??= FullInput;
        var result = day11Solver.Solve(input);
        Assert.AreEqual(ouptut, result);
    }

    [TestCase(TestInput, (uint)6 * 4, 1)]
    [TestCase(TestInput, (uint)103 * 99, 20)]
    [TestCase(TestInput, (uint)5204 * 5192, 1000)]
    [TestCase(TestInput, 2713310158, 10000)]
    public void Day11Part2Tests(string input, uint ouptut, int rounds)
    {
        var day11Solver = new Day11Solver();
        var result = day11Solver.Solve(input, rounds, false);
        Assert.AreEqual(ouptut, result);
    }
}