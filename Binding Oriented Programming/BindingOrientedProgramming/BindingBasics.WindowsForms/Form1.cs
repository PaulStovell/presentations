using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BindingBasics.WindowsForms
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Customer c = new Customer("Samantha", "Stovell");
            customerBindingSource.DataSource = c;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Customer c = ((Customer)customerBindingSource.DataSource);
            c.LastName = "Jones";

            MessageBox.Show(c.LastName);
        }
        
    }
}