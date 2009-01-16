using Search.Public;
using SearchApplication.Data;

namespace SearchApplication.Searching
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