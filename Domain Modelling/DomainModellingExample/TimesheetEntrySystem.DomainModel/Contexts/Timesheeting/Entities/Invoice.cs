using System;
using System.Collections.Generic;
using System.Text;

namespace TimesheetEntrySystem.DomainModel
{
    public class Invoice : Entity
    {
        public Customer Customer
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public List<Timesheet> Timesheets
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }
}
