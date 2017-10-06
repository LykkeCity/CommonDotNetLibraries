using System;
using System.Collections.Generic;
using System.Globalization;

namespace Common
{
    public static class DateTimeUtils
    {
        public const int SecondsInDay = 86400;


        public static string StandartDateTimeMask
        {
            get
            {
                return "dd.MM.yy HH:mm:ss";
            }
        }

        public static string StandartDate
        {
            get
            {
                return "dd/MM/yyyy";
            }
        }

        [Obsolete("Use RoundToSecond(DateTime)")]
        public static DateTime TruncMiliseconds(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Kind);
        }

        [Obsolete("Use RoundToMinute(DateTime). RoundToMinute - is not misprint, RoundSeconds rounds to the minute")]
        public static DateTime RoundSeconds(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0, dateTime.Kind);
        }

        [Obsolete("Use RoundToMinute(DateTime, int)")]
        public static DateTime RoundToMinutes(DateTime dateTime, int minInterval)
        {
            var min = dateTime.Minute / minInterval * minInterval;
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, min, 0, dateTime.Kind);
        }

        // YYYY-MM-DD 12:00:00
        public static bool IsIsoDateTimeString(string value)
        {
            value = value.Trim();

            if (value.Length < 19)
                return false;


            if (!value[0].IsDigit())
                return false;

            if (!value[1].IsDigit())
                return false;

            if (!value[2].IsDigit())
                return false;

            if (!value[3].IsDigit())
                return false;

            if (value[4] != '-')
                return false;

            if (!value[5].IsDigit())
                return false;

            if (!value[6].IsDigit())
                return false;

            if (value[7] != '-')
                return false;

            if (!value[8].IsDigit())
                return false;

            if (!value[9].IsDigit())
                return false;

            if (value[10] != ' ')
                return false;

            if (!value[11].IsDigit())
                return false;

            if (!value[12].IsDigit())
                return false;

            if (value[13] != ':')
                return false;

            if (!StringUtils.IsDigit(value[14]))
                return false;

            if (!StringUtils.IsDigit(value[15]))
                return false;

            if (value[16] != ':')
                return false;

            if (!StringUtils.IsDigit(value[17]))
                return false;

            if (!StringUtils.IsDigit(value[18]))
                return false;

            return true;

        }

        /// <summary>
        /// Truncates <see cref="DateTime"/> to the seconds
        /// </summary>
        public static DateTime RoundToSecond(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Kind);
        }

        /// <summary>
        /// Truncates <see cref="DateTime"/> to the <paramref name="sec"/> seconds
        /// </summary>
        /// <param name="dateTime"><see cref="DateTime"/> to truncate</param>
        /// <param name="sec">5 - truncates to 5 seconds</param>
        public static DateTime RoundToSecond(this DateTime dateTime, int sec)
        {
            if (sec < 1 || sec > 59)
            {
                throw new ArgumentOutOfRangeException(nameof(sec), sec, "Should be number in range 1..59");
            }

            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second / sec * sec, dateTime.Kind);
        }

        /// <summary>
        /// Truncates <see cref="DateTime"/> to the minutes
        /// </summary>
        /// <param name="dateTime"><see cref="DateTime"/> to truncate</param>
        public static DateTime RoundToMinute(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year,dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0, dateTime.Kind);
        }


        /// <summary>
        /// Truncates <see cref="DateTime"/> to the <paramref name="min"/> minutes
        /// </summary>
        /// <param name="dateTime"><see cref="DateTime"/> to truncate</param>
        /// <param name="min">5 - truncates to 5 minutes</param>
        public static DateTime RoundToMinute(this DateTime dateTime, int min)
        {
            if (min < 1 || min > 59)
            {
                throw new ArgumentOutOfRangeException(nameof(min), min, "Should be number in range 1..59");
            }

            var part = dateTime.Minute / min;

            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, part * min, 0, dateTime.Kind);
        }


        /// <summary>
        /// Truncates <see cref="DateTime"/> to the hours
        /// </summary>
        /// <param name="dateTime"><see cref="DateTime"/> to truncate</param>
        public static DateTime RoundToHour(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0, dateTime.Kind);
        }

        /// <summary>
        /// Truncates <see cref="DateTime"/> to the <paramref name="hour"/> hours
        /// </summary>
        /// <param name="dateTime"><see cref="DateTime"/> to truncate</param>
        /// <param name="hour">5 - truncates to 5 hours</param>
        public static DateTime RoundToHour(this DateTime dateTime, int hour)
        {
            if (hour < 1 || hour > 23)
            {
                throw new ArgumentOutOfRangeException(nameof(hour), hour, "Should be number in range 1..23");
            }

            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour / hour * hour, 0, 0, dateTime.Kind);
        }

        /// <summary>
        /// Decreases date precision down to week. Shifts date to monday
        /// </summary>
        public static DateTime RoundToWeek(this DateTime dateTime)
        {
            var diff = dateTime.DayOfWeek - DayOfWeek.Monday;
            var dt = dateTime.AddDays(diff == -1 ? -6 : -diff); // shifting to monday

            return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, dateTime.Kind);
        }

        /// <summary>
        ///  Уменьшаем точность до часа - отбрасывая минуты
        /// </summary>
        /// <param name="dateTime">Исходное дата-время</param>
        /// <returns>Округленная дата-время</returns>
        public static DateTime RoundToMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1, 0, 0, 0, dateTime.Kind);
        }

        /// <summary>
        ///  Уменьшаем точность до года - отбрасывая все остальное
        /// </summary>
        /// <param name="dateTime">Исходное дата-время</param>
        /// <returns>Округленная дата-время</returns>
        public static DateTime RoundToYear(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, 1, 1, 0, 0, 0, dateTime.Kind);
        }
        
        public static string ToIsoDate(this DateTime dateTime)
        {
            return dateTime.Year + "-" + dateTime.Month.ToString("00", CultureInfo.InvariantCulture) + "-" +
                   dateTime.Day.ToString("00", CultureInfo.InvariantCulture);
        }

        public static string ToTimeString(this DateTime dateTime, char separator = ':')
        {
            return dateTime.Hour.ToString("00") + separator + dateTime.Minute.ToString("00") + separator +
                   dateTime.Second.ToString("00");
        }

        public static string ToIsoDateTime(this DateTime dateTime)
        {
            return dateTime.ToString(Utils.IsoDateTimeMask);
        }


        public static string ToYyyyMmDd(this DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMdd");
        }

        /// <summary>
        /// Сгенерировать даты (без времени)
        /// </summary>
        /// <param name="dateFrom">дата с которой начать</param>
        /// <param name="dateTo">дата которой закончить (включительно)</param>
        /// <returns></returns>
        public static IEnumerable<DateTime> GenerateDates(DateTime dateFrom, DateTime dateTo)
        {
            dateFrom = dateFrom.Date;
            dateTo = dateTo.Date;

            while (dateFrom <= dateTo)
            {
                yield return dateFrom;
                dateFrom = dateFrom.AddDays(1);
            }
        }

        /// <summary>
        /// Сгенерировать даты за определенный месяц
        /// </summary>
        public static IEnumerable<DateTime> GenerateMonthDates(int year, int month)
        {
            var dateFrom = new DateTime(year, month, 1);
            var dateTo = dateFrom.AddDays(DateTime.DaysInMonth(year, month)-1);

            return GenerateDates(dateFrom, dateTo);
        }

        public static DateTime SetTime(this DateTime src, int hours, int mins, int seconds)
        {
            return new DateTime(src.Year, src.Month, src.Day, hours, mins, seconds, src.Kind);
        }

        public static string YearLastTwoDigits(this int year)
        {
            var result = year.ToString(CultureInfo.InvariantCulture);

            if (result.Length == 4)
                return result.Substring(2,2);

            return result;
        }


        public static string ToHtmlComponentDate(this DateTime dateTime)
        {
            return dateTime.Day.ToString("00") + "/" + dateTime.Month.ToString("00") + "/" + dateTime.Year;
        }


        private static DateTime _baseDateTime = new DateTime(1970,1,1);

        public static DateTime FromUnixDateTime(this uint unixDateTime)
        {
            return _baseDateTime.AddSeconds(unixDateTime);
        }

        public static double ToUnixTime(this DateTime dateTime)
        {
            return (dateTime - _baseDateTime).TotalMilliseconds;
        }

        public static string ToDateString(this DateTime? value)
        {
            return value?.ToString("d");
        }

        /// <summary>
        /// Returns date of the first week's monday in the specified year.
        /// </summary>
        public static DateTime GetFirstWeekOfYear(int year, DateTimeKind kind = DateTimeKind.Utc)
        {
            var dt = new DateTime(year, 1, 1, 0, 0, 0, kind);

            if (dt.DayOfWeek == DayOfWeek.Monday)
            {
                return dt;
            }

            var diff = DayOfWeek.Monday - dt.DayOfWeek;

            return dt.AddDays(diff == 1 ? 1 : 7 + diff);
        }

        /// <summary>
        /// Returns date of the first weeks' monday in that year.
        /// </summary>
        public static DateTime GetFirstWeekOfYear(DateTime dt)
        {
            var diff = dt.DayOfWeek - DayOfWeek.Monday;
            var year = dt.AddDays(diff == -1 ? -6 : -diff).Year;

            return GetFirstWeekOfYear(year, dt.Kind);
        }
    }
}
