using System.Windows;

namespace SearchApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Setup the main window
            MainWindow = new SearchWindow();
            MainWindow.Show();
        }
    }
}