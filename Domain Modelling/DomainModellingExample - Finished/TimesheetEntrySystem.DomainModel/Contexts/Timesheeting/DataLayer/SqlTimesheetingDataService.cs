using System;
using System.Collections.Generic;
using System.Text;

namespace TimesheetEntrySystem.DomainModel.Contexts.Timesheeting.DataLayer {
    class SqlTimesheetingDataService : ITimesheetingDataService {
        public List<Timesheet> GetTimesheets(Consultant consultant, DateTime startRange, DateTime endRange) {
            
        }

        public List<Timesheet> GetTimesheets(Customer customer, DateTime startRange, DateTime endRange) {
            
        }

        public void SaveTimesheet(Timesheet timesheet) {
            
        }

        public List<Invoice> GetInvoices(Customer customers, DateTime startRange, DateTime endRange) {
            
        }

        public void SaveInvoice(Invoice invoice) {

        }
    }
}
