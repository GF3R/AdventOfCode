using AdventOfCode.Days;

// var aoCSolver = new Day1Solver();
// var aoCSolver = new Day2Solver();
// var aoCSolver = new Day3Solver();
// var aoCSolver = new Day4Solver();
// var aoCSolver = new Day5Solver();
var aoCSolver = new Day6Solver
{
    PacketSize = 14
};

var result = aoCSolver.Solve(File.ReadAllText("./Inputs/Input6.txt"));
Console.WriteLine("Result: " + result);