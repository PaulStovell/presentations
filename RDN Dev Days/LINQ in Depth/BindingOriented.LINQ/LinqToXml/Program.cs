using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LinqToXml
{
    class Program
    {
        static void Main(string[] args)
        {
            var xml = new XDocument(
                new XElement("Contacts",
                    new XElement("Contact",
                        new XElement("Name", "Paul Stovell"),
                        new XElement("Company", "Readify"),
                        new XElement("Postcode", "5022")
                    ),
                    new XElement("Contact",
                        new XElement("Name", "Scott Guthrie"),
                        new XElement("Company", "Microsoft"),
                        new XElement("Postcode", "40001")
                    ),
                    new XElement("Contact",
                        new XElement("Name", "Darren Neimke"),
                        new XElement("Company", "Readify"),
                        new XElement("Postcode", "5198")
                    )));

            Console.WriteLine(xml.ToString());

            var companies = xml.Elements()
                .SelectMany(e => e.Elements())
                .SelectMany(e => e.Elements())
                .Where(e => e.Name == "Company")
                .Select(e => e.Value)
                .Distinct();

            Console.WriteLine();
            Console.WriteLine("Companies: ");
            Console.WriteLine();
            foreach (string company in companies)
            {
                Console.WriteLine(" - " + company);
            }

            Console.ReadKey();
        }
    }
}
