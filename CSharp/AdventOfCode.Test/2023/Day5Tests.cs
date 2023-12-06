using System.Text.RegularExpressions;
using AdventOfCode.Days;
using NUnit.Framework;

namespace AdventOfCode.Test._2023;

using Twenty23.Days;

public class Day5Tests : BaseDayTest<Day5Solver>
{
    private const string TestInput = @"seeds: 79 14 55 13

seed-to-soil map:
50 98 2
52 50 48

soil-to-fertilizer map:
0 15 37
37 52 2
39 0 15

fertilizer-to-water map:
49 53 8
0 11 42
42 0 7
57 7 4

water-to-light map:
88 18 7
18 25 70

light-to-temperature map:
45 77 23
81 45 19
68 64 13

temperature-to-humidity map:
0 69 1
1 0 69

humidity-to-location map:
60 56 37
56 93 4";

    [TestCase(TestInput, 35)]
    public void Part1Tests(string input, int output)
    {
        Part1(input, output);
    }

    [TestCase(TestInput, 46)]
    public void Part2Tests(string input, int output)
    {
        Part2(input, output);
    }
}