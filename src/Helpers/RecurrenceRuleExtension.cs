using System;
using System.Collections.Generic;

namespace RruleTool.Helpers
{
    internal static class RecurrenceRuleExtension
    {
        public static bool ContainsWeekDay(this IReadOnlyList<RruleDayOfWeek> days, DateTime date)
        {
            foreach (RruleDayOfWeek day in days)
            {
                if (day.IsWeekDayMatch(date))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
