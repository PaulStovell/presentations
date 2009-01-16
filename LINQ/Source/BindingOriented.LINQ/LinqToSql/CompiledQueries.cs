using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace LinqToSql
{
    class CompiledQueries
    {
        public static IQueryable<Contact> GetContactsStartingWith(AdventureWorksDataContext context, string s)
        {
            return null;// _compiled.Invoke(context, s);
        }
    }
}
