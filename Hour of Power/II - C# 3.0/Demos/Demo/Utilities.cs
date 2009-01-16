using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace Demo
{
    public static class Utilities
    {
        public static int ToInteger(object o)
        {
            return int.Parse(o.ToString());
        }

        public static TimeSpan Days(int days)
        {
            return TimeSpan.FromDays(days);
        }

        public static TimeSpan Hours(int hours)
        {
            return TimeSpan.FromHours(hours);
        }

        public static TimeSpan Minutes(int minutes)
        {
            return TimeSpan.FromMinutes(minutes);
        }

        public static DateTime Ago(TimeSpan timespan)
        {
            return DateTime.Now.Subtract(timespan);
        }
    }










    // VB: 
    //       <Extension()> _ 
    //       Public Function Ago(timespan as TimeSpan)
}
