using System;

public static class Heap<T> where T : IComparable<T>
{
    public static void Sort(T[] arr)
    {
        int n = arr.Length;
        for (int i = n / 2; i >= 0; i--)
        {
            HeapifyDown(arr, i, arr.Length);
        }

        for (int i = n - 1; i > 0; i--)
        {
            Swap(arr, 0, i);
            HeapifyDown(arr, 0, i);
        }
    }
    private static void HeapifyDown(T[] arr,int current, int border)
    {
        while (current < border / 2)
        {
            int child = 2 * current + 1;
            if (child + 1 < border && IsLess(arr, child, child + 1))
            {
                child = child + 1;
            }

            if (IsLess(arr, child, current))
            {
                break;
            }

            Swap(arr, current, child);
            current = child;
        }
    }
    private static bool IsLess(T[] arr, int parent, int index)
    {
        return arr[parent].CompareTo(arr[index]) < 0;
    }

    private static void Swap(T[] arr, int index, int parent)
    {
        T temp = arr[index];
        arr[index] = arr[parent];
        arr[parent] = temp;
    }
}
