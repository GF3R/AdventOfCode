namespace AdventOfCode;

public class CodeDay1
{
    public void Solve()
    {
        var input = File.ReadAllText("./Input1.txt");
        var elfSum = input.Split("\r\n\r\n").Select(t => t.Split('\n').Sum(int.Parse)).ToList();
        var max = elfSum.Max();
        Console.WriteLine($"the max => {max}");

        var sumOfMax3 = elfSum.OrderBy(e => e).TakeLast(3).Sum();
        Console.WriteLine($"the sum of the heaviest 3 => {sumOfMax3}");
    }
}