using System.Collections.Generic;
using System.Linq;
using Common.Infrastructure;
using Search.Public;
using SearchApplication.Data;

namespace SearchApplication.Searching
{
    public class AdventureWorksSearchProvider : ISearchProvider
    {
        private readonly ILog _log;

        public AdventureWorksSearchProvider(ILog log)
        {
            _log = log;
        }

        public IEnumerable<ISearchResult> Search(string text)
        {
            _log.Write(string.Format("Performing search for: '{0}'", text));

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