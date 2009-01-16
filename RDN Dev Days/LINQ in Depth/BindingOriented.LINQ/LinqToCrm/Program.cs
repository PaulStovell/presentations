using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToCrm.CrmWebService;
using LinqtoCRM;
using System.Net;

namespace LinqToCrm
{
    class Program
    {
        static void Main(string[] args)
        {
            CrmService myService = new CrmService();
            myService.Credentials = new NetworkCredential("Administrator", "mypassword");

            CrmQueryProvider provider = new CrmQueryProvider(myService);
            var results = from a in provider.Linq<account>()
                          where a.name == "Paul"
                          select new { a.name, a.accountid };


            foreach (var account in results)
            {
                Console.WriteLine("Name: " + account.name);
            }
        }
    }
}
