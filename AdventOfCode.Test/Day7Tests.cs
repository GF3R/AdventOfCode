using System.Text.RegularExpressions;
using AdventOfCode.Days;
using NUnit.Framework;

namespace AdventOfCode.Test;

public class Day7Tests
{
    private const string TestInput = @"$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k";

    [TestCase(TestInput, 95437)]
    public void Day7Part1Tests(string input, int ouptut)
    {
        var day7Solver = new Day7Solver();
        var result = day7Solver.Solve(input);
        Assert.AreEqual(ouptut, result);
    }
    
    [TestCase(TestInput, 24933642)]
    public void Day7Part2Tests(string input, int ouptut)
    {
        var day7Solver = new Day7Solver();
        var result = day7Solver.Solve(input, part1: false);
        Assert.AreEqual(ouptut, result);
    }
    
}