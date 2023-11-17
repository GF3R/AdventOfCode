namespace AdventOfCode.Twenty22.Days;

public class Day3Solver
{

    public void Solve(string input)
    {
        var lines = input.Split(Environment.NewLine);
        var sum = SolvePart1(lines.Select(x => x.ToCharArray()));
        Console.WriteLine($"Part1: {sum}");
        sum = SolvePart2(lines.Select(x => x.ToCharArray()));
        Console.WriteLine($"Part2: {sum}");        
    }

    private static int SolvePart1(IEnumerable<char[]> lines)
    {
        var sum = 0;
        foreach (var array in lines)
        {
            var firstHalf = array.Take(array.Length / 2).ToArray();
            var secondHalf = array.Skip(array.Length / 2).Take(array.Length / 2);
            var matchingChar = Array.Find(firstHalf, t => secondHalf.Contains(t));
            sum += CharValueForPuzzle(matchingChar);
        }

        return sum;
    }
    
    private static int SolvePart2(IEnumerable<char[]> lines)
    {
        var sum = 0;
        var arrayOfArrays = lines.ToArray();
        for (var i = 0; i < arrayOfArrays.Length; i+=3)
        {
            var line1 = arrayOfArrays[i];
            var line2 = arrayOfArrays[i + 1];
            var line3 = arrayOfArrays[i + 2];
            
            foreach(var c in line1)
            {
                if (line2.Contains(c) && line3.Contains(c))
                {
                    sum += CharValueForPuzzle(c);
                    break;
                }
            }
        }

        return sum;
    }

    private static int CharValueForPuzzle(char character)
    {
        return character % 32 + (char.IsUpper(character) ? 26 : 0);
    }
}