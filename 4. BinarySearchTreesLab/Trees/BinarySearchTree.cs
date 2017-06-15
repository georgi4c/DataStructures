using System;
using System.Collections.Generic;

public class BinarySearchTree<T> where T : IComparable<T>
{
    public BinarySearchTree()
    {

    }

    private BinarySearchTree(Node root)
    {
        this.Copy(root);
    }

    private Node root;

    private class Node
    {
        public Node(T value)
        {
            this.Value = value;
            this.Count = 1;
        }

        public T Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public int Count { get; set; }
    }

    public void Delete(T item)
    {
        this.root = Delete(this.root, item);
    }

    private Node Delete(Node node, T item)
    {
        if (node == null)
        {
            return null;
        }

        int cmp = item.CompareTo(node.Value);
        if (cmp < 0)
        {
            node.Left = Delete(node.Left, item);
        }
        else if (cmp > 0)
        {
            node.Right = Delete(node.Right, item);
        }
        else
        {
            if (node.Right == null)
            {
                return node.Left;
            }
            else if (node.Left == null)
            {
                return node.Right;
            }
            else
            {
                var temp = FindMin(node.Right);
                temp.Right = DeleteMin(node.Right);
                temp.Left = node.Left;
                node = temp;
            }
        }
        node.Count = Count(node.Left) + Count(node.Right) + 1;
        return node;
    }


    private Node FindMin(Node node)
    {
        if (node == null)
        {
            return null;
        }
        if (node.Left == null)
        {
            return node;
        }

        return FindMin(node.Left);
    }

    public T Floor(T element)
    {
        return this.Select(this.Rank(element) - 1);

    }

    //private Node Floor(Node node, T element)
    //{
    //    if (node == null)
    //    {
    //        return 
    //    }
    //    int itemComparedToEl = node.Value.CompareTo(element);
    //    if (itemComparedToEl > 0)
    //    {
    //        return this.Floor(node.Left, element);
    //    }

    //    T floor = this.Floor(node.Right, element);

    //    if (floor == )
    //    {

    //    }
    //}

    public T Ceiling(T element)
    {
        return this.Select(this.Rank(element) + 1);
    }

    //private T Ceiling(Node node, T element)
    //{
    //    if (node == null)
    //    {
    //        return default(T);
    //    }
    //    if (node.Value.CompareTo(element) == 0)
    //    {
    //        return node.Value;
    //    }
    //    else if(node.Value.CompareTo(element) < 0)
    //    {
    //        return this.Ceiling(node.Right, element);
    //    }
    //    else
    //    {

    //    }

    //}

    public T Select(int rank)
    {
        Node node = this.Select(rank, this.root);
        if (node == null)
        {
            throw new InvalidOperationException();
        }

        return node.Value;
    }

    private Node Select(int rank, Node node)
    {
        if (node == null)
        {
            return null;
        }

        int leftCount = this.Count(node.Left);
        if (leftCount == rank)
        {
            return node;
        }

        if (leftCount > rank)
        {
            return this.Select(rank, node.Left);
        }
        else
        {
            return this.Select(rank - (leftCount + 1), node.Right);
        }

    }

    public int Rank(T element)
    {
        return this.Rank(element, this.root);
    }

    private int Rank(T element, Node node)
    {
        if (node == null)
        {
            return 0;
        }

        int compare = element.CompareTo(node.Value);
        if (compare < 0)
        {
            return this.Rank(element, node.Left);
        }

        if (compare > 0)
        {
            return 1 + this.Count(node.Left) + this.Rank(element, node.Right);
        }
        return this.Count(node.Left);
    }

    public int Count()
    {
        return this.Count(this.root);
    }

    private int Count(Node node)
    {
        if (node == null)
        {
            return 0;
        }

        return node.Count;
    }

    private void Copy(Node node)
    {
        if (node == null)
        {
            return;
        }

        this.Insert(node.Value);
        this.Copy(node.Left);
        this.Copy(node.Right);
    }

