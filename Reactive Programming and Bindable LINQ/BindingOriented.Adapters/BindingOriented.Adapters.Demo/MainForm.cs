using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BindingOriented.Adapters.Demo.ObjectModel;
using BindingOriented.Adapters.Demo.ObjectModel.Database;

namespace BindingOriented.Adapters.Demo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            this.contactBindingSource.DataSource =
                new AdventureWorksDataContext().Contacts
                    .Where(c => c.FirstName.StartsWith("P"))
                    .Take(30);
        }

        private void ContactsGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > 0 && e.RowIndex < contactDataGridView.Rows.Count)
            {
                DataGridViewRow row = contactDataGridView.Rows[e.RowIndex];
                Contact selectedContact = row.DataBoundItem as Contact;
                if (selectedContact != null)
                {
                    ContactEditDialog dialog = new ContactEditDialog(
                        new EditableContact(selectedContact));
                    dialog.ShowDialog();
                }
            }
        }
    }
}
