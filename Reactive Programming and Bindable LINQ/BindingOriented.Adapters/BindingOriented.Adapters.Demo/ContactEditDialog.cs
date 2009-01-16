using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BindingOriented.Adapters.Demo.ObjectModel;

namespace BindingOriented.Adapters.Demo
{
    public partial class ContactEditDialog : Form
    {
        public ContactEditDialog(EditableContact contact)
        {
            InitializeComponent();

            editableContactBindingSource.DataSource = contact;
            contact.BeginEdit();
        }

        public EditableContact Contact
        {
            get { return editableContactBindingSource.DataSource as EditableContact; }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            this.Contact.EndEdit();
            this.DialogResult = DialogResult.OK;
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            this.Contact.EndEdit();
            this.Contact.BeginEdit();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            this.Contact.CancelEdit();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Contact.CancelEdit();
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
