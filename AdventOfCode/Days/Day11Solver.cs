namespace AdventOfCode.Days;

public class Day11Solver
{
    public long Solve(string input, int rounds = 20, bool relief = true)
    {
        var monkeyStrings = input.Split(Environment.NewLine + Environment.NewLine);
        var orchestrator = new MonkeyOrchestrator
        {
            ModuloShift = 1
        };
        foreach (var monkeyString in monkeyStrings)
        {
            orchestrator.ParseAndAddMonkey(monkeyString);
            orchestrator.ModuloShift *= orchestrator.Monkeys.Last().Value.DivisibleByNumber;
        }


        for (var round = 0; round < rounds; round++)
        {
            orchestrator.PerformARound(relief);
        }

        return orchestrator.GetMagicNumber();
    }
}

public class MonkeyOrchestrator
{
    public MonkeyOrchestrator()
    {
        Monkeys = new Dictionary<int, Monkey>();
    }

    public int ModuloShift { get; set; }

    public Dictionary<int, Monkey> Monkeys { get; }

    public long GetMagicNumber()
    {
        var orderedMonkeys = Monkeys.Values.Select(m => (long)m.Inspections).OrderByDescending(m => m).ToList();
        return orderedMonkeys[0] * orderedMonkeys[1];
    }

    public void PerformARound(bool relief)
    {
        foreach (var monkey in Monkeys.Select(monkeyPair => monkeyPair.Value))
        {
            while (monkey.Items.Count > 0)
            {
                monkey.Inspections += 1;
                var item = monkey.Items[0];
                var newWorryLevel = monkey.CalculateWorryLevel(item);
                if (relief)
                {
                    newWorryLevel /= 3;
                    Console.WriteLine($"relieved your worry level is {newWorryLevel}");
                }
                else
                {

                    if (item > newWorryLevel)
                    {
                        throw new OverflowException();
                    }
                    newWorryLevel %= ModuloShift;
                }

                var monkeyToGiveItem = monkey.DetermineWhichMonkeyToThrowTo((int)newWorryLevel);
                monkey.Items.Remove(item);
                Monkeys[monkeyToGiveItem].Items.Insert(0, (int)newWorryLevel);
            }
        }
    }

    public void ParseAndAddMonkey(string input)
    {
        var inputs = input.Split(Environment.NewLine).ToArray();
        var startingItems = RegexHelper.GetNumbers(inputs[1]).ToList();
        Monkeys.Add(RegexHelper.GetNumber(inputs[0]), new Monkey(inputs[2], string.Join("", inputs[3..]), startingItems));
    }
}

public class Monkey
{
    private int _monkeyNumberIfFalse;
    private int _monkeyNumberIfTrue;
    public int DivisibleByNumber;

    public Monkey(string operationString, string testString, List<int> items)
    {
        Items = items;
        SetTestParameters(testString);
        SetOperation(operationString);
        Inspections = 0;
    }

    private Func<int, long> Operation { get; set; }

    public List<int> Items { get; }

    public int Inspections { get; set; }


    public int DetermineWhichMonkeyToThrowTo(int value)
    {
        return value % DivisibleByNumber == 0 ? _monkeyNumberIfTrue : _monkeyNumberIfFalse;
    }

    public long CalculateWorryLevel(int number)
    {
        return Operation(number);
    }

    private void SetOperation(string operationString)
    {
        Operation = oldValue =>
        {
            var operationParts = operationString.Split(" = ")[1].Split(" ");
            var operationValue1 = GetNumberOrOld(operationParts[0], oldValue);
            var operationValue2 = GetNumberOrOld(operationParts[2], oldValue);
            return operationParts[1] switch
            {
                "+" => operationValue1 + operationValue2,
                "*" => operationValue1 * operationValue2,
                "/" => operationValue1 / operationValue2,
                "-" => operationValue1 - operationValue2,
                _ => throw new Exception("Unknown operation type")
            };
        };
    }

    private long GetNumberOrOld(string input, int old)
    {
        return input == "old" ? old : long.Parse(input);
    }

    private void SetTestParameters(string testString)
    {
        var numbers = RegexHelper.GetNumbers(testString).ToList();
        DivisibleByNumber = numbers[0];
        _monkeyNumberIfTrue = numbers[1];
        _monkeyNumberIfFalse = numbers[2];
    }
}