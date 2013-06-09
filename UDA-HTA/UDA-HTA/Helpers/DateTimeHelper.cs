using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UDA_HTA.Helpers
{
    public static class DateTimeHelper
    {

        public static DateTime SetDateTime(DateTime startDate, int hour, int minutes)
        {
            var date = startDate.Date.AddHours(hour).AddMinutes(minutes);

            if (date <= startDate)
                date = date.AddDays(1);

            return date;
        } 
    }
}
