using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace SampleApplication.Pages.Binding
{
    /// <summary>
    /// Interaction logic for BindingExamplePage.xaml
    /// </summary>
    public partial class ValidationExamplePage : System.Windows.Controls.Page
    {
        public ValidationExamplePage()
        {
            InitializeComponent();

            // Create a data object and make it the data context for our form
            ValidatedCustomer customer = new ValidatedCustomer();
            customer.FirstName = "Paul";
            customer.LastName = "Stovell";
            this.DataContext = customer;
        }
    }

    public class ValidatedCustomer : INotifyPropertyChanged
    {
        private string _firstName;
        private string _lastName;

        public string FirstName
        {
            get { return _firstName; }
            set 
            { 
                _firstName = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("First name must be provided.");
                }
                this.OnPropertyChanged(new PropertyChangedEventArgs("FirstName"));
                this.OnPropertyChanged(new PropertyChangedEventArgs("FullName"));
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("Last name must be provided.");
                }
                this.OnPropertyChanged(new PropertyChangedEventArgs("LastName"));
                this.OnPropertyChanged(new PropertyChangedEventArgs("FullName"));
            }
        }

        public string FullName
        {
            get { return this.FirstName + " " + this.LastName; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, e);
            }
        }
    }
}