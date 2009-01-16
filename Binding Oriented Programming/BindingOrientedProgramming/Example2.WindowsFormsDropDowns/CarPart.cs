using System;
using System.Collections.Generic;
using System.Text;

namespace Example2.WindowsFormsDropDowns
{
    public class CarPart
    {
        private string _name;

        public CarPart(string name) {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }
    }
}
