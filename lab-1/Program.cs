using System;

namespace lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            /*          Money.Of(-12, Currency.PLN) == null ? Money.Of(0, Currency.PLN) : Money.Of(-12, Currency.PLN);
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
                      Console.WriteLine(money);*/
            Money money = Money.Of(12, Currency.PLN) ?? Money.Of(0, Currency.PLN);
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
}