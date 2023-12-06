using System.Text.RegularExpressions;

namespace AdventOfCode.Days;

public static class RegexHelper
{
    public static readonly Regex RegexToFindNumber = new(@"\d+");

    public static IEnumerable<int> GetNumbers(this string input)
    {
        return RegexToFindNumber.Matches(input).Select(m => int.Parse(m.Value));
    }
    
    public static IEnumerable<long> GetLongNumbers(this string input)
    {
        return RegexToFindNumber.Matches(input).Select(m => long.Parse(m.Value));
    }

    public static int GetNumber(string input)
    {
        return GetNumbers(input).First();
    }
}