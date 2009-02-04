using System.Linq;
using Demo3.Aspects;

namespace Demo3
{
    public class ContactSearchService : IContactSearchService
    {
        [LogAspect]
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

        [LogAspect]
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
