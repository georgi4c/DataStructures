using System;
using System.Collections.Generic;

namespace ReverseNumbersWithAStack
{
    class Program
    {
        static void Main(string[] args)
        {
            var nums = Console.ReadLine()
                .Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);
            Stack<int> stack = new Stack<int>();
            foreach (var num in nums)
            {
                stack.Push(int.Parse(num));
            }
            while (stack.Count > 0)
            {
                Console.Write(stack.Pop() + " ");
            }
        }
    }
}
