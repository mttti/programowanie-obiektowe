using System;

namespace lab_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Product[] shop = new Product[4];
            shop[0] = new Computer() { Price = 2000m, Vat = 23m };
            shop[1] = new Paint() { PriceUnit = 12, Capacity = 5, Vat = 8m };
            shop[2] = new Computer() { Price = 1000m, Vat = 23m };
            shop[3] = new Butter() { Price = 12m };

            decimal sumVat = 0m;
            decimal sumPrice = 0;
            foreach (var item in shop)
            {
                sumVat += item.GetVatPrice();
                sumPrice += item.Price;
                if (item is Computer)
                {
                    Computer comp = (Computer)item;
                }

                Computer computer = item as Computer;
                if (computer != null)
                    Console.WriteLine(computer.Price);
                Console.WriteLine(computer?.Vat);

            }

            Console.WriteLine(sumVat);
            Console.WriteLine(sumPrice);
        }
    }

    class Computer : Product
    {
        public decimal Vat { get; init; }
        public override decimal GetVatPrice()
        {
            return Price * Vat / 100.0m;
        }
    }

    class Paint : Product
    {
        public decimal Vat { get; init; }
        public decimal Capacity { get; init; }

        public decimal PriceUnit { get; init; }
        public override decimal GetVatPrice()
        {
            return PriceUnit * Capacity * Vat / 100.0m;
        }

        public override decimal Price
        {
            get
            {
                return PriceUnit * Capacity;
            }
        }
    }

    class Butter : Product
    {
        public override decimal GetVatPrice()
        {
            return 2m;
        }
    }
    abstract class Product
    {
        public virtual decimal Price { get; init; }
        public abstract decimal GetVatPrice();
    }

}

