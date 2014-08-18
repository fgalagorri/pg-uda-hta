﻿using System;

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


        public static int CalculateAge(this DateTime d, DateTime? date = null)
        {
            DateTime now = date.HasValue ? date.Value : DateTime.Today;
            int age = now.Year - d.Year;
            if (d.Date > now.AddYears(-age))
                age--;
            return age;
        }
    }
}