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
    public partial class BindingExamplePage : System.Windows.Controls.Page
    {
        public BindingExamplePage()
        {
            InitializeComponent();

            // Create a data object and make it the data context for our form
            Customer customer = new Customer();
            customer.FirstName = "Paul";
            customer.LastName = "Stovell";
            this.DataContext = customer;
        }
    }

    public class Customer : INotifyPropertyChanged
    {
        private string _firstName;
        private string _lastName;

        public string FirstName
        {
            get { return _firstName; }
            set 
            {
                _firstName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("FirstName"));
                OnPropertyChanged(new PropertyChangedEventArgs("FullName"));
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LastName"));
                OnPropertyChanged(new PropertyChangedEventArgs("FullName"));
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