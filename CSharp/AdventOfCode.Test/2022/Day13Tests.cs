using AdventOfCode.Days;
using NUnit.Framework;

namespace AdventOfCode.Test;

using Twenty22.Days;

public class Day13Tests
{
    private const string TestInput = @"[1,1,3,1,1]
[1,1,5,1,1]

[[1],[2,3,4]]
[[1],4]

[9]
[[8,7,6]]

[[4,4],4,4]
[[4,4],4,4,4]

[7,7,7,7]
[7,7,7]

[]
[3]

[[[]]]
[[]]

[1,[2,[3,[4,[5,6,7]]]],8,9]
[1,[2,[3,[4,[5,6,0]]]],8,9]";

    private const string StrangeArrays = @"[[[]]]
[[[]]]";

    private static readonly string? FullInput = File.ReadAllText("./Inputs/Input13.txt");

    [TestCase(TestInput, 13)]
    [TestCase(StrangeArrays, 13)]
    public void Day13Part1Tests(string? input, int ouptut)
    {
        var day13Solver = new Day13Solver();
        input ??= FullInput;
        var result = day13Solver.Solve(input);
        Assert.AreEqual(ouptut, result);
    }

    [TestCase("[[[]]]", new object[]{new object[]{new object[]{}}})]
    [TestCase("[[]]", new object[]{new object[]{}})]
    [TestCase("[]", new object[]{})]
    [TestCase("[1,[2,[3,[4,[5,6,7]]]],8,9]", new object[]{1, new object[]{2, new object[]{3, new object[]{4, new object[]{5, 6, 7}}}}, 8, 9})]
    public void Day13ParseTests(string? input, object[] ouptut)
    {
        var day13Solver = new Day13Solver();
        input ??= FullInput;
        var result = day13Solver.FromJsonString(input);
        Assert.AreEqual(ouptut, result);
    }
}