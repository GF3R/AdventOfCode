using System.Text.RegularExpressions;
using AdventOfCode.Days;
using NUnit.Framework;

namespace AdventOfCode.Test._2023;

using Twenty23.Days;

public class Day7Tests : BaseDayTest<Day7Solver>
{
    private const string TestInput = @"32T3K 765
T55J5 684
KK677 28
KTJJT 220
QQQJA 483";

    [TestCase(TestInput, 6440)]
    public void Part1Tests(string input, int output)
    {
        Part1(input, output);
    }

    [TestCase(TestInput, 5905)]
    public void Part2Tests(string input, int output)
    {
        Part2(input, output);
    }
    
    [TestCase("JQKQJ 976", "FourOfAKind")]
    [TestCase("JJ56J 948", "FourOfAKind")]
    [TestCase("J3JJ3 66", "FiveOfAKind")]
    [TestCase("J3JJ3 66", "FiveOfAKind")]
    [TestCase("224JJ 66", "FourOfAKind")]
    [TestCase("22222 66", "FiveOfAKind")]
    [TestCase("JJJJJ 66", "FiveOfAKind")]
    public void GetBestHandTest(string input, string expectedHandType)
    {
        var hand = new Hand(input);
        Assert.AreEqual(expectedHandType, hand.GetBestPermuationWithoutJokers().ToString());
    }
}