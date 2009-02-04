using System.Linq;
using Castle.Core;
using Demo1;
using Demo1.Interceptors;

namespace Demo1
{
    [Interceptor(typeof(LogInterceptor))]
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