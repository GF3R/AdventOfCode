using AdventOfCode.Days;
using NUnit.Framework;

namespace AdventOfCode.Test;

using Core;

public class BaseDayTest<T> where T : ISolver
{
    internal void Part1(string input, object output)
    {
        var solver = (ISolver)Activator.CreateInstance(typeof(T))!;
        var result = solver.SolvePart1(input);
        Assert.AreEqual(output, result);
    }

    internal void Part2(string input, object output)
    {
        var solver = (ISolver)Activator.CreateInstance(typeof(T))!;
        var result = solver.SolvePart2(input);
        Assert.AreEqual(output, result);
    }
}