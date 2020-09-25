using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace OrzhansJozve.Utilities
{
    public static class ConvertDateToShamsi
    {
        public static string ToShamsi(this DateTime value)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            string fullMonthName = value.ToString("MMMM", CultureInfo.CreateSpecificCulture("fa"));
            return persianCalendar.GetDayOfMonth(value) + " " + fullMonthName + " " + persianCalendar.GetYear(value).ToString("00");
        }
        public static string MonthToShamsi(this DateTime value)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            string fullMonthName = value.ToString("MMMM", CultureInfo.CreateSpecificCulture("fa"));
            return fullMonthName;
        }
        public static DateTime ToGeorgianDateTime(this string persianDate)
        {
            int year = Convert.ToInt32(persianDate.Substring(0, 4));
            int month = Convert.ToInt32(persianDate.Substring(5, 2));
            int day = Convert.ToInt32(persianDate.Substring(8, 2));
            DateTime georgianDateTime = new DateTime(year, month, day, new PersianCalendar());
            return georgianDateTime;
        }
    }
}
