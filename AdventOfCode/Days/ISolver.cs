namespace AdventOfCode.Days;

public interface ISolver
{
    object SolvePart1(string input);
    object SolvePart2(string input);

    (object, object) Solve(string input);
}