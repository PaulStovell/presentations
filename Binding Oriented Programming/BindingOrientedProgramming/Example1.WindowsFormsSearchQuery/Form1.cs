using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Example1.WindowsFormsSearchQuery
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form_Loaded(object sender, EventArgs e)
        {
            bindingSource1.DataSource = new CustomerSearchQuery();
        }

        private void SearchButton_Clicked(object sender, EventArgs e)
        {
            ((CustomerSearchQuery)bindingSource1.DataSource).Execute();
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            ((CustomerSearchQuery)bindingSource1.DataSource).Cancel();
        }
    }
}