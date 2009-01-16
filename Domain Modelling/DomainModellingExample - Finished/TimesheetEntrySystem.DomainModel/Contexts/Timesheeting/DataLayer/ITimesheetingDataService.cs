using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace TimesheetEntrySystem.DomainModel.Contexts.Timesheeting.DataLayer {
    public interface ITimesheetingDataService {
        public List<Timesheet> GetTimesheets(Consultant consultant, DateTime startRange, DateTime endRange);
        public List<Timesheet> GetTimesheets(Customer customer, DateTime startRange, DateTime endRange);
        public void SaveTimesheet(Timesheet timesheet);

        public Guid LockTimesheet(Timesheet timesheet);
        public void ReleaseTimesheet(Timesheet timesheet, Guid key);

        public List<Invoice> GetInvoices(Customer customers, DateTime startRange, DateTime endRange);
        public void SaveInvoice(Invoice invoice);
    }
}
