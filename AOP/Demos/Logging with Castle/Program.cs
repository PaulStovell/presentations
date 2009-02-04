using System;
using System.Diagnostics;
using Castle.Windsor;
using Demo1.Interceptors;

namespace Demo1
{
    class Program
    {
        static void Main(string[] args)
        {
            Trace.Listeners.Add(new ConsoleTraceListener());

            var container = new WindsorContainer();
            container.AddComponent<LogInterceptor>();
            container.AddComponent<IContactSearchService, ContactSearchService>();

            var service = container.Resolve<IContactSearchService>();
            var results = service.FindByName("Paul");

            foreach (var result in results)
            {
                Console.WriteLine("{0,-20} {1,-20} {2,-20}", result.FirstName, result.LastName, result.EmailAddress);
            }

            Console.ReadKey();
        }
    }
}