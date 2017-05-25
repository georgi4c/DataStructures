using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumAndAverage
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = Console.ReadLine()?
                .Split(new[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
            if (arr == null || arr.Count == 0)
            {
                Console.WriteLine($"Sum=0; Average=0.00");
            }
            else
            {
                Console.WriteLine($"Sum={arr.Sum()}; Average={arr.Average():F2}");
            }
        }
    }
}
