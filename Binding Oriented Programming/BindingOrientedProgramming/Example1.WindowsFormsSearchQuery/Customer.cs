using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Example1.WindowsFormsSearchQuery
{
    public class Customer
    {
        private string _firstName;
        private string _lastName;

        public Customer()
        {

        }

        public Customer(string firstname, string lastname)
        {
            this.FirstName = firstname;
            this.LastName = lastname;
        }

        public string FirstName
        {
            get { return _firstName ?? ""; }
            set 
            { 
                _firstName = value;
            }
        }

        public string LastName
        {
            get { return _lastName ?? ""; }
            set
            {
                _lastName = value;
            }
        }

        public string FullName
        {
            get { return this.FirstName + " " + this.LastName; }
        }
    }
}
