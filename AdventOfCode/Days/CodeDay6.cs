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
            if (AllfourLettersAreUnique(fourLetters))
            {
                return i + PacketSize;
            }
        }
        
        throw new ArgumentException("No solution found");
    }

    private bool AllfourLettersAreUnique(List<char> fourLetters)
    {
        var uniqueLetters = fourLetters.Distinct();
        return uniqueLetters.Count() == fourLetters.Count();
    }
}