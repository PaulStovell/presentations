using System;
using System.Collections.Generic;
using System.Text;

namespace TimesheetEntrySystem.DomainModel
{
    public class Timesheet : Entity
    {
        private Customer _customer;
        private Consultant _consultant;
        private TimesheetEntryCollection _entries;

        public Timesheet()
        {
            _entries = new TimesheetEntryCollection();
        }

        public Customer Customer
        {
            get { return _customer; }
            set { _customer = value; }
        }

        public Consultant Consultant
        {
            get { return _consultant; }
            set { _consultant = value; }
        }

        public TimesheetEntryCollection Entries
        {
            get { return _entries; }
        }
    }
}
