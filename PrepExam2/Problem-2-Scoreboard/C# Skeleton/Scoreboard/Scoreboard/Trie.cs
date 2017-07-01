using System;
using System.Collections.Generic;

public class Trie<Value>
{
    private Node root;

    private class Node
    {
        public Value Val;
        public bool isTerminal;
        public Dictionary<char, Node> Next = new Dictionary<char, Node>();
    }

    public Value GetValue(string key)
    {
        Node x = GetNode(root, key, 0);
        if (x == null || !x.isTerminal)
        {
            throw new InvalidOperationException();
        }

        return x.Val;
    }

    public bool Contains(string key)
    {
        Node node = GetNode(this.root, key, 0);
        return node != null && node.isTerminal;
    }
    
    public IEnumerable<string> GetByPrefix(string prefix)
    {
        var results = new Queue<string>();
        var x = GetNode(root, prefix, 0);

        this.Collect(x, prefix, results);
        
        return results;
    }

    private Node GetNode(Node x, string key, int d)
    {
        if (x == null)
        {
            return null;
        }

        if (d == key.Length)
        {
            return x;
        }

        Node node = null;
        char c = key[d];

        if (x.Next.ContainsKey(c))
        {
            node = x.Next[c];
        }

        return GetNode(node, key, d + 1);
    }

    public void Insert(string key, Value val)
    {
        root = Insert(root, key, val, 0);
    }

    private Node Insert(Node x, string key, Value val, int d)
    {
        if (x == null)
        {
            x = new Node();
        }

        if (key.Length == d)
        {
            x.isTerminal = true;
            x.Val = val;
            return x;
        }

        Node node = null;
        char c = key[d];
        if (x.Next.ContainsKey(c))
        {
            node = x.Next[c];
        }

        x.Next[c] = this.Insert(node, key, val, d + 1);

        return x;
    }

    private void Collect(Node x, string prefix, Queue<string> results)
    {
        if (x == null)
        {
            return;
        }

        if (x.Val != null && x.isTerminal)
        {
            results.Enqueue(prefix);
        }

        foreach (var c in x.Next.Keys)
        {
            Collect(x.Next[c], prefix + c, results);
        }
    }
}