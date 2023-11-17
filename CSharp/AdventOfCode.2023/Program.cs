using System;

namespace AdventOfCode._2023
{
    using System.IO;
    using Days;

    class Program
    {
        static void Main(string[] args)
        {
            var aoCSolver = new Day1Solver();
            var (result1, result2) = aoCSolver.Solve(File.ReadAllText("./Inputs/Input14.txt"));
            Console.WriteLine("Result: " + result1);
            Console.WriteLine("Result2: " + result2);

            Console.ReadLine();        }
    }
}
