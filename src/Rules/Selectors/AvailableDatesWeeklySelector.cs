using System;
using System.Collections.Generic;
using RruleTool.Abstractions;
using RruleTool.Helpers;
using RruleTool.Models;

namespace RruleTool.Rules.Operators
{
    internal class AvailableDatesWeeklySelector : BaseRuleAvailableDatesSelector
    {
        private readonly HashSet<(int Year, int Month, int Date)> _hashSet;

        public AvailableDatesWeeklySelector(DateTime startDate,
                                            DateTime endDate,
                                            DateTime startTime,
                                            DateTime endTime,
                                            Rrule rule)
            : base(RruleFreq.Weekly, startDate, endDate, startTime, endTime, rule)
        {
            _hashSet = new HashSet<(int Year, int Month, int Date)>();
            GenerateAllDates();
        }

        public override bool ContainsDate(DateTime inputDate)
        {
            return _hashSet.Contains((inputDate.Year, inputDate.Month, inputDate.Day));
        }

        private void GenerateAllDates()
        {
            if (Rule.Interval <= 0)
            {
                return;
            }

            GetDaysWithInterval();
        }

        private void GetDaysWithInterval()
        {
            // find first day of first week based on start date and wkst
            DateTime firstDate = GetFirstDateOfFirstRecurrentWeek();

            while (firstDate < EndDate)
            {
                GetDaysForAWeek(firstDate);
                firstDate = firstDate.AddDays(7 * Rule.Interval);
            }
        }

        private void GetDaysForAWeek(DateTime firstDateOfWeek)
        {
            for (int i = 0; i < 7; i++)
            {
                DateTime newDate = firstDateOfWeek.AddDays(i);

                if (newDate < StartDate)
                {
                    continue;
                }

                if (newDate > EndDate)
                {
                    break;
                }

                if (Rule.ByDays.ContainsWeekDay(newDate))
                {
                    _hashSet.Add((newDate.Year, newDate.Month, newDate.Day));
                }
            }
        }

        // find first day of first week based on start date and wkst
        private DateTime GetFirstDateOfFirstRecurrentWeek()
        {
            for (int i = 0; i < 7; i++)
            {
                if (Rule.WeekStartsAt.IsWeekDayMatch(StartDate.AddDays(-1 * i)))
                {
                    return StartDate.AddDays(-1 * i);
                }
            }

            // won't reach.
            return default;
        }
    }
}
