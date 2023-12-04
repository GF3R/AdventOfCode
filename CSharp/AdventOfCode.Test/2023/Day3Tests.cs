using System.Text.RegularExpressions;
using AdventOfCode.Days;
using NUnit.Framework;

namespace AdventOfCode.Test._2023;

using Twenty23.Days;

public class Day3Tests : BaseDayTest<Day3Solver>
{
    private const string TestInput = @"467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598..";
    
    [Test]
    public void ParsingValidation()
    {
        var result = new ManualGrid(TestInput.Split('\n'));
        Assert.AreEqual(10, result.GridNumbers.Count);
        Assert.AreEqual(6, result.GridSymbols.Count);
        // Check symbol coordinates
        Assert.AreEqual(new []{ new Coordinate(1, 3), new Coordinate(3, 6), new Coordinate(4, 3), new Coordinate(5, 5), new Coordinate(8, 3), new Coordinate(8, 5) }, result.GridSymbols.Select(gn => gn.Coordinate).ToArray());
        Assert.AreEqual(new[] { 467, 114, 35, 633, 617, 58, 592, 755, 664, 598 }, result.GridNumbers.Select(gn => gn.Number).ToArray());
        Assert.AreEqual(new[] { '*', '#', '*', '+', '$', '*' },result.GridSymbols.Select(gn => gn.Symbol).ToArray());
        Assert.AreEqual(new[] { 467, 35, 633, 617, 592, 755, 664, 598  }, result.GetNumbersWithAdjacentSymbol().Select(gn => gn.Number).ToArray());
    }
    
    [TestCase(1, 1, 1, 2, true)]
    [TestCase(1, 1, 2, 1, true)]
    [TestCase(1, 1, 2, 2, true)]
    [TestCase(1, 1, 1, 1, true)]
    [TestCase(1, 1, 1, 3, false)]
    [TestCase(1, 1, 3, 1, false)]
    [TestCase(1, 1, 3, 3, false)]
    [TestCase(1, 1, 2, 3, false)]
    public void TestCoordinatesAdjacency(int coord1X, int coord1Y, int coord2X, int coord2Y, bool expected)
    {
        var coordinate1 = new Coordinate(coord1X, coord1Y);
        var coordinate2 = new Coordinate(coord2X, coord2Y);
        var result = coordinate1.IsAdjacentToCoordinate(coordinate2);
        Assert.AreEqual(expected, result);
    }
    
    [TestCase(TestInput, 4361)]
    public void Day1Part1Tests(string input, int output)
    {
        Part1(input, output);
    }

    [TestCase(TestInput, 467835)]
    public void Day1Part2Tests(string input, int output)
    {
        Part2(input, output);
    }
}
