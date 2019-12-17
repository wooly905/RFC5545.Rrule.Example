using System;

namespace RruleTool.Helpers
{
    internal sealed class RruleDayOfWeek
    {
        private readonly DayOfWeek _dayofWeek;

        private RruleDayOfWeek(DayOfWeek dayOfWeek)
        {
            _dayofWeek = dayOfWeek;
        }

        public string Name => _dayofWeek.ToString();

        public bool IsWeekDayMatch(DateTime target) => target.DayOfWeek == _dayofWeek;

        public static bool TryGetShortDayOfWeek(string input, out RruleDayOfWeek value)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                value = null;
                return false;
            }

            input = input.Trim();

            if (string.Equals(input, "SU", StringComparison.OrdinalIgnoreCase))
            {
                value = SU;
                return true;
            }

            if (string.Equals(input, "MO", StringComparison.OrdinalIgnoreCase))
            {
                value = MO;
                return true;
            }

            if (string.Equals(input, "TU", StringComparison.OrdinalIgnoreCase))
            {
                value = TU;
                return true;
            }

            if (string.Equals(input, "WE", StringComparison.OrdinalIgnoreCase))
            {
                value = WE;
                return true;
            }

            if (string.Equals(input, "TH", StringComparison.OrdinalIgnoreCase))
            {
                value = TH;
                return true;
            }

            if (string.Equals(input, "FR", StringComparison.OrdinalIgnoreCase))
            {
                value = FR;
                return true;
            }

            if (string.Equals(input, "SA", StringComparison.OrdinalIgnoreCase))
            {
                value = SA;
                return true;
            }

            value = null;
            return false;
        }

        public static RruleDayOfWeek SU => new RruleDayOfWeek(DayOfWeek.Sunday);

        public static RruleDayOfWeek MO => new RruleDayOfWeek(DayOfWeek.Monday);

        public static RruleDayOfWeek TU => new RruleDayOfWeek(DayOfWeek.Tuesday);

        public static RruleDayOfWeek WE => new RruleDayOfWeek(DayOfWeek.Wednesday);

        public static RruleDayOfWeek TH => new RruleDayOfWeek(DayOfWeek.Thursday);

        public static RruleDayOfWeek FR => new RruleDayOfWeek(DayOfWeek.Friday);

        public static RruleDayOfWeek SA => new RruleDayOfWeek(DayOfWeek.Saturday);
    }
}
