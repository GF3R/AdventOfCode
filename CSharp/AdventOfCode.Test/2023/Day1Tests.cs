using System.Text.RegularExpressions;
using AdventOfCode.Days;
using NUnit.Framework;

namespace AdventOfCode.Test._2023;

using Twenty23.Days;

public class Day1Tests : BaseDayTest<Day1Solver>
{
    private const string TestInput = @"1abc2
                                       pqr3stu8vwx
                                       a1b2c3d4e5f
                                       treb7uchet";

    private const string TestPart2 = @"two1nine
eightwothree
abcone2threexyz
xtwone3four
4nineeightseven2
zoneight234
7pqrstsixteen";

    [TestCase(TestInput, 142)]
    public void Day1Part1Tests(string input, int output)
    {
        Part1(input, output);
    }

    [TestCase(TestPart2, 281)]
    [TestCase("ninegflbrbv6twosvfive", 95)]
    [TestCase("8jjsclsqgfourthree7lvct75", 85)]
    [TestCase("8fivexxxrkzrppslbf8threexsskbkjcc", 83)]
    [TestCase("rdksixnzmxgppj6qkftmcthgl9", 69)]
    [TestCase("2ngoneninex", 29)]
    [TestCase("2bk", 22)]
    [TestCase("lhbvlseven41bdrkzmshkxone", 71)]
    [TestCase("sfxxlhkbhqnvskxd2five", 25)]
    [TestCase("6one1djcdmpdrgq3two", 62)]
    public void Day1Part2Tests(string input, int output)
    {
        Part2(input, output);
    }
}
