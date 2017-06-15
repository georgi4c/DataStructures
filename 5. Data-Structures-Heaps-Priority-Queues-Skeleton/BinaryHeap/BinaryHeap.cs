using System;
using System.Collections.Generic;
using System.Linq;

public class BinaryHeap<T> where T : IComparable<T>
{
    private List<T> heap;

    public BinaryHeap()
    {
        this.heap = new List<T>();
    }

    public int Count
    {
        get { return this.heap.Count; }
    }

    public void Insert(T item)
    {
        this.heap.Add(item);
        this.HeapifyUp(this.heap.Count - 1);
    }

    private void HeapifyUp(int index)
    {
        while (index > 0 && IsLess(Parent(index), index))
        {
            this.Swap(index, Parent(index));
            index = Parent(index);
        }
    }

    private void Swap(int index, int parent)
    {
        T temp = heap[index];
        heap[index] = heap[parent];
        heap[parent] = temp;
    }

    private bool IsLess(int parent, int index)
    {
        return this.heap[parent].CompareTo(this.heap[index]) < 0;
    }

    private int Parent(int index)
    {
        return (index - 1) / 2;
    }

    public T Peek()
    {
        if (this.Count < 0)
        {
            throw new InvalidOperationException();
        }
        return this.heap[0];
    }

    public T Pull()
    {
        if (this.Count <= 0)
        {
            throw new InvalidOperationException();
        }
        T item = this.heap[0];

        this.Swap(0, this.Count - 1);
        this.heap.RemoveAt(this.Count - 1);
        this.HeapifyDown(0);

        return item;
    }

    private void HeapifyDown(int index)
    {
        while (index < this.Count / 2)
        {
            int child = 2 * index + 1;
            if (child + 1 < this.Count && IsLess(child, child + 1))
            {
                child = child + 1;
            }

            if (IsLess(child, index))
            {
                break;
            }

            this.Swap(index, child);
            index = child;
        }
    }

    public void DecreaseKey(T element)
    {
        if (!this.heap.Contains(element))
        {
            throw new InvalidOperationException();
        }

        int elementIndex = this.heap.FindIndex(x => x.CompareTo(element) == 0);
        this.HeapifyDown(elementIndex);
    }
}
