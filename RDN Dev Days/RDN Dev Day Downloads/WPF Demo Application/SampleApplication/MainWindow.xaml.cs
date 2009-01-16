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
using System.Windows.Navigation;
using System.Windows.Media.Animation;


namespace SampleApplication
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new Uri("/HomePage.xaml", UriKind.Relative));
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            Storyboard b = this.Resources["Local_FadeInFrameStoryboard"] as Storyboard;
            if (b != null)
            {
                _frame.BeginStoryboard(b);
            }
        }

        private void Frame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            Storyboard b = this.Resources["Local_FadeOutFrameStoryboard"] as Storyboard;
            if (b != null)
            {
                _frame.BeginStoryboard(b);
            }
        }

        private void HomeImage_Clicked(object sender, MouseEventArgs e)
        {
            _frame.Navigate(new Uri("/HomePage.xaml", UriKind.Relative));
        }

        private void ExitImage_Clicked(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void RotateImage_Clicked(object sender, RoutedEventArgs e)
        {
            this.RenderTransform = new RotateTransform(0.00, this.ActualWidth / 2, this.ActualHeight / 2);
            Storyboard b = this.Resources["Local_SpinFrameStoryboard"] as Storyboard;
            if (b != null)
            {
                this.BeginStoryboard(b);
            }
        }
    }
}