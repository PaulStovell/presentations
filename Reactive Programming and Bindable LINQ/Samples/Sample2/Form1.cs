using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sample2.Binders;

namespace Sample2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form_Loaded(object sender, EventArgs e)
        {
            customerSearchQueryBindingSource.DataSource = new CustomerSearchQuery();
        }

        private void SearchButton_Clicked(object sender, EventArgs e)
        {
            ((CustomerSearchQuery)customerSearchQueryBindingSource.DataSource).Execute();
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            ((CustomerSearchQuery)customerSearchQueryBindingSource.DataSource).Cancel();
        }
    }
}
