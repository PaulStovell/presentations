using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace WindowsApplication9
{
    public class SecurityManagerBindingSource : BindingSource
    {
        public SecurityManagerBindingSource()
        {
            this.DataSource = SecurityManager.Current;
        }
    }
}
