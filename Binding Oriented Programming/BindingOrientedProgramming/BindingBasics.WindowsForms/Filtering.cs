using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BindingBasics.WindowsForms
{
    public partial class Filtering : Form
    {
        public Filtering()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            List<Customer> customers = new List<Customer>();
            customers.Add(new Customer("Aymeric", "Gaurat Apelli"));
            customers.Add(new Customer("Mitch", "Denny"));
            customers.Add(new Customer("Damian", "Edwards"));
            customers.Add(new Customer("Greg", "Low"));
            customers.Add(new Customer("Chris", "Hewitt"));
            customers.Add(new Customer("Omar", "Besiso"));
            customers.Add(new Customer("Ducas", "Francis"));
            customers.Add(new Customer("Paul", "Stovell"));
            customers.Add(new Customer("Darren", "Neimke"));

            CustomerFilterableCollection filterableCustomers = new CustomerFilterableCollection(customers);

            customerFilterableCollectionBindingSource.DataSource = filterableCustomers;
        }
    }
}