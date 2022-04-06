using System;
using System.Collections;
using System.Collections.Generic;

namespace lab_5
{
    record Ingredient
    {
        public double Calories { get; init; }
        public string Name { get; init; }
    }

    class Sandwitch : IEnumerable<Ingredient>
    {
        public Ingredient Bread { get; init; }
        public Ingredient Butter { get; init; }
        public Ingredient Salad { get; init; }
        public Ingredient Ham { get; init; }

        public IEnumerator<Ingredient> GetEnumerator()
        {
            yield return Bread;//zwrocone w Current po pierwszym wywolaniu MoveNext
            yield return Butter;//zwrocone w Current po drugim wywolaniu MoveNext
            yield return Ham;
            yield return Salad;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    class Parking : IEnumerable<string>
    {
        private string[] _arr = { null,"FF123", null, "GL1234", null, "KK2137", null };

        public string this[char slot]
        {
            get
            {
                return _arr[slot - 'A'];
            }
            set
            {
                _arr[slot - 'A'] = value;
            }
        }



        public IEnumerator<string> GetEnumerator()
        {
            foreach (var item in _arr)
            {
                if (item != null)
                    yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    class SandwitchEnumerator : IEnumerator<Ingredient>
    {
        private Sandwitch _sandwitch;

        int counter = -1;

        public SandwitchEnumerator(Sandwitch sandwitch)
        {
            _sandwitch = sandwitch;
        }

        public Ingredient Current
        {
            get
            {
                return counter switch
                {
                    0 => _sandwitch.Bread,
                    1 => _sandwitch.Butter,
                    2 => _sandwitch.Ham,
                    3 => _sandwitch.Salad,
                    _ => null
                };
            }
        }

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            return ++counter < 4;
        }

        public void Reset()
        {

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Sandwitch sandwitch = new Sandwitch()
            {
                Bread = new Ingredient() { Calories = 100, Name = "Bułka" },
                Ham = new Ingredient() { Calories = 400, Name = "Szynka" },
                Salad = new Ingredient() { Calories = 100, Name = "Lodowa" },
                Butter = new Ingredient() { Calories = 300, Name = "Masło" }
            };

            IEnumerator<Ingredient> enumerator = sandwitch.GetEnumerator();

            /*while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }
            foreach (Ingredient item in sandwitch)
            {
                Console.WriteLine(item);
            }*/

            Parking parking = new Parking();

            foreach (var item in parking)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine(string.Join(", ", parking));
            //Console.WriteLine(string.Join(", ", sandwitch));

            Console.WriteLine(parking['D']);

            parking['A'] = "TT41221";

            Console.WriteLine(string.Join(", ", parking));


        }
    }
}
