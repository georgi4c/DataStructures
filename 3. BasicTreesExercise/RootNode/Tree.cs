using System;
using System.Collections.Generic;

namespace RootNode
{
    public class Tree<T>
    {
        public T Value { get; set; }
        public Tree<T> Parent { get; set; }
        public List<Tree<T>> Children { get; private set; }
        public int Depth { get; set; }

        public Tree(T value, params Tree<T>[] children)
        {
            this.Value = value;
            this.Children = new List<Tree<T>>();
            foreach (var child in children)
            {
                this.Children.Add(child);
                this.Parent = this;
            }
        }

        public void PrintTree(int indent = 0)
        {
            Console.WriteLine(new string(' ', indent) + this.Value);
            foreach (var child in this.Children)
            {
                child.PrintTree(indent + 2);
            }
        }
        
    }
}