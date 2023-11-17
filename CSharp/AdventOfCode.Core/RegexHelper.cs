using System.Text.RegularExpressions;

namespace AdventOfCode.Days;

public class RegexHelper
{
    public static readonly Regex RegexToFindNumber = new(@"\d+");

    public static IEnumerable<int> GetNumbers(string input)
    {
        return RegexToFindNumber.Matches(input).Select(m => int.Parse(m.Value));
    }

    public static int GetNumber(string input)
    {
        return GetNumbers(input).First();
    }
}