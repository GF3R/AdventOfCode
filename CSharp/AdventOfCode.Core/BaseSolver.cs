namespace AdventOfCode.Core;

public abstract class BaseSolver : ISolver
{
    public abstract object SolvePart1(string input);

    public abstract object SolvePart2(string input);

    public (object, object) Solve(string input)
    {
        return (this.SolvePart1(input), this.SolvePart2(input));
    }
}