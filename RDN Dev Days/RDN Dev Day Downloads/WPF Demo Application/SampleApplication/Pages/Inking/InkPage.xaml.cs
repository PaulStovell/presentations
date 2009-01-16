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
using System.Windows.Ink;

namespace SampleApplication.Pages.Inking
{
    /// <summary>
    /// Interaction logic for InkPage.xaml
    /// </summary>
    public partial class InkPage : System.Windows.Controls.Page
    {
        public InkPage()
        {
            InitializeComponent();
        }

        private void InkCanvas_StrokeCollected(object sender, InkCanvasStrokeCollectedEventArgs e)
        {
            InkAnalyzer analyser = new InkAnalyzer();
            analyser.AddStrokes(_inkCanvas.Strokes);
            analyser.Analyze();
            _inkResults.Text = analyser.GetRecognizedString();
            e.Handled = false;
        }
    }
}