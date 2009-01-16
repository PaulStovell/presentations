using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Threading;
using Search.Public;
using SearchApplication.Views.SearchInput;

namespace SearchApplication.Views.SearchResults
{
    public partial class SearchResultsView : UserControl
    {
        private readonly ObservableCollection<ISearchResult> _searchResults = new ObservableCollection<ISearchResult>();
        private readonly ISearchInputView _searchInputView;
        private readonly ISearchProvider[] _searchProviders;

        public SearchResultsView(ISearchInputView searchInputView, ISearchProvider[] searchProviders)
        {
            InitializeComponent();

            _searchInputView = searchInputView;
            _searchProviders = searchProviders;
            _list.ItemsSource = _searchResults;
        }

        private void View_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _searchInputView.SearchRequested += SearchInputView_SearchRequested;
        }

        private void SearchInputView_SearchRequested(object sender, SearchRequestEventArgs e)
        {
            _searchResults.Clear();

            // Start up enough threads to fetch the search results
            for (var i = 0; i < _searchProviders.Count(); i++)
            {
                var searchProvider = _searchProviders[i];
                ThreadPool.QueueUserWorkItem(
                    delegate
                    {
                        // Perform the search
                        var results = searchProvider.Search(e.SearchText);

                        // Add the results to the results list
                        Dispatcher.Invoke(
                            DispatcherPriority.Normal,
                            new Action(
                                delegate
                                {
                                    foreach (var result in results)
                                    {
                                        _searchResults.Add(result);
                                    }
                                }));
                    }
                    );
            }
        }
    }
}
