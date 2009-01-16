using System;
using System.Collections.Generic;
using System.Text;

namespace BindingBasics.WindowsForms
{
    public class CustomerFilterableCollection : FilterableCollection<Customer>
    {
        public CustomerFilterableCollection(IList<Customer> items)
            : base(items)
        {

        }

        protected override bool FilterItem(Customer item)
        {
            return item.FullName.ToLower().Contains(this.FilterText.ToLower());
        }
    }
}
