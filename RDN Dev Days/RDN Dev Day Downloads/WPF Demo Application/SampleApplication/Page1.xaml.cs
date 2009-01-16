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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SampleApplication
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>

    public partial class Page1 : System.Windows.Controls.Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void Button1_Click()
        {
            PageFunction1 function = new PageFunction1();
            function.Return += new ReturnEventHandler<string>(function_Return);
            this.NavigationService.Navigate(function);
        }

        void function_Return(object sender, ReturnEventArgs<string> e)
        {
            
        }
    }
}