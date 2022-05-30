using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GraphCalculationHelper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter numbers count:");
            var n = int.Parse(Console.ReadLine());
            Console.WriteLine();

            var fileName = $"Result_{n}.txt";

            var input = File.ReadAllLines(fileName);
            var graphsCount = 0L;
            var savingGroupSizeCount = 0L;
            var graphsCountByGroupSize = new Dictionary<long, long>();
            var groupSizeToCount = new Dictionary<long, (long graphsCount, long OrientationsTotalCount)>();

            foreach (var str in input)
            {
                if (string.IsNullOrEmpty(str))
                {
                    continue;
                }

                var groupSize = long.Parse(new string(str.Split('=')[1].Skip(1).TakeWhile(x => x != ';').ToArray()));
                var directGraphsCount = long.Parse(new string(str.Split('=')[2].Skip(1).TakeWhile(x => x != ';').ToArray()));
                if (graphsCountByGroupSize.ContainsKey(groupSize))
                {
                    graphsCountByGroupSize[groupSize]++;
                }
                else
                {
                    graphsCountByGroupSize.Add(groupSize, 1);
                }

                if (groupSizeToCount.TryGetValue(groupSize, out var value))
                {
                    groupSizeToCount[groupSize] = (value.graphsCount + 1, value.OrientationsTotalCount + directGraphsCount);
                }
                else
                {
                    groupSizeToCount.Add(groupSize, (1, directGraphsCount));
                }

                graphsCount += long.Parse(new string(str.Split('=')[2].Skip(1).TakeWhile(x => x != ';').ToArray()));
                savingGroupSizeCount += long.Parse(new string(str.Split('=')[3].Skip(1).TakeWhile(x => x != ';').ToArray()));
            }

            var averageSavingGroupSizeCount = (savingGroupSizeCount + .0) / (graphsCount - 1);

            Console.WriteLine($"Total graphs saving group size: {savingGroupSizeCount}");
            Console.WriteLine($"Average graphs saving group size: {averageSavingGroupSizeCount}");


            var outpur = new List<string>();
            foreach (var kvp in graphsCountByGroupSize.OrderBy(x => x.Key))
            {
                outpur.Add(kvp.Key.ToString());
            }
            outpur.Add("Разделитель-------------------------------");
            foreach (var kvp in graphsCountByGroupSize.OrderBy(x => x.Key))
            {
                outpur.Add(kvp.Value.ToString());
            }
            File.WriteAllLines($"NeorGraphsCountByGroupSize_{n}.txt", outpur);

            outpur = new List<string>();
            foreach (var kvp in groupSizeToCount.OrderBy(x => x.Key))
            {
                outpur.Add(kvp.Key.ToString());
            }
            outpur.Add("Разделитель-------------------------------");
            foreach (var kvp in groupSizeToCount.OrderBy(x => x.Key))
            {
                outpur.Add(kvp.Value.OrientationsTotalCount.ToString());
            }
            File.WriteAllLines($"OrGraphsCountByGroupSize_{n}.txt", outpur);

            outpur = new List<string>();
            foreach (var kvp in groupSizeToCount.OrderBy(x => x.Key))
            {
                outpur.Add(kvp.Key.ToString());
            }
            outpur.Add("Разделитель-------------------------------");
            foreach (var kvp in groupSizeToCount.OrderBy(x => x.Key))
            {
                outpur.Add(((kvp.Value.OrientationsTotalCount + .0) / kvp.Value.graphsCount).ToString("#.##"));
            }
            File.WriteAllLines($"AverageOrGraphsCountByGroupSize_{n}.txt", outpur);
        }
    }
}
