using System;
using System.Diagnostics;
using Castle.Windsor;
using Demo.Interceptors;
using System.Threading;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new WindsorContainer();
            container.AddComponent<CircuitBreakerInterceptor>();
            container.AddComponent<IVeryBadDataLayer, VeryBadDataLayer>();

            var dataLayer = container.Resolve<IVeryBadDataLayer>();

            while (true)
            {
                try
                {
                    var customers = dataLayer.GetCustomers();
                    Console.WriteLine("Got customers: {0}", customers.Length);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Thread.Sleep(300);
                }
            }
        }
    }
}