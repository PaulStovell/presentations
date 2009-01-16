using AdventureWorksSearch.Data;
using Search.Public;

namespace AdventureWorksSearch.Searching
{
    internal class AdventureWorksSearchResult : ISearchResult
    {
        internal AdventureWorksSearchResult(Contact contact)
        {
            Text = contact.FirstName + " " + contact.LastName;
            Contact = contact;
        }

        public string Text { get; set; }
        public Contact Contact { get; set; }
    }
}