using System.Collections.Generic;
using RruleTool.Helpers;
using RruleTool.Abstractions;

namespace RruleTool.Models
{
    internal class Rrule
    {
        public Rrule(RruleFreq frequency, int interval, RruleDayOfWeek weekStartsAt, IReadOnlyList<RruleDayOfWeek> byDays)
        {
            Frequency = frequency;
            Interval = interval;
            WeekStartsAt = weekStartsAt;
            ByDays = byDays;
        }

        public RruleFreq Frequency { get;  }

        public int Interval { get;  }

        public RruleDayOfWeek WeekStartsAt { get;  }

        public IReadOnlyList<RruleDayOfWeek> ByDays { get; }
    }
}
