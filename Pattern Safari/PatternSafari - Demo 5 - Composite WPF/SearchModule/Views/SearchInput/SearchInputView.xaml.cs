using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Composite.Events;
using Search.Public;

namespace Search.Views.SearchInput
{
    public partial class SearchInputView : UserControl
    {
        private readonly IEventAggregator _eventAggregator;

        public SearchInputView(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            InitializeComponent();
        }

        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            var searchInformation = new SearchRequest();
            searchInformation.SearchText = _searchTextBox.Text;

            // Broadcast the search event
            _eventAggregator.GetEvent<PerformSearchBroadcastEvent>().Publish(searchInformation);
        }
    }
}
