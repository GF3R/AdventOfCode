namespace AdventOfCode.Days;

public class CodeDay6
{
    public int PacketSize = 4;
    
    public int Solve(string input)
    {
        var inputPacket = input.ToCharArray().ToList();

        for (var i = 0; i < inputPacket.Count; i++)
        {
            var fourLetters = inputPacket.GetRange(i, PacketSize);
            if (AllFourLettersAreUnique(fourLetters))
            {
                return i + PacketSize;
            }
        }
        
        throw new ArgumentException("No solution found");
    }

    private static bool AllFourLettersAreUnique(ICollection<char> fourLetters)
    {
        return fourLetters.Distinct().Count() == fourLetters.Count;
    }
}