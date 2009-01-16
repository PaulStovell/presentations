using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using TimesheetEntrySystem.DomainModel.Contexts.Timesheeting.DataLayer;
using TimesheetEntrySystem.DomainModel.Services;

namespace TimesheetEntrySystem.DomainModel
{
    public class TimesheetSystem
    {
        private ITimesheetingDataService _dataService;

        public TimesheetSystem()
        {
            _dataService = new SqlTimesheetingDataService();
        }

        public TimesheetSystem(ITimesheetingDataService dataService)
        {
            _dataService = dataService;
        }



        public BindingList<Timesheet> GetTimesheets(Consultant consultant, DateTime startRange, DateTime endRange);

        public void SaveTimesheet(Timesheet timesheet);

        public Guid LockTimesheet(Timesheet timesheet);

        public void ReleaseTimesheet(Timesheet timesheet, Guid key);


        public Invoice GenerateInvoice(Customer customer, DateTime month)
        {
            InvoiceGenerator g = new InvoiceGenerator();
            return g.Generate();
        }

        public Invoice GetLatestInvoice(Customer customer);

        public Invoice GetInvoice(Customer customer, DateTime month);

        public void SaveInvoice(Invoice invoice);
    }
}
