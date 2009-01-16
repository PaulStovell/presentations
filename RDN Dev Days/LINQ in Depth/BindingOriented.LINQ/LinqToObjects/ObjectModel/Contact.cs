using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqToObjects.ObjectModel
{
    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }

        public static List<Contact> GetAll()
        {
            List<Contact> contacts = new List<Contact>() {
                new Contact() { FirstName =  "Paul", LastName = "Stovell", CompanyName = "Readify", PostCode = "5022" },
                new Contact() { FirstName =  "Mitch", LastName = "Denny", CompanyName = "Readify", PostCode = "3022" },
                new Contact() { FirstName =  "Darren", LastName = "Neimke", CompanyName = "Readify", PostCode = "5902" },
                new Contact() { FirstName =  "Richard", LastName = "Banks", CompanyName = "Readify", PostCode = "2001" },
                new Contact() { FirstName =  "Scott", LastName = "Guthrie", CompanyName = "Microsoft", PostCode = "40001" },
                new Contact() { FirstName =  "Scott", LastName = "Hanselman", CompanyName = "Microsoft", PostCode = "40001" },
                new Contact() { FirstName =  "Martin", LastName = "Fowler", CompanyName = "Thoughtworks", PostCode = "40922" }
            };
            return contacts;
        }
    }
}
