using System;

namespace lab_2
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Product[] shop = new Product[4];
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

            IFly[] flyingObject = new IFly[2];

            flyingObject[0] = new Duck();
            flyingObject[1] = new Hydroplane();
            ISwim[] swimmingObject = new ISwim[2];
            swimmingObject[0] = (ISwim)flyingObject[0];*/

            /* Vehicle[] vehicles =
                 {
                  new Bicycle(){Weight = 15, MaxSpeed = 30, isDriver = true},
                  new Car(){Weight = 900, MaxSpeed = 120, isFuel = true, isEngineWorking = true},
                  new Bicycle(){Weight = 21, MaxSpeed = 40, isDriver = true},
                  new Bicycle(){Weight = 19, MaxSpeed = 35, isDriver = true},
                  new Car(){Weight = 1200, MaxSpeed = 130, isFuel = true, isEngineWorking = true}
                 };*/

            /* IAggregate aggregate;
             IIterator iterator = aggregate.CreateIterator();
             while (iterator.HasNext())
             {
                 Console.WriteLine(iterator.GetNext());
             }*/

            ElectricScooter skuter = new ElectricScooter() { Weight = 100, MaxSpeed = 55, IsEngineWorking = true, MaxRange = 100 };            
            
            Console.WriteLine(skuter.BatteriesLevel);
            skuter.chargeBatteries();
            Console.WriteLine(skuter.BatteriesLevel);
            skuter.Drive(10);
            Console.WriteLine(skuter.BatteriesLevel);
            skuter.chargeBatteries();
            Console.WriteLine(skuter.BatteriesLevel);


            IFlyable[] things = new IFlyable[4];
            things[0] = new Hydroplane() {MaxSpeed=800 };
            things[1] = new Wasp();
            things[2] = new Duck();

            int n = 0;

            foreach (var item in things)
            {
                if (item is ISwimmingable && item is IFlyable)
                {
                    n++;
                }
            }
            Console.WriteLine($"Ilość obiektów które implementują jednocześnie interfejsy ISwimmingable i IFlyable: {n}");
        }
    }
    //ćwiczenie 1
    public abstract class Scooter : Vehicle
    {

    }
    public class ElectricScooter : Scooter
    {

        private int _batterieslevel = 0;

        public int BatteriesLevel { get { return _batterieslevel; } }

        public bool IsEngineWorking { get; set; }

        public int MaxRange { get; set; }

        public void chargeBatteries()
        {
            _batterieslevel = 100;
        }
        public override decimal Drive(int distance)
        {
            if (BatteriesLevel > 0 && IsEngineWorking)
            {
                _mileage += distance;
                _batterieslevel -= distance;
                return (decimal)(distance / (double)MaxSpeed);
            }
            return -1;
        }

        public override string ToString()
        {
            return $"Electric Scooter{{Weight: {Weight}, MaxSpeed: {MaxSpeed}, Mileage: {_mileage}}}"; ;
        }
    }
    public class KickScooter:Vehicle
    {
        public bool isDriver { get; set; }
        public override decimal Drive(int distance)
        {
            if (isDriver)
            {
                _mileage += distance;
                return (decimal)(distance / (double)MaxSpeed);
            }
            return -1;
        }
        public override string ToString()
        {
            return $"Kick Scooter{{Weight: {Weight}, MaxSpeed: {MaxSpeed}, Mileage: {_mileage}}}"; ;
        }
    }
    public abstract class Vehicle
    {
        public double Weight { get; init; }
        public int MaxSpeed { get; init; }
        protected int _mileage;
        public int Mealeage
        {
            get { return _mileage; }
        }
        public abstract decimal Drive(int distance);
        public override string ToString()
        {
            return $"Vehicle{{ Weight: {Weight}, MaxSpeed: {MaxSpeed}, Mileage: {_mileage} }}";
        }
    }
    public class Car : Vehicle
    {
        public bool isFuel { get; set; }
        public bool isEngineWorking { get; set; }
        public override decimal Drive(int distance)
        {
            if (isFuel && isEngineWorking)
            {
                _mileage += distance;
                return (decimal)(distance / (double)MaxSpeed);
            }
            return -1;
        }
        public override string ToString()
        {
            return $"Car{{Weight: {Weight}, MaxSpeed: {MaxSpeed}, Mileage: {_mileage}}}";
        }
    }
    public class Bicycle : Vehicle
    {
        public bool isDriver { get; set; }
        public override decimal Drive(int distance)
        {
            if (isDriver)
            {
                _mileage += distance;
                return (decimal)(distance / (double)MaxSpeed);
            }
            return -1;
        }
        public override string ToString()
        {
            return $"Bicycle{{Weight: {Weight}, MaxSpeed: {MaxSpeed}, Mileage: {_mileage}}}"; ;
        }
    }
    //klasa produkt
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
    //ćwiczenie 2
    interface IDriveable
    {
        int Drive(int distance);
    }
    interface ISwimmingable
    {
        int Swim(int distance);
    }
    interface IFlyable
    {
        bool TakeOff();
        int Fly(int disntance);
        bool Land();
    }
    class Duck : ISwimmingable, IFlyable
    {
        public int Fly(int disntance)
        {
            Console.WriteLine("FLY");
            return 0;
        }

        public bool Land()
        {
            Console.WriteLine("LAND");
            return true;
        }

        public int Swim(int distance)
        {
            Console.WriteLine("SWIM");
            return 0;
        }

        public bool TakeOff()
        {
            Console.WriteLine("TAKE OFF");
            return true;
        }

    }
    class Wasp : IFlyable
    {
        public int Fly(int disntance)
        {
            Console.WriteLine("FLY");
            return 0;
        }

        public bool Land()
        {
            Console.WriteLine("LAND");
            return true;
        }

        public bool TakeOff()
        {
            Console.WriteLine("TAKE OFF");
            return true;
        }
    }
    public class Hydroplane : Vehicle, IFlyable, ISwimmingable
    {
        public override decimal Drive(int distance)
        {
            Console.WriteLine("DRIVE");
            return 0;
        }
        public int Swim(int distance)
        {
            Console.WriteLine("SWIM");
            return 0;
        }
        public bool TakeOff()
        {
            Console.WriteLine("TAKE OFF");
            return true;
        }
        public int Fly(int distance)
        {
            Console.WriteLine("FLY");
            return 0;
        }
        public bool Land()
        {
            Console.WriteLine("LAND");
            return true;
        }
    }

}

