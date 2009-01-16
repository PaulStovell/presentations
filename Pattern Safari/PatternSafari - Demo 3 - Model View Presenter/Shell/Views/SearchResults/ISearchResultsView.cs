using System.Windows;
using Search.Public;

namespace SearchApplication.Views.SearchResults
{
    public interface ISearchResultsView
    {
        void ClearResults();
        void AddResult(ISearchResult searchResult);
        event RoutedEventHandler Loaded;
    }
}
