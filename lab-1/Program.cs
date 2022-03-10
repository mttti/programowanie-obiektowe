using System;

namespace lab1
{
    class Program
    {
        static void Main(string[] args)
        {

            Money money = Money.Of(12, Currency.PLN) ?? Money.Of(0, Currency.PLN);
            Money money1 = Money.Of(12, Currency.PLN) ?? Money.Of(0, Currency.PLN);
            Money sum = money + money1;
            Console.WriteLine(sum.Value);
            Money result = 0.1m * money;
            Console.WriteLine(result.Value);
            Console.WriteLine($"{money.Value} {money.Currency}");

            if (money > result)
                Console.WriteLine("Money jest wieksze");
            else
                Console.WriteLine("Money jest mniejsze");

            if (money.Equals(Money.Of(12, Currency.PLN)))
                Console.WriteLine("Wartości są równe");
            else
                Console.WriteLine("Wartości są różne");

            decimal price = money;
            Console.WriteLine(price);
            Console.WriteLine(money);
            Console.WriteLine(money.Equals(Money.Of(12, Currency.PLN)));
            Person maciek = new Person();
            maciek.FirstName = "maciek";
            Console.WriteLine(maciek);

            Console.WriteLine(money.ToString());

            IEquatable<Money> ie = money;
            Console.WriteLine("MONEY");
            Money[] prices =
            {
                Money.Of(5, Currency.PLN),
                Money.Of(54, Currency.EUR),
                Money.Of(13, Currency.USD),
                Money.Of(31, Currency.PLN),
                Money.Of(155, Currency.PLN)
            };

            Array.Sort(prices);
            foreach (var item in prices)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("--Nowa beczka--");
            Tank beczka = new Tank(5, 10);
            Console.WriteLine(beczka.Level);
            Console.WriteLine("Dolanie paliwa do beczki");
            beczka.refuel(3);
            Console.WriteLine(beczka.Level);
            Console.WriteLine("Odlanie paliwa z beczki");
            beczka.consume(5);
            Console.WriteLine(beczka.Level);
            Console.WriteLine("Cysterna");
            Tank cysterna = new Tank(40, 50);
            Console.WriteLine(cysterna.Level);
            Console.WriteLine("Odlanie paliwa z cysterny do beczki");
            beczka.refuel(cysterna, 7);
            Console.WriteLine($"Cysterna: {cysterna.Level}");
            Console.WriteLine($"Beczka: {beczka.Level}");

            Console.WriteLine("STUDENCI POSORTOWANI WG NAZWISK, IMION, ŚREDNIEJ");
            Student[] uczniowie =
            {
                Student.Of("Damian", "Kowalski", 2.00M),
                Student.Of("Maciej", "Kowalski", 4.00M),
                Student.Of("Łukasz", "Kowalski", 5.00M),
                Student.Of("Łukasz", "Kowalski", 3.00M),
                Student.Of("Andrzej", "Nowak", 3.50M),
                Student.Of("Krzysztof", "Nowak", 5.50M),
                Student.Of("Zenon", "Nowak", 2.0M),
                Student.Of("Zenon", "Nowak", 6.0M),
                Student.Of("Krzysztof", "Lewandowski", 3.00M),
                Student.Of("Marian", "Lewandowski", 5.00M),
                Student.Of("Eustachy", "Lewandowski", 4.00M),
                Student.Of("Eustachy", "Lewandowski", 6.00M),
                /*Student.Of("Adam", "Małysz", 2.00M),
                Student.Of("Robert", "Żmijewski", 3.50M),
                Student.Of("Martyna", "Baron", 6.00M),
                Student.Of("Amelia", "Józefczyk", 2.50M)*/
            };


            Array.Sort(uczniowie);
            foreach (var item in uczniowie)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }

    public class Person
    {
        private string firstName;
        public static Person OfName(string name)
        {
            if (name != null && name.Length >= 2)
                return new Person(name);

            else
                throw new ArgumentOutOfRangeException("Imię jest zbyt krótkie.");
        }
        private Person(string _firstName)
        {
            firstName = _firstName;
        }

        public Person()
        {
        }

        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                if (value != null && value.Length >= 2)
                    firstName = value;
                else
                    throw new ArgumentOutOfRangeException("Imię jest zbyt krótkie.");
            }
        }

