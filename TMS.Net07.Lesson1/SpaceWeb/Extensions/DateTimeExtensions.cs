using System;
using System.Linq;

namespace SpaceWeb.Extensions
{
    public static class DateTimeExtensions
    {
        public static int GetWorkingDaysInPeriod(this DateTime start, DateTime due)
        {
            return Enumerable.Range(0, (due - start).Days)
                            .Select(days => start.AddDays(days))
                            .Count(date => date.DayOfWeek != DayOfWeek.Sunday && date.DayOfWeek != DayOfWeek.Saturday);
        }
    }
}