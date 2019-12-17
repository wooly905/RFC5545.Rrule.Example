using System;
using RruleTool.Models;

namespace RruleTool.Abstractions
{
    internal abstract class BaseRuleAvailableDatesSelector
    {
        protected BaseRuleAvailableDatesSelector(RruleFreq frequency,
                                                 DateTime startDate,
                                                 DateTime endDate,
                                                 DateTime startTime,
                                                 DateTime endTime,
                                                 Rrule rule)
        {
            Frequency = frequency;
            StartDate = startDate;
            EndDate = endDate;
            StartTime = startTime;
            EndTime = endTime;
            Rule = rule;
        }

        public int Duration => (EndDate - StartDate).Days + 1;

        public DateTime EndDate { get; }

        public DateTime EndTime { get; }

        public RruleFreq Frequency { get; }

        public Rrule Rule { get; }

        public DateTime StartDate { get; }

        public DateTime StartTime { get; }

        public abstract bool ContainsDate(DateTime inputDate);
    }
}
