namespace AdventOfCode;

public class CodeDay2
{
    private const string ROCK = "A";
    private const string PAPER = "B";
    private const string SCISSORS = "C";
    private const string OUTCOMELOSE = "X";
    private const string OUTCOMEWDRAW = "Y";
    private const string OUTCOMEWIN = "Z";

    public void Solve(string input)
    {
        var parsedList = input.Split('\n').Select(s => s.Split(" "));
        var score = 0;
        
        foreach (var item in parsedList)
        {
            var oponent = item[0].Trim();
            var outcome = item[1].Trim();
            var mine = ToMySign(oponent, outcome);

            score += Solver(oponent, mine);
            score += MyItemToPoint(mine);
        }

        Console.WriteLine($"the score is {score}");
    }

    private static string ToMySign(string oponent, string outcome)
    {
        switch (oponent)
        {
            case ROCK:
                switch (outcome)
                {
                    case OUTCOMEWIN:
                        return PAPER;
                    case OUTCOMELOSE:
                        return SCISSORS;
                    case OUTCOMEWDRAW:
                        return ROCK;
                }

                break;
            case PAPER:
                switch (outcome)
                {
                    case OUTCOMEWIN:
                        return SCISSORS;
                    case OUTCOMELOSE:
                        return ROCK;
                    case OUTCOMEWDRAW:
                        return PAPER;
                }

                break;
            case SCISSORS:
                switch (outcome)
                {
                    case OUTCOMEWIN:
                        return ROCK;
                    case OUTCOMELOSE:
                        return PAPER;
                    case OUTCOMEWDRAW:
                        return SCISSORS;
                }

                break;
        }

        throw new ArgumentException();
    }

    private int MyItemToPoint(string mine)
    {
        return mine switch
        {
            ROCK => 1,
            PAPER => 2,
            SCISSORS => 3,
            _ => throw new ArgumentException()
        };
    }

    private static int Solver(string oponent, string mine)
    {
        switch (oponent)
        {
            case ROCK:
                switch (mine)
                {
                    case ROCK:
                        return 3;
                    case SCISSORS:
                        return 0;
                    case PAPER:
                        return 6;
                }

                break;
            case PAPER:
                switch (mine)
                {
                    case ROCK:
                        return 0;
                    case SCISSORS:
                        return 6;
                    case PAPER:
                        return 3;
                }

                break;
            case SCISSORS:
                switch (mine)
                {
                    case ROCK:
                        return 6;
                    case SCISSORS:
                        return 3;
                    case PAPER:
                        return 0;
                }

                break;
        }

        throw new ArgumentException();
    }
}