        public override string ToString()
        {
            return $"First name: {firstName}";
        }
    }
    public enum Currency
    {
        PLN = 1,
        USD = 2,
        EUR = 3
    }
    public class Money : IEquatable<Money>, IComparable<Money>
    {
        public static implicit operator decimal(Money money)
        {
            return money.Value;
        }
        public static explicit operator double(Money money)
        {
            return (double)money.Value;
        }

        private readonly decimal _value;
        private readonly Currency _currency;
        private Money(decimal value, Currency currency)
        {
            _value = value;
            _currency = currency;
        }

        public decimal Value
        {
            get { return _value; }
        }


        // Money * 4 --> *(money, 4)
        public static Money operator *(Money money, decimal factor)
        {
            return Money.Of(money._value * factor, money.Currency);
        }

        public static Money operator *(decimal factor, Money money)
        {
            return Money.Of(money._value * factor, money.Currency);
        }

        public static Money operator +(Money a, Money b)
        {
            IsSameCurrency(a, b);
            return Money.Of(a._value + b._value, a.Currency);
        }
        public static bool operator >(Money a, Money b)
        {
            IsSameCurrency(a, b);
            return a.Value > b.Value;
        }

        private static void IsSameCurrency(Money a, Money b)
        {
            if (a.Currency != b.Currency)
                throw new ArgumentException("Różne waluty");
        }

        public static bool operator <(Money a, Money b)
        {
            IsSameCurrency(a, b);
            return a.Value < b.Value;
        }

        public Currency Currency
        {
            get
            {
                return _currency;
            }
        }

        public static Money? Of(decimal value, Currency currency)
        {
            return value < 0 ? null : new Money(value, currency);
        }

        public static Money? OfWithException(decimal value, Currency currency)
        {
            return value < 0 ? throw new ArgumentOutOfRangeException("Wartość musi być większa niż 0.") : new Money(value, currency);
        }

        public override string ToString()
        {
            return $"Value: {_value} Currency: {_currency}";
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Money);
        }

        public bool Equals(Money other)
        {
            return other != null &&
                   _value == other._value &&
                   _currency == other._currency;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_value, _currency);
        }

        public int CompareTo(Money other)
        {
            int curResult = _currency.CompareTo(other._currency);
            if (curResult == 0)
                return _value.CompareTo(other._value);
            else
                return curResult;
        }
    }
    public class Tank
    {
        public readonly int Capacity;
        private int _level;


        public int Level
        {
            get
            {
                return _level;
            }
            private set
            {
                if (value < 0 || value > Capacity)
                {
                    throw new ArgumentOutOfRangeException("Wpisz poprawną wartość");
                }
                _level = value;
            }
        }

        public bool refuel(int amount)
        {
            if (amount < 0)
            {
                return false;
            }
            if (_level + amount > Capacity)
            {
                return false;
            }
            _level += amount;
            return true;
        }

        public bool consume(int amount)
        {
            if (amount < 0)
                return false;
            else if (amount > _level)
                return false;
            else
            {
                _level = _level - amount;
                return true;
            }
        }

        public bool refuel(Tank sourceTank, int amount)
        {
            if (amount < 0)
                return false;
            else if (amount > sourceTank._level)
                return false;
            else if (amount + _level > Capacity)
                return false;
            else
            {
                sourceTank._level -= amount;
                _level += amount;
                return true;
            }
        }

        public Tank(int _level, int capacity)
        {
            Capacity = capacity;
            Level = _level;
        }
    }

    class Student : IComparable<Student>
    {
        private string _nazwisko;
        public string Nazwisko
        {
            get => _nazwisko; 
            set
            {
                _nazwisko = value;
            }
        }
        private string _imie;
        public string Imie {
            get => _imie;
            set
            {
                _imie = value;
            } 
        }
        private decimal _srednia;
        public decimal Srednia {
            get => _srednia;
            set
            {
                _srednia = value;
            } 
        }

        public Student(string _imie, string _nazwisko, decimal _srednia)
        {
            Imie = _imie;
            Nazwisko = _nazwisko;
            Srednia = _srednia;
        }

        public int CompareTo(Student other)
        {
            int nazwiskoRes = _nazwisko.CompareTo(other._nazwisko);
            if (nazwiskoRes == 0)
            {
                int imieRes = _imie.CompareTo(other._imie);
                if (imieRes == 0)
                    return -_srednia.CompareTo(other._srednia);
                else
                    return imieRes;
            }
            else
                return nazwiskoRes;
        }

        public static Student? Of(string _imie, string _nazwisko, decimal _srednia)
        {
            return new Student(_imie, _nazwisko, _srednia);
        }
        public override string ToString()
        {
            return $"Imie: {_imie}, Nazwisko: {_nazwisko}, Średnia: {_srednia}";
        }
    }

}