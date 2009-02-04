using System;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection;

namespace Demo2
{
    class Program
    {
        static void Main(string[] args)
        {
            Trace.Listeners.Add(new ConsoleTraceListener());

            var service = PolicyInjection.Wrap<IContactSearchService>(new ContactSearchService());
            var results = service.FindByName("Paul");

            foreach (var result in results)
            {
                Console.WriteLine("{0,-20} {1,-20} {2,-20}", result.FirstName, result.LastName, result.EmailAddress);
            }

            Console.ReadKey();
        }
    }
}