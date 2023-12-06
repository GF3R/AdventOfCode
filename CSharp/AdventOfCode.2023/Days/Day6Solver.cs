namespace AdventOfCode.Twenty23.Days
{
    using System;
    using System.Linq;
    using AdventOfCode.Days;
    using Core;

    public class Day6Solver : BaseSolver
    {
        public override object SolvePart1(string input)
        {
            var lines = input.Split("\n");
            var times = lines[0].GetNumbers().ToArray();
            var distances = lines[1].GetNumbers().ToArray();
            var sum = 1L;
            for (int i = 0; i < times.Length; i++)
            {
                var race = new Race(times[i], distances[i]);
                var numOfSolutions = race.GetNumberOfSolutions();
                Console.WriteLine($"Number of solutions: {numOfSolutions}");
                sum *= numOfSolutions;
            }


            return sum;
        }

        public override object SolvePart2(string input)
        {
            var lines = input.Split("\n");
            var time = lines[0].Replace(" ", string.Empty).GetLongNumbers().ToArray()[0];
            var distance = lines[1].Replace(" ", string.Empty).GetLongNumbers().ToArray()[0];
            var race = new Race(time, distance);
            return race.GetNumberOfSolutions();
        }
    }
    

    public class Race
    {
        public long Time { get; set; }
        public long Distance { get; set; }

        public Race(long time, long distance)
        {
            this.Time = time;
            this.Distance = distance;
        }

        public long GetDistance(long numberOfMiliseconds, long speed)
        {
            return numberOfMiliseconds * speed;
        }

        public long GetNumberOfSolutions()
        {
            var solutions = 0;
            for (long i = 1; i <= this.Time; i++)
            {
                if (GetDistance(this.Time - i, i) > this.Distance)
                {
                    solutions++;
                }
            }

            return solutions;
        }
    }

}
