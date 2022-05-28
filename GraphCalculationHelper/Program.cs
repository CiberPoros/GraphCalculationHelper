using System;
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

            foreach (var str in input)
            {
                if (string.IsNullOrEmpty(str))
                {
                    continue;
                }

                graphsCount += long.Parse(new string(str.Split('=')[2].Skip(1).TakeWhile(x => x != ';').ToArray()));
                savingGroupSizeCount += long.Parse(new string(str.Split('=')[3].Skip(1).TakeWhile(x => x != ';').ToArray()));
            }

            var averageSavingGroupSizeCount = (savingGroupSizeCount + .0) / (graphsCount - 1);

            Console.WriteLine($"Total graphs saving group size: {savingGroupSizeCount}");
            Console.WriteLine($"Average graphs saving group size: {averageSavingGroupSizeCount}");
        }
    }
}
