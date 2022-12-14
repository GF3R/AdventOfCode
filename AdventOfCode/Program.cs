using AdventOfCode.Days;

// var aoCSolver = new Day1Solver();
// var aoCSolver = new Day2Solver();
// var aoCSolver = new Day3Solver();
// var aoCSolver = new Day4Solver();
// var aoCSolver = new Day5Solver();
// var aoCSolver = new Day6Solver
// {
//     PacketSize = 14
// };

// var aoCSolver = new Day7Solver();
// var aoCSolver = new Day8Solver();
// var aoCSolver = new Day9Solver();
// var aoCSolver = new Day10Solver();
// var aoCSolver = new Day11Solver();
// var aoCSolver = new Day12Solver();
// var aoCSolver = new Day13Solver();
var aoCSolver = new Day14Solver();
var (result1, result2) = aoCSolver.Solve(File.ReadAllText("./Inputs/Input14.txt"));
Console.WriteLine("Result: " + result1);
Console.WriteLine("Result2: " + result2);

Console.ReadLine();