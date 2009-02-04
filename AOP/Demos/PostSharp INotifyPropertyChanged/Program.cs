using System.ComponentModel;
using Demo.Aspects;
using System;

[assembly: CrossPropertyDependenciesAspect(AttributeTargetAssemblies = "*", AttributeTargetTypes = "*")]

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var customer = new Customer();
            customer.PropertyChanged += Customer_PropertyChanged;

            Console.WriteLine("Changing customer ID");
            customer.CustomerId = Guid.NewGuid();

            Console.WriteLine("Changing customer first name");
            customer.FirstName = "Paul";

            Console.ReadKey();
        }

        private static void Customer_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine("    Property change: {0}", e.PropertyName);
        }
    }
}