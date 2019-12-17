using System;
using RruleTool.Abstractions;
using RruleTool.Models;
using RruleTool.Rules.Operators;

namespace RruleTool.Rules
{
    internal static class RuleAvailableDatesSelectorFactory
    {
        public static BaseRuleAvailableDatesSelector GetRruleOperator(DateTime startDate,
                                                                      DateTime endDate,
                                                                      DateTime startTime,
                                                                      DateTime endTime,
                                                                      string rule)
        {
            Rrule recurRule = RruleParser.Parse(rule);

            if (recurRule == null)
            {
                return null;
            }

            if (recurRule.Frequency == RruleFreq.Daily)
            {
                return new AvailableDatesDailySelector(startDate, endDate, startTime, endTime, recurRule);
            }

            if (recurRule.Frequency == RruleFreq.Weekly)
            {
                return new AvailableDatesWeeklySelector(startDate, endDate, startTime, endTime, recurRule);
            }

            if (recurRule.Frequency == RruleFreq.Monthly)
            {
                return new AvailableDatesMonthlySelector(startDate, endDate, startTime, endTime, recurRule);
            }

            if (recurRule.Frequency == RruleFreq.Yearly)
            {
                return new AvailableDatesYearlySelector(startDate, endDate, startTime, endTime, recurRule);
            }

            return null;
        }
    }
}
