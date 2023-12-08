using System.Text.RegularExpressions;
using AdventOfCode.Days;
using NUnit.Framework;

namespace AdventOfCode.Test._2023;

using Twenty23.Days;

public class Day8Tests : BaseDayTest<Day8Solver>
{
    private const string TestInput1 = @"RL

AAA = (BBB, CCC)
BBB = (DDD, EEE)
CCC = (ZZZ, GGG)
DDD = (DDD, DDD)
EEE = (EEE, EEE)
GGG = (GGG, GGG)
ZZZ = (ZZZ, ZZZ)";

    private const string TestInput2 = @"LLR

AAA = (BBB, BBB)
BBB = (AAA, ZZZ)
ZZZ = (ZZZ, ZZZ)";

    private const string TestInput3 = @"LR

11A = (11B, XXX)
11B = (XXX, 11Z)
11Z = (11B, XXX)
22A = (22B, XXX)
22B = (22C, 22C)
22C = (22Z, 22Z)
22Z = (22B, 22B)
XXX = (XXX, XXX)";

    [TestCase(TestInput1, 2)]
    [TestCase(TestInput2, 6)]
    public void Part1Tests(string input, int output)
    {
        Part1(input, output);
    }

    [TestCase(TestInput3, 6)]
    public void Part2Tests(string input, int output)
    {
        Part2(input, output);
    }

}