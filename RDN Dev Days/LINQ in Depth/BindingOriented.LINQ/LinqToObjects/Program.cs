using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToObjects.ObjectModel;

namespace LinqToObjects
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            var contacts = Contact.GetAll();
            var results = contacts.Where(c => c.FirstName.StartsWith("S"))
                .OrderBy(c => c.LastName)
                .Union(otherContacts);

            foreach (var g in results)
            {
                Console.WriteLine(g.Key);
                foreach (var c in g)
                {
                    Console.WriteLine("  " + c.LastName + ", " + c.FirstName);
                }
            }

            Console.ReadKey();
        }
    }
}
