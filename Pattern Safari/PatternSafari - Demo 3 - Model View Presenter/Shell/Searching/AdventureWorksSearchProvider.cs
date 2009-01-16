using System.Collections.Generic;
using System.Linq;
using Search.Public;
using SearchApplication.Data;

namespace SearchApplication.Searching
{
    public class AdventureWorksSearchProvider : ISearchProvider
    {
        public IEnumerable<ISearchResult> Search(string text)
        {
            using (var dataContext = new AdventureWorksDataContext())
            {
                var results = (from contact in dataContext.Contacts
                               where contact.FirstName.StartsWith(text)
                               select new AdventureWorksSearchResult(contact)).Take(5);
                
                return results.ToList().Cast<ISearchResult>();
            }
        }
    }
}