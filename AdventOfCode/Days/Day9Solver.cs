namespace AdventOfCode.Days;

public class Day9Solver
{
    public int Solve(string? input, int numberOfKnots)
    {
        var visited = new HashSet<(int, int)>();
        var knots = Enumerable.Range(0, numberOfKnots).Select(t => new SnakePoint { X = 0, Y = 0 }).ToList();
        foreach (var step in input.Split(Environment.NewLine).Where(t => !string.IsNullOrWhiteSpace(t)))
        {
            for (var steps = 0; steps < int.Parse(step[2..]); ++steps)
            {
                MoveHead(knots[0], step[0]);
                for (var knotPlace = 0; knotPlace < knots.Count - 1; knotPlace++)
                {
                    MoveTail(knots[knotPlace], knots[knotPlace + 1]);
                }
                TryAdd(visited, (knots[^1].X, knots[^1].Y));
            }
        }

        return visited.Count;
    }


    private void MoveHead(SnakePoint head, char direction)
    {
        switch (direction)
        {
            case 'U':
                head.Y++;
                break;
            case 'D':
                head.Y--;
                break;
            case 'L':
                head.X--;
                break;
            case 'R':
                head.X++;
                break;
        }
    }

    private void MoveTail(SnakePoint knotInFront, SnakePoint knotBehind)
    {
        if (Math.Abs(knotInFront.X - knotBehind.X) > 1)
        {
            knotBehind.X += knotInFront.X > knotBehind.X ? 1 : -1;
            //If we are in a different row as well we can move diagonally
            if (knotBehind.Y != knotInFront.Y)
            {
                knotBehind.Y += knotInFront.Y > knotBehind.Y ? 1 : -1;
            }
        }
        
        if (Math.Abs(knotInFront.Y - knotBehind.Y) > 1)
        {
            knotBehind.Y += knotInFront.Y > knotBehind.Y ? 1 : -1;
            //If we are in a different column as well we can move diagonally
            if (knotBehind.X != knotInFront.X)
            {
                knotBehind.X += knotInFront.X > knotBehind.X ? 1 : -1;
            }
        }
    }

    private static void TryAdd<T>(HashSet<T> set, T item)
    {
        if (!set.Contains(item))
        {
            set.Add(item);
        }
    }

    private class SnakePoint
    {
        public int X;
        public int Y;
    }
}