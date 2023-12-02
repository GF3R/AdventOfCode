namespace AdventOfCode.Twenty23
{
    using System;
    using System.IO;
    using AdventOfCode.Twenty23.Days;

    class Program
    {
        static void Main(string[] args)
        {
            // var aoCSolver = new Day1Solver();
            var aoCSolver = new Day2Solver();
            var (result1, result2) = aoCSolver.Solve(File.ReadAllText("./Inputs/Input2.txt"));
            Console.WriteLine("Result: " + result1);
            Console.WriteLine("Result2: " + result2);

            Console.ReadLine();        
        }
    }
}
