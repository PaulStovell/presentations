using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.ComponentModel;

namespace TimesheetEntrySystem.DomainModel
{
    public class TimesheetEntryCollection : IEnumerable<TimesheetEntry>
    {
        private List<TimesheetEntry> _entries;

        public TimesheetEntryCollection()
        {

        }

        public TimesheetEntry AddNew()
        {
            TimesheetEntry entry = null;
            if (_entries.Count < 7)
            {
                entry = new TimesheetEntry();
                _entries.Add(entry);
            }
            return entry;
        }

        public void RemoveEntry(TimesheetEntry entry)
        {
            if (_entries.Contains(entry))
            {
                _entries.Remove(entry);
            }
        }

        public bool CanAddNew
        {
            get { return _entries.Count < 7; }
        }

        public IEnumerator<TimesheetEntry> GetEnumerator()
        {
            return _entries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
