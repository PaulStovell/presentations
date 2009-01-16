using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Practices.Composite.Events;
using Search.Public;

namespace Advertising.Views.Advert
{
    /// <summary>
    /// Interaction logic for AdvertView.xaml
    /// </summary>
    public partial class AdvertView : UserControl
    {
        private readonly IEventAggregator _eventAggregator;
        private int _nextAdvertId = 0;

        public AdvertView(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<PerformSearchBroadcastEvent>().Subscribe(SearchPerformed);

            InitializeComponent();
        }

        private void SearchPerformed(SearchRequest obj)
        {
            // Decide on an image
            _image.Source = (ImageSource) Resources["Advert" + _nextAdvertId];
            _nextAdvertId++;
            if (_nextAdvertId >= 3) _nextAdvertId = 0;
        }
    }
}
