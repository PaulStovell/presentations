using System;
using System.Collections.Generic;
using System.Text;
using TimesheetEntrySystem.DomainModel.Contexts.Timesheeting.DataLayer;
using TimesheetEntrySystem.DomainModel;

namespace TimesheetEntrySystem.Tests.TimesheetingTests
{
    class MockTimesheetDataService : ITimesheetingDataService
    {
        public List<Timesheet> GetTimesheets(Consultant consultant, DateTime startRange, DateTime endRange)
        {

        }

        public List<Timesheet> GetTimesheets(Customer customer, DateTime startRange, DateTime endRange)
        {

        }

        public void SaveTimesheet(Timesheet timesheet)
        {

        }

        public List<Invoice> GetInvoices(Customer customers, DateTime startRange, DateTime endRange)
        {

        }

        public void SaveInvoice(Invoice invoice)
        {

        }
    }
}
