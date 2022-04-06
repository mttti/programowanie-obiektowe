using System;

namespace lab_4
{
    enum Degree
    {
        BardzoDobry,
        DobryPlus,
        Dobry,
        DostatecznyPlus,
        Dostateczny
    }


    class Program
    {
        static void Main(string[] args)
        {
            Degree ocena = Degree.Dostateczny;
            Console.WriteLine(ocena);
        }
    }
}
