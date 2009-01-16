using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Castle.Windsor;
using Common.Threading;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Wpf.Events;
using Search.Public;

namespace Search.Views.SearchResults
{
    public class SearchResultsViewModel
    {
        private readonly ObservableCollection<ISearchResult> _searchResults = new ObservableCollection<ISearchResult>();
        private readonly IEventAggregator _eventAggregator;
        private readonly IDispatcher _dispatcher;
        private readonly ISearchProvider[] _searchProviders;

        public SearchResultsViewModel(IEventAggregator eventAggregator, IDispatcher dispatcher, ISearchProvider[] searchProviders)
        {
            _eventAggregator = eventAggregator;
            _dispatcher = dispatcher;
            _searchProviders = searchProviders;
        }

        public ObservableCollection<ISearchResult> SearchResults
        {
            get { return _searchResults; }
        }

        public void Initialize()
        {
            _eventAggregator.GetEvent<PerformSearchBroadcastEvent>().Subscribe(PerformSearch);
        }

        private void PerformSearch(SearchRequest searchInformation)
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
                            var results = searchProvider.Search(searchInformation.SearchText);
                            
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
