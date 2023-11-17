namespace AdventOfCode.Twenty22.Days;

using System.Text.Json;

public class Day13Solver
{
    public const int RightOrder = -1;
    public const int WrongOrder = 1;
    public const int TiedOrder = 0;

    private string possibleInput = @"[1,1,3,1,1]
[1,1,5,1,1]

[[1],[2,3,4]]
[[1],4]

[9]
[[8,7,6]]

[[4,4],4,4]
[[4,4],4,4,4]

[7,7,7,7]
[7,7,7]

[]
[3]

[[[]]]
[[]]

[1,[2,[3,[4,[5,6,7]]]],8,9]
[1,[2,[3,[4,[5,6,0]]]],8,9]";

    public int Solve(string input)
    {
        var pairs = input.Split(Environment.NewLine + Environment.NewLine).Select(x =>
        {
            var pair = x.Split(Environment.NewLine);
            return (this.FromJsonString(pair[0]) as object[], this.FromJsonString(pair[1]) as object[]);
        }).ToArray();

        var sumOfIndices = 0;
        for (int i = 0; i < pairs.Length; i++)
        {
            var result = this.ComparePare(pairs[i].Item1, pairs[i].Item2);
            if (this.ComparePare(pairs[i].Item1, pairs[i].Item2) == RightOrder)
            {
                sumOfIndices += i + 1;
            }
        }

        return sumOfIndices;
    }

    public int SolvePart2(string input)
    {
        var items = new List<Object>();
        foreach (var item in input.Split(Environment.NewLine + Environment.NewLine))
        {
            var pair = item.Split(Environment.NewLine);
            items.Add(this.FromJsonString(pair[0]));
            items.Add(this.FromJsonString(pair[1]));
        }

        var divider1 = new object[] { new object[] { 2 } };
        var divider2 = new object[] { new object[] { 6 } };
        items.Add(divider1);
        items.Add(divider2);
        items.Sort(this.ComparePare);
        var index1 = items.FindIndex(p => p == divider1) + 1;
        var index2 = items.FindIndex(p => p == divider2) + 1;
        return index1 * index2;
    }

    private int ComparePare(object a, object b)
    {
        if (a is int aAsInt && b is int bAsInt)
        {
            return aAsInt < bAsInt ? RightOrder : aAsInt > bAsInt ? WrongOrder : TiedOrder;
        }

        var aArray = a is object[] aAsArray ? aAsArray : new[] { a };
        var bArray = b is object[] bAsArray ? bAsArray : new[] { b };

        var smallestArrayLength = Math.Min(aArray.Length, bArray.Length);
        if (aArray.Length == bArray.Length && smallestArrayLength == 0)
        {
            return TiedOrder;
        }

        for (int i = 0; i < smallestArrayLength; i++)
        {
            var result = this.ComparePare(aArray[i], bArray[i]);
            if (result != TiedOrder)
            {
                return result;
            }
        }

        return aArray.Length.CompareTo(bArray.Length);
    }

    public object FromJsonString(string json)
    {
        var element = (JsonElement)JsonSerializer.Deserialize<object>(json)!;
        return FromJsonElement(element);
    }

    private static object FromJsonElement(JsonElement element) =>
        element.ValueKind switch
        {
            JsonValueKind.Number => element.GetInt32(),
            JsonValueKind.Array => element.EnumerateArray().Select(FromJsonElement).ToArray(),
            _ => throw new ArgumentOutOfRangeException(nameof(element), "Unsupported JSON element type"),
        };
}