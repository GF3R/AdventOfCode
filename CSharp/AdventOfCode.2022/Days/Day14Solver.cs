namespace AdventOfCode.Twenty22.Days;

using AdventOfCode.Core;

public class Day14Solver : BaseSolver
{
    Dictionary<(int, int), CavePoint> _cavePoints = new Dictionary<(int, int), CavePoint>();

    int _maxX;
    int _maxY;
    int _minX = int.MaxValue;
    int _minY = int.MaxValue;

    public override object SolvePart1(string input)
    {
        _cavePoints.Clear();
        this.CreateRocks(input);
        this.CreateSand();
        this.PrintPoints();
        return _cavePoints.Values.Count(x => x.Type == Material.Sand);
    }

    public override object SolvePart2(string input)
    {
        _cavePoints.Clear();
        this.CreateRocks(input, true);
        this.CreateSand();
        this.PrintPoints();
        return _cavePoints.Values.Count(x => x.Type == Material.Sand);
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
            var pointBelow = this.GetCavePoint(currentPoint.X, currentPoint.Y + 1);
            if (pointBelow.Type == Material.Air)
            {
                currentPoint = this.PointToFallingSand(pointBelow);
                continue;
            }


            var pointDownLeft = this.GetCavePoint(currentPoint.X - 1, currentPoint.Y + 1);
            if (triedToMoveLeft != pointDownLeft.Y)
            {
                // move left
                if (pointDownLeft.Type == Material.Air)
                {
                    currentPoint = this.PointToFallingSand(pointDownLeft);
                }
                else
                {
                    triedToMoveLeft = pointDownLeft.Y;
                }

                continue;
            }

            var pointDownRight = this.GetCavePoint(currentPoint.X + 1, currentPoint.Y + 1);
            if (triedToMoveRight != pointDownRight.Y)
            {
                // move right
                if (pointDownRight.Type == Material.Air)
                {
                    currentPoint = this.PointToFallingSand(pointDownRight);
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
          

                _cavePoints[currentPoint.GetPoint()] = currentPoint;
                _cavePoints = _cavePoints.Where(t => t.Value.Type != Material.FallingSand).ToDictionary(t => t.Key, t => t.Value);
                if (currentPoint is { X: 500, Y: 0 })
                {
                    break;
                }
                currentPoint = new CavePoint
                {
                    Type = Material.FallingSand,
                    X = 500,
                    Y = 0,
                };
                triedToMoveLeft = 0;
                triedToMoveRight = 0;
            }

            this.PrintPoints();
        }
    }

    private CavePoint PointToFallingSand(CavePoint pointBelow)
    {
        CavePoint currentPoint = pointBelow;
        currentPoint.Type = Material.FallingSand;
        _cavePoints.Add(currentPoint.GetPoint(), currentPoint);
        return currentPoint;
    }

    private CavePoint GetCavePoint(int x, int y)
    {
        if (_cavePoints.ContainsKey((x, y)))
        {
            return _cavePoints[(x, y)];
        }

        return new CavePoint
        {
            X = x,
            Y = y,
            Type = Material.Air
        };
    }

    private void CreateRocks(string input, bool includeFloor = false)
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
                _maxX = Math.Max(_maxX, x);
                _maxY = Math.Max(_maxY, y);
                _minX = Math.Min(_minX, x);
                _minY = Math.Min(_minY, y);
                var newCavePoint = new CavePoint
                {
                    X = x,
                    Y = y,
                    Type = Material.Rock
                };
                if (!_cavePoints.ContainsKey(newCavePoint.GetPoint()))
                {
                    _cavePoints.Add(newCavePoint.GetPoint(), newCavePoint);
                }

                lastCavePoint = lastCavePoint == null ? newCavePoint : this.CreateCavepointsFromLastToNext(newCavePoint, lastCavePoint);
            }
        }

        if (!includeFloor) return;
        
        for (var i = _minX * -3; i < _maxX * 3; i++)
        {
            var point = new CavePoint
            {
                Type = Material.Rock,
                X = i,
                Y = _maxY + 2
            };
            _cavePoints.Add(point.GetPoint(), point);
        }
    }

    private void PrintPoints()
    {
        var output = string.Empty;
        for (var y = _minY - 50; y <= _maxY + 10; y++)
        {
            for (var x = _minX - 50; x <= _maxX + 50; x++)
            {
                var point = this.GetCavePoint(x, y);

                switch (point.Type)
                {
                    case Material.Rock:
                        output +='#';
                        break;
                    case Material.FallingSand:
                        output+='~';
                        break;
                    case Material.Air:
                        output+='.';
                        break;
                    case Material.Sand:
                        output+='o';
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            output+=Environment.NewLine;
        }
        Console.WriteLine("\r" + output);
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
                if (!_cavePoints.ContainsKey(lastCavePoint.GetPoint()))
                {
                    _cavePoints.Add((lastCavePoint.X, lastCavePoint.Y), lastCavePoint);
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
                if (!_cavePoints.ContainsKey(lastCavePoint.GetPoint()))
                {
                    _cavePoints.Add((lastCavePoint.X, lastCavePoint.Y), lastCavePoint);
                }
            }
        }

        return lastCavePoint;
    }
}

class CavePoint
{
    public int X { get; set; }
    public int Y { get; set; }

    public Material Type { get; set; } = Material.Air;

    public (int, int) GetPoint()
    {
        return (this.X, this.Y);
    }
}

public enum Material
{
    Rock,
    Air,
    Sand,
    FallingSand,
}