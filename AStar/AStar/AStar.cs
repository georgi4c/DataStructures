using System;
using System.Collections.Generic;

public class AStar
{
    public Dictionary<Node, Node> Parent { get; private set; }
    public Dictionary<Node, int> Cost { get; private set; }
    public PriorityQueue<Node> Open { get; private set; }
    private char[,] Map { get; set; }

    public AStar(char[,] map)
    {
        this.Open = new PriorityQueue<Node>();
        this.Parent = new Dictionary<Node, Node>();
        this.Cost = new Dictionary<Node, int>();
        this.Map = map;
    }
    

    public static int GetH(Node current, Node goal)
    {
        var deltaX = Math.Abs(current.Col - goal.Col);
        var deltaY = Math.Abs(current.Row - goal.Row);

        return deltaX + deltaY;
    }

    public IEnumerable<Node> GetPath(Node start, Node goal)
    {
        this.Open.Enqueue(start);
        this.Parent[start] = null;
        this.Cost[start] = 0;
        while (this.Open.Count != 0)
        {
            var current = this.Open.Dequeue();
            if (current.Equals(goal))
            {
                break;
            }
            ProcessingNeighborNode(current, goal, current.Row + 1, current.Col);
            ProcessingNeighborNode(current, goal, current.Row - 1, current.Col);
            ProcessingNeighborNode(current, goal, current.Row, current.Col + 1);
            ProcessingNeighborNode(current, goal, current.Row, current.Col - 1);
        }

        var result = new List<Node>();
        if (!this.Parent.ContainsKey(goal))
        {
            result.Add(start);
            return result;
        }
        var currentNode = goal;
        while (currentNode != null)
        {
            result.Add(currentNode);
            currentNode = this.Parent[currentNode];
        }
        result.Reverse();
        return result;
    }

    private void ProcessingNeighborNode(Node parent,Node goal , int nRow, int nCol)
    {
        var newCost = this.Cost[parent] + 1;

        if (nRow < 0 || nRow >= this.Map.GetLength(0) ||
            nCol < 0 || nCol >= this.Map.GetLength(1) ||
            this.Map[nRow, nCol] == 'W' ||
            this.Map[nRow, nCol] == 'P')
        {
            return;
        }

        var current = new Node(nRow, nCol);
        if (!this.Cost.ContainsKey(current) || newCost < this.Cost[current])
        {
            this.Cost[current] = newCost;
            current.F = newCost + GetH(current, goal);
            this.Open.Enqueue(current);
            this.Parent[current] = parent;
        }


    }
}

