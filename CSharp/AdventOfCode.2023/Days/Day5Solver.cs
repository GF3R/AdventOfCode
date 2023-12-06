namespace AdventOfCode.Twenty23.Days
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using AdventOfCode.Days;
    using Core;

    public class Day5Solver : BaseSolver
    {
        public List<SourceToTargetMap> SeedToSoilMap { get; set; } = new();
        public List<SourceToTargetMap> SoilToFertilizer { get; set; } = new();
        public List<SourceToTargetMap> FertilizerToWater { get; set; } = new();
        public List<SourceToTargetMap> WaterToLight { get; set; } = new();
        public List<SourceToTargetMap> LightToTemperature { get; set; } = new();
        public List<SourceToTargetMap> TemperatureToHumidity { get; set; } = new();
        public List<SourceToTargetMap> HumidityToLocation { get; set; } = new();

        public long[] Seeds { get; set; }

        public List<Seed> SeedList { get; set; } = new();

        public override object SolvePart1(string input)
        {
            var lines = input.Split("\n");
            ParseInput(lines);
            var lowestLocation = long.MaxValue;
            foreach (var seed in Seeds)
            {
                var soil = SeedToSoilMap.GetValue(seed);
                var fertilizer = SoilToFertilizer.GetValue(soil);
                var water = FertilizerToWater.GetValue(fertilizer);
                var light = WaterToLight.GetValue(water);
                var temperature = LightToTemperature.GetValue(light);
                var humidity = TemperatureToHumidity.GetValue(temperature);
                var location = HumidityToLocation.GetValue(humidity);
                Console.WriteLine($"Seed: {seed}, Soil: {soil}, Fertilizer: {fertilizer}, Light: {light}, Temperature: {temperature}, Humidity: {humidity}, Location: {location}");

                if (location < lowestLocation)
                {
                    lowestLocation = location;
                }
            }

            return lowestLocation;
        }

        public override object SolvePart2(string input)
        {
            var lines = input.Split("\n");

            // Clear all lists
            SeedToSoilMap.Clear();
            SoilToFertilizer.Clear();
            FertilizerToWater.Clear();
            WaterToLight.Clear();
            LightToTemperature.Clear();
            TemperatureToHumidity.Clear();
            HumidityToLocation.Clear();
            SeedList.Clear();

            ParseInput(lines, false);

            var lowestLocation = long.MaxValue;
            var toLocationMaps = new ToLocationMaps();
            foreach (var seed in SeedList)
            {
                
                var maps = SeedToSoilMap.GetMaps(seed);
                
                var start = seed.Start;
                for(long i = start; i < seed.End; i++)
                {
                    if (toLocationMaps.SeedToLocationMap.TryGetValue(start, out var destination))
                    {
                        lowestLocation = Math.Min(lowestLocation, destination);
                        continue;
                    }
                    
                    var soil = SeedToSoilMap.GetValue(start);
                    
                    if(toLocationMaps.SoilToLocationMap.TryGetValue(soil, out destination))
                    {
                        toLocationMaps.SeedToLocationMap.Add(start, destination);
                        lowestLocation = Math.Min(lowestLocation, destination);
                        continue;
                    }
                    
                    var fertilizer = SoilToFertilizer.GetValue(soil);

                    if(toLocationMaps.FertilizerToLocationMap.TryGetValue(fertilizer, out destination))
                    {
                        toLocationMaps.SeedToLocationMap.Add(start, destination);
                        toLocationMaps.SoilToLocationMap.Add(soil, destination);
                        lowestLocation = Math.Min(lowestLocation, destination);
                        continue;
                    }
                    
                    var water = FertilizerToWater.GetValue(fertilizer);
                    
                    if(toLocationMaps.WaterToLocationMap.TryGetValue(water, out destination))
                    {
                        toLocationMaps.SeedToLocationMap.Add(start, destination);
                        toLocationMaps.SoilToLocationMap.Add(soil, destination);
                        toLocationMaps.FertilizerToLocationMap.Add(fertilizer, destination);
                        lowestLocation = Math.Min(lowestLocation, destination);
                        continue;
                    }
                    
                    var light = WaterToLight.GetValue(water);
                    
                    if(toLocationMaps.LightToLocationMap.TryGetValue(light, out destination))
                    {
                        toLocationMaps.SeedToLocationMap.Add(start, destination);
                        toLocationMaps.SoilToLocationMap.Add(soil, destination);
                        toLocationMaps.FertilizerToLocationMap.Add(fertilizer, destination);
                        toLocationMaps.WaterToLocationMap.Add(water, destination);
                        lowestLocation = Math.Min(lowestLocation, destination);
                        continue;
                    }
                    
                    var temperature = LightToTemperature.GetValue(light);
                    
                    if(toLocationMaps.TemperatureToLocationMap.TryGetValue(temperature, out destination))
                    {
                        toLocationMaps.SeedToLocationMap.Add(start, destination);
                        toLocationMaps.SoilToLocationMap.Add(soil, destination);
                        toLocationMaps.FertilizerToLocationMap.Add(fertilizer, destination);
                        toLocationMaps.WaterToLocationMap.Add(water, destination);
                        toLocationMaps.LightToLocationMap.Add(light, destination);
                        lowestLocation = Math.Min(lowestLocation, destination);
                        continue;
                    }
                    
                    var humidity = TemperatureToHumidity.GetValue(temperature);
                    
                    if(toLocationMaps.HumidityToLocationMap.TryGetValue(humidity, out destination))
                    {
                        toLocationMaps.SeedToLocationMap.Add(start, destination);
                        toLocationMaps.SoilToLocationMap.Add(soil, destination);
                        toLocationMaps.FertilizerToLocationMap.Add(fertilizer, destination);
                        toLocationMaps.WaterToLocationMap.Add(water, destination);
                        toLocationMaps.LightToLocationMap.Add(light, destination);
                        toLocationMaps.TemperatureToLocationMap.Add(temperature, destination);
                        lowestLocation = Math.Min(lowestLocation, destination);
                        continue;
                    }
                    
                    var location = HumidityToLocation.GetValue(humidity);
                    toLocationMaps.SeedToLocationMap.Add(start, location);
                    toLocationMaps.SoilToLocationMap.Add(soil, location);
                    toLocationMaps.FertilizerToLocationMap.Add(fertilizer, location);
                    toLocationMaps.WaterToLocationMap.Add(water, location);
                    toLocationMaps.LightToLocationMap.Add(light, location);
                    toLocationMaps.TemperatureToLocationMap.Add(temperature, location);
                    toLocationMaps.HumidityToLocationMap.Add(humidity, location);
                    lowestLocation = Math.Min(lowestLocation, location);
                    
                    start++;

                }
            }

            return lowestLocation;
        }

        public void ParseInput(string[] lines, bool part1 = true)
        {
            var mode = ParseMode.Seeds;
            foreach (var originalLine in lines)
            {
                var line = originalLine.Trim();
                switch (line)
                {
                    case "":
                        break;
                    case "seed-to-soil map:":
                        mode = ParseMode.SeedToSoil;
                        break;
                    case "soil-to-fertilizer map:":
                        mode = ParseMode.SoilToFertilizer;
                        break;
                    case "fertilizer-to-water map:":
                        mode = ParseMode.FertilizerToWater;
                        break;
                    case "water-to-light map:":
                        mode = ParseMode.WaterToLight;
                        break;
                    case "light-to-temperature map:":
                        mode = ParseMode.LightToTemperature;
                        break;
                    case "temperature-to-humidity map:":
                        mode = ParseMode.TemperatureToHumidity;
                        break;
                    case "humidity-to-location map:":
                        mode = ParseMode.HumidityToLocation;
                        break;
                    default:
                        if (line.Contains("seeds:"))
                        {
                            if (part1 == true)
                            {
                                this.Seeds = line.Split("seeds:")[1].GetLongNumbers().ToArray();
                            }
                            else
                            {
                                var seedInput = line.Split("seeds:")[1].GetLongNumbers().ToArray();
                                for (int i = 0; i < seedInput.Length; i += 2)
                                {
                                    var seedSource = seedInput[i];
                                    var seedLength = seedInput[i + 1];
                                    this.SeedList.Add(new Seed(seedSource, seedLength));
                                }
                            }

                            break;
                        }

                        ParseLineForMode(line, mode);
                        break;
                }
            }
        }

        private void ParseLineForMode(string line, ParseMode mode)
        {
            var numbers = line.GetLongNumbers().ToArray();
            var destinationRange = numbers[0];
            var sourceRange = numbers[1];
            var rangeLength = numbers[2];
            var map = new SourceToTargetMap(sourceRange, destinationRange, rangeLength);
            switch (mode)
            {
                case ParseMode.Seeds:
                    throw new Exception("Wrong Mode");
                case ParseMode.SeedToSoil:
                    this.SeedToSoilMap.Add(map);
                    break;
                case ParseMode.SoilToFertilizer:
                    this.SoilToFertilizer.Add(map);
                    break;
                case ParseMode.FertilizerToWater:
                    this.FertilizerToWater.Add(map);
                    break;
                case ParseMode.WaterToLight:
                    this.WaterToLight.Add(map);
                    break;
                case ParseMode.LightToTemperature:
                    this.LightToTemperature.Add(map);
                    break;
                case ParseMode.TemperatureToHumidity:
                    this.TemperatureToHumidity.Add(map);
                    break;
                case ParseMode.HumidityToLocation:
                    this.HumidityToLocation.Add(map);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

    }

    public enum ParseMode
    {
        Seeds,
        SeedToSoil,
        SoilToFertilizer,
        FertilizerToWater,
        WaterToLight,
        LightToTemperature,
        TemperatureToHumidity,
        HumidityToLocation
    }

    public class ToLocationMaps
    {
        public Dictionary<long, long> SeedToLocationMap { get; set; } = new();
        public Dictionary<long, long> SoilToLocationMap { get; set; } = new();
        public Dictionary<long, long> FertilizerToLocationMap { get; set; } = new();
        public Dictionary<long, long> WaterToLocationMap { get; set; } = new();
        public Dictionary<long, long> LightToLocationMap { get; set; } = new();
        public Dictionary<long, long> TemperatureToLocationMap { get; set; } = new();
        public Dictionary<long, long> HumidityToLocationMap { get; set; } = new();
    }

    public class Seed
    {
        public long Start { get; set; }
        public long End { get; set; }

        public Seed(long start, long length)
        {
            this.Start = start;
            this.End = start + length;
        }

    }

    public class SourceToTargetMap
    {
        public long FromSource { get; set; }
        public long ToSource { get; set; }

        public long FromTarget { get; set; }

        public SourceToTargetMap(long source, long target, long range)
        {
            this.FromSource = source;
            this.ToSource = source + range;
            this.FromTarget = target;
        }

        public bool IsSourceInRange(long number)
        {
            return number >= FromSource && number <= ToSource;
        }

        public bool IsOverlapping(Seed seed)
        {
            return IsSourceInRange(seed.Start) || IsSourceInRange(seed.End);
        }

        public long GetTarget(long number)
        {
            if (!IsSourceInRange(number))
            {
                throw new Exception($"Number {number} is not in range {FromSource} - {ToSource}");
            }

            return number - FromSource + FromTarget;
        }
        
        public long GetTarget(Seed seed)
        {
            if (!this.IsOverlapping(seed))
            {
                throw new Exception($"Seed {seed.Start} - {seed.End} is not in range {FromSource} - {ToSource}");
            }

            return  FromSource + FromTarget;
        }

    }

    public static class SourceToTargetMapListExtension
    {
        public static long GetValue(this List<SourceToTargetMap> list, long number)
        {
            var map = list.FirstOrDefault(m => m.IsSourceInRange(number));
            if (map != null)
            {
                return map.GetTarget(number);
            }

            return number;
        }
        
        public static SourceToTargetMap[] GetMaps(this List<SourceToTargetMap> list, Seed seed)
        {
            return list.Where(m => m.IsOverlapping(seed)).ToArray();
        }
    }

    public class Range
    {
        public long Start { get; set; }
        public long End { get; set; }
        public long Length => End - Start;
    }

}
