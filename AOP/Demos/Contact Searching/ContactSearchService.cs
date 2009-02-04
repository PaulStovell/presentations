using System;
using System.Diagnostics;
using System.Linq;
using Demo0;

namespace Demo0
{
    public class ContactSearchService : IContactSearchService
    {
        public Contact[] FindByName(string name)
        {
            Trace.WriteLine(string.Format("Begin FindByName (name = {0})", name));
            Trace.Indent();
            var context = new AdventureWorksDataContext();
            var results = null as Contact[];
            try
            {
                results = (from contact in context.Contacts
                           where contact.FirstName.StartsWith(name) || contact.LastName.StartsWith(name)
                           select contact)
                    .Take(3)
                    .ToList()
                    .ToArray();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                throw;
            }
            finally
            { 
                Trace.Unindent();
                Trace.WriteLine(string.Format("End FindByName {0}", name));
            }
            return results;
        }

        public Contact[] FindByEmailAddress(string emailAddress)
        {
            Trace.WriteLine(string.Format("Begin FindByEmailAddress (email = {0})", emailAddress));
            Trace.Indent();
            var context = new AdventureWorksDataContext();
            var results = null as Contact[];
            try
            {
                results = (from contact in context.Contacts
                           where contact.EmailAddress == emailAddress
                           select contact)
                    .Take(3)
                    .ToArray();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                throw;
            }
            finally
            {
                context.Dispose();
                Trace.Unindent();
                Trace.WriteLine(string.Format("End FindByName {0}", emailAddress));
            }
            return results;
        }
    }
}