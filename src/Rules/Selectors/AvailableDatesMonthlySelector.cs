using System;
using RruleTool.Abstractions;
using RruleTool.Models;

namespace RruleTool.Rules.Operators
{
    // For example purpose only. No implementation here.
    internal class AvailableDatesMonthlySelector : BaseRuleAvailableDatesSelector
    {
        public AvailableDatesMonthlySelector(DateTime startDate,
                                             DateTime endDate,
                                             DateTime startTime,
                                             DateTime endTime,
                                             Rrule rule)
            : base(RruleFreq.Monthly, startDate, endDate, startTime, endTime, rule)
        {
        }

        public override bool ContainsDate(DateTime inputDate)
        {
            // For example purpose only. No implementation here.
            throw new NotImplementedException();
        }
    }
}
