using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace Shortchase.Helpers
{
    public static class DateHelper
    {
        public static string DateFormat(DateTime? Date, bool FullMonth = false)
        {
            try
            {
                if (Date.HasValue)
                {

                    if (FullMonth) return String.Format("{0:MMMM dd, yyyy - hh:mm:ss tt}", Date.Value);
                    else return String.Format("{0:MMM dd, yyyy - hh:mm:ss tt}", Date.Value);
                }
                else return "No Date";
            }
            catch
            {
                return "No Date";
            }
        }

        public static string TrendingDateFormat(DateTime? Date)
        {
            try
            {
                if (Date.HasValue)
                {

                    return String.Format("{0:MMMM dd, yyyy hh:mm tt}", Date.Value);
                }
                else return "No Date";
            }
            catch
            {
                return "No Date";
            }
        }

        public static string POTDDateFormat(DateTime? Date)
        {
            try
            {
                if (Date.HasValue)
                {

                    return String.Format("{0:MMM dd, yyyy hh:mm tt}", Date.Value);
                }
                else return "No Date";
            }
            catch
            {
                return "No Date";
            }
        }

        ///
        /// Utility method used to display Time in a unambiguous layout
        /// <param name="date">The DateTime to be processed</param>
        ///
        public static string TimeFormat(DateTime? Date)
        {
            try
            {
                if (Date.HasValue)
                {
                    return String.Format("{0:hh:mm:ss tt}", Date.Value);
                }
                else return "No Time";
            }
            catch
            {
                return "No Time";
            }
        }

        ///
        /// Utility method used to display DateTime in a unambiguous layout without time
        /// <param name="date">The DateTime to be processed</param>
        ///
        public static string DateSimpleFormat(DateTime? Date, bool FullMonth = false)
        {
            try
            {
                if (Date.HasValue)
                {
                    if (FullMonth) return String.Format("{0:MMMM dd, yyyy}", Date.Value);
                    else return String.Format("{0:MMM dd, yyyy}", Date.Value);
                }
                else return "No Date";
            }
            catch
            {
                return "No Date";
            }
        }

        /// <summary>
        /// Converts a ISO 8601 date string to DateTime
        /// E.g. 2000-01-01T10:00:00Z
        /// </summary>
        /// <param name="IsoDate"></param>
        /// <returns>DateTime object or current Date if a string cannot be parsed</returns>
        public static DateTime FromISO(string IsoDate)
        {
            try
            {
                return DateTime.Parse(IsoDate, null, System.Globalization.DateTimeStyles.RoundtripKind);
            }
            catch
            {
                return DateTime.Now;
            }
        }




        ///
        /// Utility method used to display DateTime in a unambiguous layout without time
        /// <param name="date">The DateTime to be processed</param>
        ///
        public static string OrderableDate(DateTime? Date)
        {
            try
            {
                if (Date.HasValue)
                {
                    return String.Format("{0:yyyy-MM-dd}", Date.Value);
                }
                else return "No Date";
            }
            catch
            {
                return "No Date";
            }
        }

        /// <summary>
        /// Convert a DateTime (which might be null) from UTC timezone
        /// into the user's timezone. 
        /// </summary>
        /// <param name="timezoneOffset">Offset in Minutes</param>
        /// <returns></returns>
        public static DateTime FromUTCData(this DateTime dt, int timezoneOffset)
        {
            try
            {
                DateTime newDate = dt - new TimeSpan(timezoneOffset / 60, timezoneOffset % 60, 0);
                return newDate;
            }
            catch (Exception e)
            {
                return DateTime.Now;
            }
        }



        public static string GetDateStringForAPI(DateTime Date)
        {
            try
            {
                string value = (Date.Year >= 10 ? Date.Year.ToString() : "0" + Date.Year.ToString()) + "-" + (Date.Month >= 10 ? Date.Month.ToString() : "0" + Date.Month.ToString()) + "-" + (Date.Day >= 10 ? Date.Day.ToString() : "0" + Date.Day.ToString());

                return value;
            }
            catch
            {
                return null;
            }
        }



        public static string ToISOString(DateTime Date)
        {
            try
            {
                string year = (Date.Year >= 10 ? Date.Year.ToString() : "0" + Date.Year.ToString());
                string month = (Date.Month >= 10 ? Date.Month.ToString() : "0" + Date.Month.ToString());
                string day = (Date.Day >= 10 ? Date.Day.ToString() : "0" + Date.Day.ToString());
                string hour = (Date.Hour >= 10 ? Date.Hour.ToString() : "0" + Date.Hour.ToString());
                string minute = (Date.Minute >= 10 ? Date.Minute.ToString() : "0" + Date.Minute.ToString());
                string second = (Date.Second >= 10 ? Date.Second.ToString() : "0" + Date.Second.ToString());
                string text = year + "-" + month + "-" + day + "T" + hour + ":" + minute + ":" + second;
                return text;
            }
            catch
            {
                return "No Date";
            }
        }
    }
}
