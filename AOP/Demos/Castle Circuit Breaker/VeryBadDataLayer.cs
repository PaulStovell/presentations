using System.Threading;
using Castle.Core;
using Demo.Interceptors;

namespace Demo
{
    [Interceptor(typeof(CircuitBreakerInterceptor))]
    public class VeryBadDataLayer : IVeryBadDataLayer
    {
        public Customer[] GetCustomers()
        {
            Thread.Sleep(1000);

            return new[]
                       {
                           new Customer() { FirstName = "Paul", LastName = "Stovell"},
                           new Customer() { FirstName = "Jason", LastName = "Stangroome"}
                       };
        }
    }
}