    public void Insert(T value)
    {
        if (this.root == null)
        {
            this.root = new Node(value);
            return;
        }
        if (this.Contains(value))
        {
            return;
        }

        Node parent = null;
        Node current = this.root;
        while (current != null)
        {
            current.Count++;
            if (value.CompareTo(current.Value) < 0)
            {
                parent = current;
                current = current.Left;

            }
            else if (value.CompareTo(current.Value) > 0)
            {

                parent = current;
                current = current.Right;
            }
            else
            {
                return;
            }
        }
        Node newNode = new Node(value);
        if (value.CompareTo(parent.Value) < 0)
        {
            parent.Left = newNode;
        }
        else
        {
            parent.Right = newNode;
        }

    }

    public bool Contains(T value)
    {
        Node current = this.root;
        while (current != null)
        {
            if (value.CompareTo(current.Value) < 0)
            {
                current = current.Left;
            }
            else if (value.CompareTo(current.Value) > 0)
            {
                current = current.Right;
            }
            else
            {
                break;
            }
        }

        return current != null;
    }

    public void DeleteMin()
    {
        this.root = DeleteMin(this.root);
        //if (this.root == null)
        //{
        //    throw new InvalidOperationException();
        //}
        //Node parent = null;
        //Node min = this.root;
        //while (min.Left != null)
        //{
        //    min.Count--;
        //    parent = min;
        //    min = min.Left;
        //}
        //if (parent == null)
        //{
        //    this.root = min.Right;
        //}
        //else
        //{
        //    parent.Left = min.Right;
        //}
    }

    private Node DeleteMin(Node node)
    {
        if (node == null)
        {
            return null;
        }
        if (node.Left == null)
        {
            return node.Right;
        }
        node.Left = DeleteMin(node.Left);
        node.Count = Count(node.Left) + Count(node.Right) + 1;
        return node;
    }

    public void DeleteMax()
    {
        if (this.root == null)
        {
            throw new InvalidOperationException();
        }
        Node parent = null;
        Node max = this.root;
        while (max.Right != null)
        {
            max.Count--;
            parent = max;
            max = max.Right;
        }
        if (parent == null)
        {
            this.root = max.Left;
        }
        else
        {
            parent.Right = max.Left;
        }
    }

    public BinarySearchTree<T> Search(T item)
    {
        Node current = this.root;
        while (current != null)
        {
            if (item.CompareTo(current.Value) < 0)
            {
                current = current.Left;
            }
            else if (item.CompareTo(current.Value) > 0)
            {
                current = current.Right;
            }
            else
            {
                break;
            }
        }
        return new BinarySearchTree<T>(current);
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        Queue<T> queue = new Queue<T>();

        this.Range(this.root, queue, startRange, endRange);

        return queue;
    }

    private void Range(Node node, Queue<T> queue, T startRange, T endRange)
    {
        if (node == null)
        {
            return;
        }
        int nodeInLowerRange = startRange.CompareTo(node.Value);
        int nodeInHigherRange = endRange.CompareTo(node.Value);
        if (nodeInLowerRange < 0)
        {
            this.Range(node.Left, queue, startRange, endRange);
        }
        if (nodeInLowerRange <= 0 && nodeInHigherRange >= 0)
        {
            queue.Enqueue(node.Value);
        }
        if (nodeInHigherRange > 0)
        {
            this.Range(node.Right, queue, startRange, endRange);
        }
    }

    public void EachInOrder(Action<T> action)
    {
        EachInOrder(this.root, action);
    }

    private void EachInOrder(Node currentNode, Action<T> action)
    {
        if (currentNode == null)
        {
            return;
        }

        EachInOrder(currentNode.Left, action);
        action(currentNode.Value);
        EachInOrder(currentNode.Right, action);
    }
}

public class Launcher
{
    public static void Main(string[] args)
    {
        var bst = new BinarySearchTree<int>();

        bst.Insert(1);
        bst.Insert(4);
        bst.Insert(45);
        bst.Insert(3);
        bst.Insert(5);
        bst.Insert(8);
        bst.Insert(9);
        bst.Insert(10);
        bst.Insert(37);
        bst.Insert(39);

        int nodeCount = bst.Count();

        Console.WriteLine(nodeCount);

        Console.WriteLine(bst.Rank(10));
    }
}
