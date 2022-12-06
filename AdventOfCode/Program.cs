using AdventOfCode;
using AdventOfCode.Days;

// var aoCSolver = new CodeDay1();
// var aoCSolver = new CodeDay3();
// var aoCSolver = new CodeDay4();
// var aoCSolver = new CodeDay5();
var aoCSolver = new CodeDay6
{
    PacketSize = 14
};

var result = aoCSolver.Solve(File.ReadAllText("./Inputs/Input6.txt"));
Console.WriteLine("Result: " + result);