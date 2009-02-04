using System.Linq;
using Demo2;

namespace Demo2
{
    public class ContactSearchService : IContactSearchService
    {
        public Contact[] FindByName(string name)
        {
            using (var context = new AdventureWorksDataContext())
            {
                return (from contact in context.Contacts
                        where contact.FirstName.StartsWith(name) || contact.LastName.StartsWith(name)
                        select contact
                       )
                    .Take(3)
                    .ToArray();
            }
        }

        public Contact[] FindByEmailAddress(string emailAddress)
        {
            using (var context = new AdventureWorksDataContext())
            {
                return (from contact in context.Contacts
                        where contact.EmailAddress == emailAddress
                        select contact)
                    .Take(3)
                    .ToArray();
            }
        }
    }
}