namespace AdventOfCode.Twenty23.Days
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Core;

    public class Day8Solver : BaseSolver
    {
        /*
         Result1: 12737
         Result2: 9064949303801
        */
        public override object SolvePart1(string input)
        {
            var lines = input.Split("\n");
            var instructions = lines[0].Trim();
            var networkElements = CreateNetworkElements(lines);

            var steps = 0;
            var currentElement = networkElements.First(e => e.Name == "AAA");
            return GetStepsToEnd(currentElement, instructions, elem => elem.IsZZZ);
        }

        public override object SolvePart2(string input)
        {
            var lines = input.Split("\n");
            var instructions = lines[0].Trim();
            var networkElements = CreateNetworkElements(lines);
            var startingNodes = networkElements.Where(k => k.Name[^1] == 'A');
            var steps = startingNodes.Select(elem => GetStepsToEnd(elem, instructions, elem => elem.IsFinalForGhost));
            return MathHelper.LCM(steps.ToList());
        }
        
        private static long GetStepsToEnd(NetworkElement element, string instructions, Func<NetworkElement, bool> checkIfFinal)
        {
            var steps = 0L;
            while (!checkIfFinal(element))
            {
                element = instructions[(int) steps % instructions.Length] == 'L' ? element.Left : element.Right;
                steps++;
            }

            return steps;
        }
        

        private static List<NetworkElement> CreateNetworkElements(string[] lines)
        {
            var networkElements = lines.Skip(2).Select(l => new NetworkElement(l)).ToList();
            var networkElementDict = networkElements.ToDictionary(n => n.Name, n => n);

            foreach (var element in networkElements)
            {
                element.Left = networkElementDict[element.LeftName];
                element.Right = networkElementDict[element.RightName];
            }

            return networkElements;
        }
    }

    public class NetworkElement
    {
        public string LeftName { get; set; }

        public NetworkElement Left { get; set; }

        public string RightName { get; set; }

        public NetworkElement Right { get; set; }

        public string Name { get; set; }

        public bool IsZZZ { get; set; }

        public bool IsFinalForGhost { get; set; }

        public NetworkElement(string input)
        {
            var regex = new Regex(@"[A-Z0-9]{3}");
            var matches = regex.Matches(input);
            Name = matches[0].Value;
            LeftName = matches[1].Value;
            RightName = matches[2].Value;
            IsZZZ = Name == "ZZZ";
            IsFinalForGhost = Name.EndsWith("Z");
        }

    }
}
