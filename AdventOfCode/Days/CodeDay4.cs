using System.Runtime.CompilerServices;

namespace AdventOfCode;

public class CodeDay4
{

    public void Solve(string input)
    {
        var parsedElves = input.Split(Environment.NewLine)
            .Select(line => line.Split(","))
            .Select(GetElfRange)
            .Select(e => e.ToArray()).ToArray();
        
        var completelyOverlapping = parsedElves.Count(AreRangesCompletelyOverlapping);
        var overlappingRanges = parsedElves.Count(AreRangesOverlapping);
        
        Console.WriteLine("Overlapping ranges: " + overlappingRanges);
        Console.WriteLine("Completely overlapping ranges: " + completelyOverlapping);
    }

    private static IEnumerable<Tuple<int, int>> GetElfRange(IEnumerable<string> line)
    {
        var elfRange = new List<Tuple<int, int>>();
        foreach (var elfLine in line)
        {
            var elfs = elfLine.Split('-');
            elfRange.Add(new Tuple<int, int>(int.Parse(elfs[0]), int.Parse(elfs[1])));
        }

        return elfRange;
    }
    
    private static bool AreRangesOverlapping(IList<Tuple<int, int>> elves)
    {
        if (elves.Count > 2)
        {
            throw new ArgumentException("More than two elves");
        }
        
        var elf1 = elves[0];
        var elf2 = elves[1];
        
        return elf1.Item1 <= elf2.Item2 && elf1.Item2 >= elf2.Item1 || elf2.Item1 <= elf1.Item2 && elf2.Item2 >= elf1.Item1;
    }
    

    private static bool AreRangesCompletelyOverlapping(IList<Tuple<int, int>> elves)
    {
        if (elves.Count > 2)
        {
            throw new ArgumentException("More than two elves");
        }
        
        var elf1 = elves[0];
        var elf2 = elves[1];
        return (elf1.Item1 >= elf2.Item1 && elf1.Item2 <= elf2.Item2) || (elf2.Item1 >= elf1.Item1 && elf2.Item2 <= elf1.Item2);
    }
}
        