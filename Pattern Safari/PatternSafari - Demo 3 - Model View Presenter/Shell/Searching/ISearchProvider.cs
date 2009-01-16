using System.Collections.Generic;

namespace Search.Public
{
    public interface ISearchProvider
    {
        IEnumerable<ISearchResult> Search(string text);
    }
}
