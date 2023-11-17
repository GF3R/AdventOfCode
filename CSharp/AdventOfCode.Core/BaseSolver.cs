namespace AdventOfCode.Days;

public abstract class BaseSolver : ISolver
{
    public abstract object SolvePart1(string input);

    public abstract object SolvePart2(string input);

    public (object, object) Solve(string input)
    {
        return (SolvePart1(input), SolvePart2(input));
    }
}