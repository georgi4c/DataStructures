using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoveOddOccurrences
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = Console.ReadLine()?
                .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
            var occurrences = new Dictionary<int,int>();

            foreach (var item in list)
            {
                if (!occurrences.ContainsKey(item))
                {
                    occurrences[item] = 0;
                }
                occurrences[item]++;
            }
            var invalidElements = occurrences
                .Where(x => x.Value % 2 != 0)
                .Select(x => x.Key);
            var result = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                if (!invalidElements.Contains(list[i]))
                {
                    result.Add(list[i]);
                }
            }
            Console.WriteLine(string.Join(" ", result));
        }
    }
}
