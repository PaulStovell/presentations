using System.Linq;
using System.Threading;
using Common.Threading;
using Search.Public;
using SearchApplication.Views.SearchInput;

namespace SearchApplication.Views.SearchResults
{
    public class SearchResultsPresenter
    {
        private readonly ISearchInputView _searchInput;
        private readonly IDispatcher _dispatcher;
        private readonly ISearchProvider[] _searchProviders;

        public SearchResultsPresenter(ISearchInputView searchInputView, ISearchResultsView view, IDispatcher dispatcher, ISearchProvider[] searchProviders)
        {
            View = view;
            View.Loaded += View_Loaded;
            _searchInput = searchInputView;
            _dispatcher = dispatcher;
            _searchProviders = searchProviders;
        }

        private void View_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _searchInput.SearchRequested += SearchInput_SearchRequested;   
        }

        public ISearchResultsView View { get; private set; }

        private void SearchInput_SearchRequested(object sender, SearchRequestEventArgs e)
        {
            View.ClearResults();

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
                                            View.AddResult(result);
                                        }
                                    });
                        }
                    );
            }
        }
    }
}
