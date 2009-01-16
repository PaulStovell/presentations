using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace BindingBasics.WindowsForms
{
    public class Customer : INotifyPropertyChanged, IDataErrorInfo
    {
        private static readonly PropertyChangedEventArgs FirstNamePropertyChanged = new PropertyChangedEventArgs("FirstName");
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
                OnPropertyChanged(FirstNamePropertyChanged);
                OnPropertyChanged(new PropertyChangedEventArgs("FullName"));
            }
        }

        public string LastName
        {
            get { return _lastName ?? ""; }
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

        public string Error
        {
            get { return this[null]; }
        }

        public string this[string columnName]
        {
            get 
            {
                StringBuilder messageBuilder = new StringBuilder();

                columnName = columnName ?? "";

                if (columnName == "LastName" || columnName == "")
                {
                    if (this.LastName.Trim().Length == 0)
                    {
                        messageBuilder.AppendLine("Last name is required.");
                    }
                }
                if (columnName == "FirstName" || columnName == "")
                {
                    if (this.FirstName.Trim().Length == 0)
                    {
                        messageBuilder.AppendLine("First name is required.");
                    }
                }

                return messageBuilder.ToString().TrimEnd();
            }
        }

    }
}
