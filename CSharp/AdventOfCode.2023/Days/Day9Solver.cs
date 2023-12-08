namespace AdventOfCode.Twenty23.Days
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using AdventOfCode.Days;
    using Core;

    public class Day9Solver : BaseSolver
    {
        public override object SolvePart1(string input)
        {
            var lines = input.Split("\n");
            var sum = 0;
            foreach(var line in lines)
            {
                var measurementHistory = new MeasurementHistory(line);
                var nextNumber = GetNextNumber(measurementHistory);
                Console.WriteLine($"Next Number: {nextNumber}");
                Console.WriteLine(measurementHistory.ToString());
                if(nextNumber < 0)
                {
                    Console.WriteLine($"Negative number found {nextNumber}");
                }
                sum += nextNumber;
            }
            
            return sum;
        }

        public override object SolvePart2(string input)
        {
            var lines = input.Split("\n");
            var sum = 0;
            foreach(var line in lines)
            {
                var measurementHistory = new MeasurementHistory(line);
                var nextNumber = GetPreviousNumber(measurementHistory);
                Console.WriteLine($"Next Number: {nextNumber}");
                Console.WriteLine(measurementHistory.ToString());
                if(nextNumber < 0)
                {
                    Console.WriteLine($"Negative number found {nextNumber}");
                }
                sum += nextNumber;
            }
            
            return sum;
        }

        private static int GetPreviousNumber(MeasurementHistory measurementHistory)
        {
            var currentMeasurement = measurementHistory;
            while (!currentMeasurement.AreAllNumbersTheSame())
            {
                currentMeasurement.OneDown = currentMeasurement.GetValuesOneDown();
                currentMeasurement.OneDown.OneUp = currentMeasurement;
                currentMeasurement = currentMeasurement.OneDown;
            }
            
            return GetTop(currentMeasurement).GetPreviousValue();
        }
        
        private static int GetNextNumber(MeasurementHistory measurementHistory)
        {
            var currentMeasurement = measurementHistory;
            while (!currentMeasurement.AreAllNumbersTheSame())
            {
                currentMeasurement.OneDown = currentMeasurement.GetValuesOneDown();
                currentMeasurement.OneDown.OneUp = currentMeasurement;
                currentMeasurement = currentMeasurement.OneDown;
            }
            
            return GetTop(currentMeasurement).GetNextValue();
        }

        private static MeasurementHistory GetTop(MeasurementHistory measurementHistory)
        {
            if(measurementHistory.OneUp == null)
            {
                return measurementHistory;
            }
            
            return GetTop(measurementHistory.OneUp);
        }
    }

    public class MeasurementHistory
    {
        public List<int> Measurements { get; set; }
        
        public MeasurementHistory OneUp { get; set; }
        public MeasurementHistory OneDown { get; set; }
        
        public MeasurementHistory(string line)
        {
            this.Measurements = line.Split(" ").Select(int.Parse).ToList();
        }
        
        protected MeasurementHistory(List<int> measurements)
        {
            this.Measurements = measurements;
        }
        
        public override string ToString()
        {
            var result = string.Join(",", this.Measurements);
            var oneDown = this.OneDown;
            while (oneDown != null)
            {
                result += "\n";
                result += string.Join(",", oneDown.Measurements);
                oneDown = oneDown.OneDown;
            }
            
            return result;
        }
        
        public bool AreAllNumbersTheSame()
        {
            return this.Measurements.All(m => m == this.Measurements.First());
        }

        public MeasurementHistory GetValuesOneDown()
        {
            var previous = this.Measurements.First();
            var measurements = new List<int>();
            
            foreach(var measurement in this.Measurements.Skip(1))
            {
                measurements.Add(measurement - previous);
                previous = measurement;
            }
            
            return new MeasurementHistory(measurements);
        }

        public int GetNextValue()
        {
            if (this.AreAllNumbersTheSame())
            {
                return this.Measurements.First();
            }
            
            return this.Measurements.Last() + this.OneDown.GetNextValue();
        }
        
        public int GetPreviousValue()
        {
            if (this.AreAllNumbersTheSame())
            {
                return this.Measurements.First();
            }
            
            return this.Measurements.First() - this.OneDown.GetPreviousValue();
        }
    }
  
}
