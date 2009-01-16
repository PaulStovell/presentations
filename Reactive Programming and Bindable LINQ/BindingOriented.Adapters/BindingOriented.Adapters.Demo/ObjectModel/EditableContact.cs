using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BindingOriented.Adapters.Demo.ObjectModel.Database;

namespace BindingOriented.Adapters.Demo.ObjectModel
{
    public class EditableContact : EditableAdapter<Contact>
    {
        public EditableContact(Contact contact)
            : base(contact)
        {
        }

        public string FullName
        {
            get 
            { 
                return string.Format("{0} {1}", 
                    this.ReadUncommitted("FirstName"),
                    this.ReadUncommitted("LastName"));
            }
        }
    }
}
