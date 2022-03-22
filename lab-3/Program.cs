using System;

namespace lab_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack<string> stringStack = new Stack<string>();
            stringStack.Push("abc");

            Reward reward = new Reward();

            reward.GiveTo("director");

            Stack<object> objectStack = new Stack<object>();
            objectStack.Push(1);
            objectStack.Push("bb");

            ValueTuple<string, decimal, int> product = ValueTuple.Create("laptop", 2137, 4);
            Console.WriteLine(product.Item1);
            Console.WriteLine(product);

            (string, decimal, int) tuple = ("laptop", 2137, 4);
            Console.WriteLine(product==tuple);

            (string name, decimal price, int quantity) wpis = tuple;
            wpis = (name: "laptop", price: 2137, quantity: 21);
            Console.WriteLine(wpis.name);
        }
    }

    class Reward
    {
        public Reward GiveTo<T>(T target)
        {
            Console.WriteLine($"Reward goes to {target}");
            return this;
        }
    }

    class Stack<T>
    {
        private T[] _arr = new T[100];
        private int _last = -1;

        public void Push(T item)
        {
            //warunki testujące
            _arr[++_last] = item;
        }

        public T Pop()
        {
            return _arr[_last--];
        }
    }
}

