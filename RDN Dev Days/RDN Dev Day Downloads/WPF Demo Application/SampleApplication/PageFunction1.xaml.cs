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
    /// Interaction logic for PageFunction1.xaml
    /// </summary>

    public partial class PageFunction1 : System.Windows.Navigation.PageFunction<String>
    {

        public PageFunction1()
        {
            InitializeComponent();
        }

        private void SaveButton_Click()
        {
            this.OnReturn(new ReturnEventArgs<string>());
        }
    }
}