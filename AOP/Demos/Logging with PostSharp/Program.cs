using System;
using System.Diagnostics;

namespace Demo3
{
    class Program
    {
        static void Main(string[] args)
        {
            Trace.Listeners.Add(new ConsoleTraceListener());

            var service = new ContactSearchService();
            var results = service.FindByName("Paul");

            foreach (var result in results)
            {
                Console.WriteLine("{0,-20} {1,-20} {2,-20}", result.FirstName, result.LastName, result.EmailAddress);
            }

            Console.ReadKey();
        }
    }
}