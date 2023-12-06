namespace AdventOfCode.Core;

using System.Diagnostics;

public abstract class BaseSolver : ISolver
{
    public abstract object SolvePart1(string input);

    public abstract object SolvePart2(string input);

    public (object, object) Solve(string input)
    {
        var sw = new Stopwatch();
        sw.Start();
        var part1 = this.SolvePart1(input);
        var ellapsedForPart1 = sw.Elapsed;
        Console.WriteLine($"Part 1: {part1} ({ellapsedForPart1})");
        var part2 = this.SolvePart2(input);
        var ellapsedForPart2 = sw.Elapsed - ellapsedForPart1;
        Console.WriteLine($"Part 2: {part2} ({ellapsedForPart2})");
        sw.Stop();
        return (part1, part2);
    }
}