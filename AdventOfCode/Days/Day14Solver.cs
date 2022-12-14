using System.Text;

namespace AdventOfCode.Days;

public class Day14Solver : BaseSolver
{
    private readonly string possibleInput = @"498,4 -> 498,6 -> 496,6
503,4 -> 502,4 -> 502,9 -> 494,9";

    Dictionary<(int, int), CavePoint> cavePoints = new Dictionary<(int, int), CavePoint>();

    int maxX;
    int maxY;
    int minX = int.MaxValue;
    int minY = int.MaxValue;

    public override object SolvePart1(string input)
    {
        CreateRocks(input);
        CreateSand();
        PrintPoints();
        return cavePoints.Values.Count(x => x.Type == Material.Sand);
    }

    public override object SolvePart2(string input)
    {
        CreateRocks(input);
        CreateSand();
        PrintPoints();
        return cavePoints.Values.Count(x => x.Type == Material.Sand);
    }

    private void CreateSand()
    {
        var startPoint = new CavePoint
        {
            Type = Material.FallingSand,
            X = 500,
            Y = 0,
        };

        var currentPoint = startPoint;
        int triedToMoveLeft = 0;
        int triedToMoveRight = 0;
        while (currentPoint.Y < 5000)
        {
            // move down
            var pointBelow = GetCavePoint(currentPoint.X, currentPoint.Y + 1);
            if (pointBelow.Type == Material.Air)
            {
                currentPoint = PointToFallingSand(pointBelow);
                continue;
            }


            var pointDownLeft = GetCavePoint(currentPoint.X - 1, currentPoint.Y + 1);
            if (triedToMoveLeft != pointDownLeft.Y)
            {
                // move left
                if (pointDownLeft.Type == Material.Air)
                {
                    currentPoint = PointToFallingSand(pointDownLeft);
                }
                else
                {
                    triedToMoveLeft = pointDownLeft.Y;
                }

                continue;
            }

            var pointDownRight = GetCavePoint(currentPoint.X + 1, currentPoint.Y + 1);
            if (triedToMoveRight != pointDownRight.Y)
            {
                // move right
                if (pointDownRight.Type == Material.Air)
                {
                    currentPoint = PointToFallingSand(pointDownRight);
                }
                else
                {
                    triedToMoveRight = pointDownRight.Y;
                }

                continue;
            }

            if (currentPoint.Type == Material.FallingSand)
            {
                currentPoint.Type = Material.Sand;
                cavePoints[currentPoint.GetPoint()] = currentPoint;
                cavePoints = cavePoints.Where(t => t.Value.Type != Material.FallingSand).ToDictionary(t => t.Key, t => t.Value);
                currentPoint = new CavePoint
                {
                    Type = Material.FallingSand,
                    X = 500,
                    Y = 0,
                };
                triedToMoveLeft = 0;
                triedToMoveRight = 0;
            }
        }
    }

    private CavePoint PointToFallingSand(CavePoint pointBelow)
    {
        CavePoint currentPoint = pointBelow;
        currentPoint.Type = Material.FallingSand;
        cavePoints.Add(currentPoint.GetPoint(), currentPoint);
        return currentPoint;
    }

    private CavePoint GetCavePoint(int x, int y)
    {
        if (cavePoints.ContainsKey((x, y)))
        {
            return cavePoints[(x, y)];
        }

        return new CavePoint
        {
            X = x,
            Y = y,
            Type = Material.Air
        };
    }

    private void CreateRocks(string input)
    {
        foreach (var line in input.Split(Environment.NewLine))
        {
            CavePoint? lastCavePoint = null;
            var parts = line.Split("->");
            foreach (var part in parts)
            {
                var coords = part.Split(",");
                var x = int.Parse(coords[0]);
                var y = int.Parse(coords[1]);
                maxX = Math.Max(maxX, x);
                maxY = Math.Max(maxY, y);
                minX = Math.Min(minX, x);
                minY = Math.Min(minY, y);
                var newCavePoint = new CavePoint
                {
                    X = x,
                    Y = y,
                    Type = Material.Rock
                };
                if (!cavePoints.ContainsKey(newCavePoint.GetPoint()))
                {
                    cavePoints.Add(newCavePoint.GetPoint(), newCavePoint);
                }

                lastCavePoint = lastCavePoint == null ? newCavePoint : CreateCavepointsFromLastToNext(newCavePoint, lastCavePoint);
            }
        }
    }

    private void PrintPoints()
    {
        var sb = new StringBuilder();
        for (var y = minY - 1; y <= maxY + 1; y++)
        {
            for (var x = minX - 1; x <= maxX + 1; x++)
            {
                var point = GetCavePoint(x, y);

                switch (point.Type)
                {
                    case Material.Rock:
                        sb.Append('#');
                        break;
                    case Material.FallingSand:
                        sb.Append('~');
                        break;
                    case Material.Air:
                        sb.Append('.');
                        break;
                    case Material.Sand:
                        sb.Append('o');
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            sb.Append(Environment.NewLine);
        }

        Console.Clear();
        Console.WriteLine(sb.ToString());
    }

    private CavePoint CreateCavepointsFromLastToNext(CavePoint newCavePoint, CavePoint lastCavePoint)
    {
        var xDiff = newCavePoint.X - lastCavePoint.X;
        var yDiff = newCavePoint.Y - lastCavePoint.Y;
        if (xDiff != 0 && yDiff != 0)
        {
            throw new Exception("Invalid input");
        }

        while (xDiff != 0)
        {
            lastCavePoint = new CavePoint
            {
                X = lastCavePoint.X + (xDiff > 0 ? 1 : -1),
                Y = lastCavePoint.Y,
                Type = Material.Rock
            };
            xDiff = newCavePoint.X - lastCavePoint.X;
            if (xDiff != 0)
            {
                if (!cavePoints.ContainsKey(lastCavePoint.GetPoint()))
                {
                    cavePoints.Add((lastCavePoint.X, lastCavePoint.Y), lastCavePoint);
                }
            }
        }

        while (yDiff != 0)
        {
            lastCavePoint = new CavePoint
            {
                X = lastCavePoint.X,
                Y = lastCavePoint.Y + (yDiff > 0 ? 1 : -1),
                Type = Material.Rock
            };
            yDiff = newCavePoint.Y - lastCavePoint.Y;
            if (yDiff != 0)
            {
                if (!cavePoints.ContainsKey(lastCavePoint.GetPoint()))
                {
                    cavePoints.Add((lastCavePoint.X, lastCavePoint.Y), lastCavePoint);
                }
            }
        }

        return lastCavePoint;
    }

    public override object SolvePart2(string input)
    {
        return null;
    }
}

class CavePoint
{
    public int X { get; set; }
    public int Y { get; set; }

    public Material Type { get; set; } = Material.Air;

    public (int, int) GetPoint()
    {
        return (X, Y);
    }
}

public enum Material
{
    Rock,
    Air,
    Sand,
    FallingSand,
}