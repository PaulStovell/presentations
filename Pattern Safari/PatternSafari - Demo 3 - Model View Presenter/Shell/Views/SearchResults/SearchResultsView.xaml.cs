using System.Collections.ObjectModel;
using System.Windows.Controls;
using Search.Public;

namespace SearchApplication.Views.SearchResults
{
    public partial class SearchResultsView : UserControl, ISearchResultsView
    {
        private readonly ObservableCollection<ISearchResult> _searchResults = new ObservableCollection<ISearchResult>();

        public SearchResultsView()
        {
            InitializeComponent();

            _list.ItemsSource = _searchResults;
        }

        public void ClearResults()
        {
            _searchResults.Clear();
        }

        public void AddResult(ISearchResult searchResult)
        {
            _searchResults.Add(searchResult);
        }
    }
}
