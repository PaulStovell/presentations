using System;
using Demo.Aspects;

namespace Demo
{
    public class Customer : BusinessObjectBase
    {
        public Guid CustomerId { get; [NotifyChange]set; }
        public string FirstName { get; [NotifyChange]set; }
        public string LastName { get; [NotifyChange]set; }
        public DateTime? DateOfBirth { get; [NotifyChange]set; }

        public string FullName
        {
            [DependsOn("FirstName", "LastName")]
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }
    }
}
