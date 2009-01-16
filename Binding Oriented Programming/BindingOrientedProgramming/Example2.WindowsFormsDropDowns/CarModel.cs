using System;
using System.Collections.Generic;
using System.Text;

namespace Example2.WindowsFormsDropDowns
{
    public class CarModel
    {
        private string _name;
        private List<CarPart> _availableParts;

        public CarModel(string name)
        {
            _name = name;
            _availableParts = new List<CarPart>();
        }

        public string Name
        {
            get { return _name; }
        }

        public List<CarPart> AvailableParts
        {
            get { return _availableParts; }
        }
    }
}
