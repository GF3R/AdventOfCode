namespace AdventOfCode.Twenty22.Days;

public class Day6Solver
{
    public int PacketSize = 4;
    
    public int Solve(string input)
    {
        var inputPacket = input.ToCharArray().ToList();

        for (var i = 0; i < inputPacket.Count; i++)
        {
            var letters = inputPacket.GetRange(i, PacketSize);
            if (AllLettersAreUnique(letters))
            {
                return i + PacketSize;
            }
        }
        
        throw new ArgumentException("No solution found");
    }

    private static bool AllLettersAreUnique(ICollection<char> fourLetters)
    {
        return fourLetters.Distinct().Count() == fourLetters.Count;
    }
}