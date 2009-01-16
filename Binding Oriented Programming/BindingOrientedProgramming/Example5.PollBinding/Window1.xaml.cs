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
using System.ComponentModel;
using System.Threading;

namespace PollBindingExample {
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : System.Windows.Window {
        private SalesOrder _salesOrder;

        public Window1() {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(Window_Loaded);
        }

        void Window_Loaded(object sender, RoutedEventArgs e) {
            _salesOrder = new SalesOrder();
            this.DataContext = _salesOrder;
        }

        private void Button_Clicked(object sender, RoutedEventArgs e) {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerAsync();
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e) {
            // Keep going to the database to see what the current status of this sale is
            _salesOrder.Status = "Gathering books...";
            Thread.Sleep(1000);
            _salesOrder.Status = "Printing labels...";
            Thread.Sleep(1000);
            _salesOrder.Status = "Packaging...";
            Thread.Sleep(1000);
            _salesOrder.Status = "Shipping...";
            Thread.Sleep(1000);
            _salesOrder.Status = "Arrived!";
        }
    }
}