using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Example3.WpfSearchQuery
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : System.Windows.Window
    {
        private CustomerSearchQuery _customerSearchQuery;

        public Window1()
        {
            _customerSearchQuery = new CustomerSearchQuery();

            InitializeComponent();
        }

        public CustomerSearchQuery CustomerSearchQuery
        {
            get { return _customerSearchQuery; }
        }

        private void SearchButton_Clicked(object sender, EventArgs e)
        {
            this.CustomerSearchQuery.Execute();
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            this.CustomerSearchQuery.Cancel();
        }
    }
}