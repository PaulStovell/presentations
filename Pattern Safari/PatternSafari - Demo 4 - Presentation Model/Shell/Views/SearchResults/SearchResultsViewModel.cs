using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Common.Threading;
using Search.Public;
using SearchApplication.Views.SearchInput;

namespace SearchApplication.Views.SearchResults
{
    public class SearchResultsViewModel
    {
        private readonly ObservableCollection<ISearchResult> _searchResults = new ObservableCollection<ISearchResult>();
        private readonly ISearchInputView _searchInputView;
        private readonly IDispatcher _dispatcher;
        private readonly ISearchProvider[] _searchProviders;

        public SearchResultsViewModel(ISearchInputView searchInputView, IDispatcher dispatcher, ISearchProvider[] searchProviders)
        {
            _searchInputView = searchInputView;
            _dispatcher = dispatcher;
            _searchProviders = searchProviders;
        }

        public ObservableCollection<ISearchResult> SearchResults
        {
            get { return _searchResults; }
        }

        public void Initialize()
        {
            _searchInputView.SearchRequested += SearchInputView_SearchRequested;
        }

        private void SearchInputView_SearchRequested(object sender, SearchRequestEventArgs e)
        {
            SearchResults.Clear();

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
                            _dispatcher.Dispatch(
                                delegate
                                    {
                                        foreach (var result in results)
                                        {
                                            SearchResults.Add(result);
                                        }
                                    });
                        }
                    );
            }
        }
    }
}
