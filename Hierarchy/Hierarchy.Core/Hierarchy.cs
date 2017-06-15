using System.Linq;


    using System;
    using System.Collections.Generic;
    using System.Collections;
using Hierarchy.Core;

public class Hierarchy<T> : IHierarchy<T> where T : IComparable
    {
        private Node root;
        private Dictionary<T, Node> nodesDic;

        public Hierarchy(T root)
        {
            this.root = new Node(root){Count = 1};
            nodesDic = new Dictionary<T, Node> {[root] = this.root};
        }

        
        public int Count
        {
            get { return this.root.Count; }
        }

        public void Add(T element, T child)
        {
            if (!nodesDic.ContainsKey(element))
            {
                throw new ArgumentException("Element does not exist!");
            }
            if (nodesDic.ContainsKey(child))
            {
                throw new ArgumentException("Child already exist!");
            }
            Node elementNode = nodesDic[element];
            Node childNode = new Node(child);
            childNode.Parent = elementNode;
            elementNode.Children.Add(childNode);
            nodesDic[child] = childNode;
            this.root.Count++;
        }

        public void Remove(T element)
        {
            if (this.root.Value.Equals(element))
            {
                throw new InvalidOperationException("Can not remove the root");
            }
            if (!nodesDic.ContainsKey(element))
            {
                throw new ArgumentException("Element does not exist!");
            }
            var elementNode = nodesDic[element];
            var parentNode = nodesDic[elementNode.Parent.Value];
            foreach (var child in elementNode.Children)
            {
                nodesDic[child.Value].Parent = parentNode;
                child.Parent = parentNode;
                parentNode.Children.Add(child);
            }
            parentNode.Children.Remove(elementNode);
            nodesDic.Remove(element);
            this.root.Count--;
        }

        public IEnumerable<T> GetChildren(T item)
        {
            if (!this.Contains(item))
            {
                throw new ArgumentException("Element does not exist!");
            }
            return nodesDic[item].Children.Select(x => x.Value);
        }

        public T GetParent(T item)
        {
            if (!this.Contains(item))
            {
                throw new ArgumentException("Element does not exist!");
            }
            Node parentNode = nodesDic[item].Parent;
            if (parentNode == null)
            {
                return default(T);
            }
            return parentNode.Value;
        }

        public bool Contains(T value)
        {
            return this.nodesDic.ContainsKey(value);
        }

        public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
        {
            var thisHierarchyElements = this.nodesDic.Keys;
            var otherHierarchyElements = other.nodesDic.Keys;
            var result = thisHierarchyElements.Intersect(otherHierarchyElements);
            return result;
        } 

        public IEnumerator<T> GetEnumerator()
        {
            var currentNode = this.root;
            var queue = new Queue<Node>();
            if (currentNode==null)
            {
                yield break;
            }
            while (true)
            {
                yield return currentNode.Value;
                foreach (var child in currentNode.Children)
                {
                    queue.Enqueue(child);
                }
                if (queue.Count == 0)
                {
                    yield break;
                }
                currentNode = queue.Dequeue();
            }
        }
        

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private class Node
        {
            public Node(T value)
            {
                this.Value = value;
                this.Children = new List<Node>();
            }

            public T Value { get; }
            public Node Parent { get; set; }
            public List<Node> Children { get; set; }


            public int Count { get; set; }
        }
    }
