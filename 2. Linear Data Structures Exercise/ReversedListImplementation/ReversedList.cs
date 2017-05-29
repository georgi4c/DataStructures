using System;
using System.Collections;
using System.Collections.Generic;

    public class ReversedList<T> : IEnumerable<T>
    {
        private const int InitialCount = 2;
        private T[] items;

        public ReversedList()
        {
            this.items = new T[2];
            this.Capacity = items.Length;
            this.Count = 0;
        }

        public int Count;
        public int Capacity;

        public T this[int index]
        {
            get
            {
                if (index >= this.Count)
                {
                    throw new ArgumentOutOfRangeException();
                }

                return this.items[index + this.items.Length - this.Count];
            }

            set
            {
                if (index >= this.Count)
                {
                    throw new ArgumentOutOfRangeException();
                }

                this.items[index + this.items.Length - this.Count] = value;
            }
        }

        public void Add(T item)
        {
            if (this.Count == items.Length)
            {
                this.Resize();
            }
            this.items[this.items.Length - this.Count - 1] = item;
            this.Count++;

        }

        private void Resize()
        {
            var itemsLength = this.items.Length;
            var newList = new T[itemsLength * 2];
            
            for (int i = this.items.Length - 1; i >= 0; i--)
            {
                newList[i + itemsLength] = items[i];
            }
            this.items = newList;
            this.Capacity = this.items.Length;
        }

        public T RemoveAt(int index)
        {
            if (index >= this.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            T removed = this[index];
            this.Shift(index);
            this.Count--;
            return removed;

        }

        private void Shift(int index)
        {
            var diff = items.Length - Count;
            for (int i = diff + index; i > diff; i--)
            {
                this.items[i] = this.items[i - 1];
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
