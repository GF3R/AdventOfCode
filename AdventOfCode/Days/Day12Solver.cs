namespace AdventOfCode.Days;

public class Day12Solver
{
    public int Solve(string input, bool fromStart = true)
    {
        var points = BuildGraph(input);
        var q = new Queue<(int, int)>();
        var goal = points.Single(p => p.Value.IsEnd).Value;
        q.Enqueue((goal.X, goal.Y));
        var poiByCoord = new Dictionary<(int, int), Point>
        {
            { (goal.X, goal.Y), goal }
        };

        while (q.Any())
        {
            var (x, y) = q.Dequeue();
            var thisPoi = poiByCoord[(x, y)];

            foreach (var nextCoord in thisPoi.Neighbors)
            {
                if (poiByCoord.ContainsKey((nextCoord.X, nextCoord.Y)))
                {
                    continue;
                }

                var nextPoi = points[(nextCoord.X, nextCoord.Y)];
                poiByCoord.Add((nextCoord.X, nextCoord.Y), nextPoi);
                nextPoi.DistanceFromGoal = (thisPoi.DistanceFromGoal ?? 0) + 1;
                q.Enqueue((nextPoi.X, nextPoi.Y));
            }
        }

        if (fromStart)
        {
            return points.Select(p => p.Value).Single(p => p.IsStart).DistanceFromGoal!.Value;
        }

        return points.Select(p => p.Value).Where(p => p.Value == 0 && p.DistanceFromGoal.HasValue).Min(p => p.DistanceFromGoal!.Value);
    }

    private Dictionary<(int, int), Point> BuildGraph(string input)
    {
        var points = new Dictionary<(int, int), Point>();

        var lines = input.Split(Environment.NewLine);
        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            for (var j = 0; j < line.Length; j++)
            {
                var isStart = line[j] == 'S';
                var isEnd = line[j] == 'E';
                var point = new Point
                {
                    Value = (isStart ? 'a' : isEnd ? 'z' : line[j]) - 'a',
                    X = i,
                    Y = j,
                    IsEnd = isEnd,
                    IsStart = isStart
                };
                points.Add((i, j), point);
            }
        }

        foreach (var point in points)
        {
            point.Value.Neighbors = GetValidNeighbours(points, point.Value);
        }

        return points;
    }

    private static IEnumerable<Point> GetValidNeighbours(Dictionary<(int, int), Point> points, Point point)
    {
        return points.Where(p => IsValidEdge(point.Value, p.Value.Value) && IsNeighbour(p.Value, point)).Select(p => p.Value);
    }

    private static bool IsValidEdge(int nextEdge, int currentEdge)
    {
        return nextEdge - currentEdge is 1 or 0 || nextEdge < currentEdge;
    }

    private static bool IsNeighbour(Point pointa, Point pointb)
    {
        return pointa.X == pointb.X && Math.Abs(pointa.Y - pointb.Y) == 1 || pointa.Y == pointb.Y && Math.Abs(pointa.X - pointb.X) == 1;
    }
}

public class Point
{
    public int X { get; init; }
    public int Y { get; init; }
    public IEnumerable<Point> Neighbors { get; set; } = null!;
    public int Value { get; init; } = -1;
    public bool IsStart { get; init; }
    public bool IsEnd { get; init; }
    public int? DistanceFromGoal { get; set; }
}