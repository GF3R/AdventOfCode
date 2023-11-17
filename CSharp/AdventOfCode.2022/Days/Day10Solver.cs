namespace AdventOfCode.Twenty22.Days;

public class Day10Solver
{
    public int Solve(string input)
    {
        var commandLines = input.Split(Environment.NewLine);
        int instructionPointer = 0;
        int cycle = 1;
        int x = 1;
        bool inAddxCycle = false;
        string[] currentCommand = null!;
        var sum = 0;
        var line = "";
        do
        {
            line += this.GetChar(x, (cycle-1) % 40);

            if (cycle  % 40 == 0)
            {
                Console.WriteLine(line);
                sum += cycle * x;
                line = "";
            }


            if (inAddxCycle)
            {
                x += int.Parse(currentCommand[1]);
                inAddxCycle = false;
                instructionPointer++;
            }
            else
            {
                currentCommand = commandLines[instructionPointer].Split(' ');
                switch (currentCommand[0])
                {
                    case "noop":
                        instructionPointer++;
                        break;
                    case "addx":
                        inAddxCycle = true;
                        break;
                }
            }
            cycle++;

        } while (instructionPointer < commandLines.Length);

        return sum;
    }

    private char GetChar(int spritePos, int currentPixel)
    {
        if (spritePos + 1 == currentPixel || spritePos - 1 == currentPixel || spritePos == currentPixel)
        {
            return '#';
        }

        return '.';
    }
}