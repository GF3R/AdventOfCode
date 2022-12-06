using System.Text.RegularExpressions;
using AdventOfCode.Days;
using NUnit.Framework;

namespace AdventOfCode.Test;

public class Day5Tests
{
    private string testInput = @"
    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2";

    [Test]
    public void TestPart1()
    {
        var codeDay5 = new CodeDay5();
        var result = codeDay5.Solve(testInput);
        Assert.AreEqual("CMZ", result);
    }

    [Test]
    public void TestPart2()
    {
        var codeDay5 = new CodeDay5
        {
            IsCrateMover9001 = true
        };
        
        var result = codeDay5.Solve(testInput);
        Assert.AreEqual("MCD", result);
    }
    
    [Test]
    public void TestFullPart2()
    {
        var codeDay5 = new CodeDay5
        {
            IsCrateMover9001 = true
        };
        var result = codeDay5.Solve(File.ReadAllText("./Inputs/Input5.txt"));
        Assert.AreEqual("QNDWLMGNS", result);
    }
    
    [TestCase("[Z] [M] [P]", "ZMP")]
    [TestCase("    [D]    ", "    D    ")]
    [TestCase("[N] [C]    ", "NC    ")]
    [TestCase("    [S] [N] [R]         [S] [F] [N]", "    SNR        SFN")]
    public void RegexTest(string input, string ouptut)
    {
        var regexFilterBrackets = new Regex(@"[A-Z]|\s{4}");
        var matches = regexFilterBrackets.Matches(input).Select(m => m.Value).ToList();
        Assert.AreEqual(ouptut, string.Join("", matches));
    }
}