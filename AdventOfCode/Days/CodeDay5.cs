using System.Text.RegularExpressions;

namespace AdventOfCode;

public class CodeDay5
{
    public bool IsCrateMover9001 = false;

    public object Solve(string input)
    {
        var inputParameters = input.Split(Environment.NewLine + Environment.NewLine);
        var boxStack = ParseBoxStackFromInput(inputParameters[0]);
        ProcessInputOnBoxStack(inputParameters[1], boxStack);
        var result = string.Join(string.Empty, boxStack.Select(t => t.Value.First()).ToArray());
        Console.WriteLine("Result: " + result);
        return result;
    }
    
    private void ProcessInputOnBoxStack(string inputParameter, Dictionary<int, List<string>> boxStack)
    {
        var inputLines = inputParameter.Split(Environment.NewLine);
        foreach (var inputLine in inputLines)
        {
            var inputLineParts = inputLine.Split(" ");
            var take = int.Parse(inputLineParts[1]);
            var from = int.Parse(inputLineParts[3]);
            var to = int.Parse(inputLineParts[5]);
            var boxStackFrom = boxStack[from].GetRange(0, take);
            boxStack[from].RemoveRange(0, take);
            if (IsCrateMover9001)
            {
                boxStackFrom.Reverse();
            }

            foreach (var boxes in boxStackFrom)
            {
                boxStack[to].Insert(0, boxes);
            }
        }
    }


    private static Dictionary<int, List<string>> ParseBoxStackFromInput(string boxStackInput)
    {
        var regex = new Regex(@"[A-Z]|\s{4}");

        var craneInput = boxStackInput.Split(Environment.NewLine);
        var boxes = craneInput[^1].Split("   ").Select(t => int.Parse(t.Trim())).ToDictionary(t => t, t => new List<string>());
        foreach (var craneLine in craneInput.Take(craneInput.Length - 1))
        {
            var craneLineParts = regex.Matches(craneLine).Select(m => m.Value).ToArray();
            for (var i = 0; i < craneLineParts.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(craneLineParts[i]))
                {
                    continue;
                }

                boxes[i + 1].Add(craneLineParts[i]);
            }
        }

        return boxes;
    }
}