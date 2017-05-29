class Program
    {
        static void Main(string[] args)
        {
            var list = new ReversedList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list[0] = 5;
        }
    }

