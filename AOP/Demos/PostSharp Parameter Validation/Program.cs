using Demo.Aspects;
using System;

[assembly: ParameterValidationAspect(AttributeTargetAssemblies = "*", AttributeTargetTypes = "*")]

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            DoStuff(new object(), -12);
            Console.WriteLine("Done");
            Console.ReadKey();
        }

        public static void DoStuff([NotNull]object o, [GreaterThan(0)] int customerId)
        {
            Console.WriteLine("Doing stuff...");
        }
    }
}