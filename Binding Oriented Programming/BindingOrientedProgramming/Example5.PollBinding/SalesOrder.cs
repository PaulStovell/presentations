using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace PollBindingExample {
    public class SalesOrder : DomainObject {
        private string _status;

        public string Status {
            get { return _status; }
            set {
                _status = value;
                this.OnPropertyChanged(new PropertyChangedEventArgs("Status"));
            }
        }
    }
}
