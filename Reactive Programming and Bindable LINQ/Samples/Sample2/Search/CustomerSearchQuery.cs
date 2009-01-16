﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sample2.Model;
using System.ComponentModel;
using System.Threading;

namespace Sample2.Binders
{
    internal sealed class CustomerSearchQuery : SearchQuery<Customer>
    {
        private string _firstName;
        private string _lastName;

        public CustomerSearchQuery()
        {

        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                this.OnPropertyChanged(new PropertyChangedEventArgs("FirstName"));
                Cancel();
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                this.OnPropertyChanged(new PropertyChangedEventArgs("LastName"));
                Cancel();
            }
        }

        protected override IEnumerable<Customer> SearchOnBackgroundThread()
        {
            List<Customer> results = new List<Customer>();
            for (int i = 0; i < 100; i++)
            {
                // Create some test data
                if (base.CancellationPending)
                {
                    break;
                }
                Customer p = new Customer();
                p.FirstName = _firstName + " " + i.ToString();
                p.LastName = _lastName + " " + i.ToString();
                results.Add(p);

                Thread.Sleep(10);
            }
            return results;
        }
    }
}
