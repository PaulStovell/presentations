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

namespace SampleApplication.Pages.Interop
{
    /// <summary>
    /// Interaction logic for InteropPage.xaml
    /// </summary>

    public partial class InteropPage : System.Windows.Controls.Page
    {
        public InteropPage()
        {
            InitializeComponent();
        }

        private void LaunchButton_Click(object sender, RoutedEventArgs e)
        {
            WindowsForm form = new WindowsForm();
            form.Show();
        }
    }
}