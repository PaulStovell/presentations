using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {










            string numericString = "15";
            int number = Utilities.ToInteger(numericString);
            Console.WriteLine(number);

            DateTime yesterday = Utilities.Ago(Utilities.Days(1));
            Console.WriteLine(yesterday);

            Console.ReadKey();
        }
    }
}
