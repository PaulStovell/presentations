using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace WindowsApplication9
{
    public class SecurityManager
    {
        private static readonly SecurityManager _current = new SecurityManager();

        public static SecurityManager Current
        {
            get { return _current; }
        }

        public bool IsAdministrator
        {
            // Change this code to a role that does/doesn't exist
            get { return false; } // Thread.CurrentPrincipal.IsInRole("dssssssssministrators"); }
        }


    }
}
