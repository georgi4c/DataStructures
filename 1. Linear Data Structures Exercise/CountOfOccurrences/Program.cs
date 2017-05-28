using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountOfOccurrences
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = Console.ReadLine()?
                .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
            var occurrences = new SortedDictionary<int, int>();

            foreach (var item in list)
            {
                if (!occurrences.ContainsKey(item))
                {
                    occurrences[item] = 0;
                }
                occurrences[item]++;
            }
            foreach (var occurrence in occurrences)
            {
                Console.WriteLine($"{occurrence.Key} -> {occurrence.Value} times");
            }
        }
    }
}
