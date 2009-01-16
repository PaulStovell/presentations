using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace TimesheetEntrySystem.DomainModel
{
    public class TimesheetEntry : Entity
    {
        private static readonly PropertyChangedEventArgs HoursWorkedChangedEventArgs = new PropertyChangedEventArgs("HoursWorked");
        private static readonly PropertyChangedEventArgs DayChangedEventArgs = new PropertyChangedEventArgs("Day");
        private Guid _id;
        private DateTime _day;
        private int _hoursWorked;
        private Guid _timesheetId;

        public TimesheetEntry()
        {
            _id = Guid.NewGuid();
        }

        public Guid Id
        {
            get { return _id; }
        }

        public DateTime Day
        {
            get { return _day; }
            set
            {
                _day = value;
                OnPropertyChanged(DayChangedEventArgs);
            }
        }

        public int HoursWorked
        {
            get { return _hoursWorked; }
            set
            {
                _hoursWorked = value;
                OnPropertyChanged(HoursWorkedChangedEventArgs);
            }
        }

        public override bool HasSameIdentity(Entity otherEntity)
        {
            return (otherEntity is TimesheetEntry) && ((TimesheetEntry)otherEntity).Id == this.Id;
        }
    }
}
