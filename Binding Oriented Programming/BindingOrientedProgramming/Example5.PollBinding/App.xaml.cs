using System;
using System.Windows;
using System.Data;
using System.Xml;
using System.Configuration;

namespace PollBindingExample {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>

    public partial class App : System.Windows.Application {
        protected override void OnStartup(StartupEventArgs e) {
            BindingDispatcher.Current.EnablePropertyChangedNotifications();
            base.OnStartup(e);
        }
    }
}