using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongestSubsequence
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = Console.ReadLine()?
                .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
            int maxNum=list[0];
            int maxCount = 1;
            int currentCount = 1;
            
            for (int i = 1; i < list.Count; i++)
            {
                
                if (list[i] == list[i-1])
                {
                    currentCount++;
                    
                }
                else
                {
                currentCount = 1;

                }
                if (currentCount > maxCount)
                {
                    maxNum = list[i];
                    maxCount = currentCount;
                }
            }
            var result = new List<int>(maxCount);
            for (int i = 0; i < maxCount; i++)
            {
                result.Add(maxNum);
            }
            Console.WriteLine(string.Join(" ", result));
        }
    }
}
