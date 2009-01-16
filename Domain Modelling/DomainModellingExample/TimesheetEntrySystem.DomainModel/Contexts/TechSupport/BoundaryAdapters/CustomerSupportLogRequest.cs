using System;
using System.Collections.Generic;
using System.Text;
using TimesheetEntrySystem.DomainModel.BoundaryContracts;
using TimesheetEntrySystem.DomainModel.Contexts.TechSupport.Entities;

namespace TimesheetEntrySystem.DomainModel.Contexts.TechSupport.BoundaryAdapters
{
    class CustomerSupportLogRequest : ICustomerSupportLogRequest
    {
        public CustomerSupportLogRequest(Customer c, SupportLog log)
        {

        }

        public int NumberOfCalls
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }
    }
}
