﻿using AdventOfCode.Days;

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
var aoCSolver = new Day11Solver();
var result = aoCSolver.Solve(File.ReadAllText("./Inputs/Input11.txt"), 10000, false);
Console.WriteLine("Result: " + result);