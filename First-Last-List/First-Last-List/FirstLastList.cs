using System;
using System.Collections.Generic;
using System.Linq;

using Wintellect.PowerCollections;

public class FirstLastList<T> : IFirstLastList<T> where T : IComparable<T>
{
    private LinkedList<T> byInsertion;
    private OrderedBag<LinkedListNode<T>> byOrder;
    private OrderedBag<LinkedListNode<T>> byOrderReversed;

    public FirstLastList()
    {
        this.byInsertion = new LinkedList<T>();
        this.byOrder = new OrderedBag<LinkedListNode<T>>((x, y) => x.Value.CompareTo(y.Value));
        this.byOrderReversed = new OrderedBag<LinkedListNode<T>>((x,y) => -x.Value.CompareTo(y.Value));
    }
    public int Count
    {
        get { return this.byInsertion.Count; }
    }

    public void Add(T element)
    {
        var node = new LinkedListNode<T>(element);
        this.byInsertion.AddLast(node);
        this.byOrder.Add(node);
        this.byOrderReversed.Add(node);
    }

    public void Clear()
    {
        this.byInsertion.Clear();
        this.byOrder.Clear();
        this.byOrderReversed.Clear();
    }

    public IEnumerable<T> First(int count)
    {
        RangeCheck(count);
        var current = byInsertion.First;
        while (count > 0)
        {
            yield return current.Value;
            current = current.Next;
            count--;
        }

    }

    public IEnumerable<T> Last(int count)
    {
        RangeCheck(count);
        var current = byInsertion.Last;
        while (count > 0)
        {
            yield return current.Value;
            current = current.Previous;
            count--;
        }
    }

    public IEnumerable<T> Max(int count)
    {
        RangeCheck(count);
        foreach (var item in byOrderReversed)
        {
            if (count <= 0)
            {
                break;
            }

            yield return item.Value;
            count--;
        }


    }

    public IEnumerable<T> Min(int count)
    {
        RangeCheck(count);
        foreach (var item in byOrder)
        {
            if (count <= 0)
            {
                break;
            }

            yield return item.Value;
            count--;
        }
    }

    public int RemoveAll(T element)
    {
        var node = new LinkedListNode<T>(element);
        var range = byOrder.Range(node, true, node, true);

        foreach (var item in range)
        {
            byInsertion.Remove(item);
        }

        var count = byOrder.RemoveAllCopies(node);
        byOrderReversed.RemoveAllCopies(node);
        return count;
    }

    private void RangeCheck(int count)
    {
        if (count > this.Count)
        {
            throw new ArgumentOutOfRangeException();
        }
    }
}
