using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Sample1
{
    public class SmartContact : INotifyPropertyChanged
    {
        private string _firstName;
        private string _lastName;

        public SmartContact()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string LastName
        {
            get { return _lastName; }
            set 
            {
                _lastName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LastName"));
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("FirstName"));
            }
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
