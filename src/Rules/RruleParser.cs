using System;
using System.Collections.Generic;
using RruleTool.Abstractions;
using RruleTool.Helpers;
using RruleTool.Models;

namespace RruleTool.Rules
{
    internal static class RruleParser
    {
        public static Rrule Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }

            string[] tempElements = input.Split(';', StringSplitOptions.RemoveEmptyEntries);

            if (tempElements.Length <= 1)
            {
                return null;
            }

            if (!TryGetFreqency(tempElements, out RruleFreq frequency))
            {
                return null;
            }

            return new Rrule(frequency, GetInterval(tempElements), GetWeekdayStartsAt(tempElements), GetByDay(tempElements));
        }

        private static bool TryGetFreqency(string[] elements, out RruleFreq frequency)
        {
            string freqKeyword = "FREQ";

            for (int index = 0; index < elements.Length; index++)
            {
                string target = elements[index].Trim();

                if (!target.StartsWith(freqKeyword, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                string[] tempElements = target.Split('=', StringSplitOptions.RemoveEmptyEntries);

                if (tempElements.Length != 2)
                {
                    // default to Daily
                    frequency = RruleFreq.Daily;
                    return false;
                }

                if (string.Equals(tempElements[0].Trim(), freqKeyword, StringComparison.OrdinalIgnoreCase)
                    && TryGetFrequencyInternal(tempElements[1].Trim(), out RruleFreq value))
                {
                    frequency = value;
                    return true;
                }
            }

            frequency = RruleFreq.Daily;
            return false;
        }

        private static bool TryGetFrequencyInternal(string word, out RruleFreq frequency)
        {
            // Only parse 4 frequencies. RFC 5545 has more.
            string dailyKeyword = "DAILY";
            string weeklykeyword = "WEEKLY";
            string monthlyKeyword = "MONTHLY";
            string yearlyKeyword = "YEARLY";

            if (string.Equals(word, dailyKeyword, StringComparison.OrdinalIgnoreCase))
            {
                frequency = RruleFreq.Daily;
                return true;
            }

            if (string.Equals(word, weeklykeyword, StringComparison.OrdinalIgnoreCase))
            {
                frequency = RruleFreq.Weekly;
                return true;
            }

            if (string.Equals(word, monthlyKeyword, StringComparison.OrdinalIgnoreCase))
            {
                frequency = RruleFreq.Monthly;
                return true;
            }

            if (string.Equals(word, yearlyKeyword, StringComparison.OrdinalIgnoreCase))
            {
                frequency = RruleFreq.Yearly;
                return true;
            }

            frequency = RruleFreq.Daily;
            return false;
        }

        private static int GetInterval(string[] elements)
        {
            string internalKeyword = "INTERVAL";

            for (int index = 0; index < elements.Length; index++)
            {
                string element = elements[index].Trim();

                if (!element.StartsWith(internalKeyword))
                {
                    continue;
                }

                string[] tempElements = elements[index].Split('=', StringSplitOptions.RemoveEmptyEntries);

                if (tempElements.Length != 2)
                {
                    // default to 1
                    return 1;
                }

                if (int.TryParse(tempElements[1], out int value))
                {
                    return value;
                }
            }

            // default to 1
            return 1;
        }

        private static RruleDayOfWeek GetWeekdayStartsAt(string[] elements)
        {
            string wkstKeyword = "WKST";

            for (int index = 0; index < elements.Length; index++)
            {
                string target = elements[index].Trim();

                if (!target.StartsWith(wkstKeyword, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                string[] tempElements = target.Split('=', StringSplitOptions.RemoveEmptyEntries);

                if (tempElements.Length == 2
                    && RruleDayOfWeek.TryGetShortDayOfWeek(tempElements[1], out RruleDayOfWeek value))
                {
                    return value;
                }
            }

            // default to Monday
            return Helpers.RruleDayOfWeek.MO;
        }

        private static List<RruleDayOfWeek> GetByDay(string[] elements)
        {
            string byDayKeyword = "BYDAY";

            for (int index = 0; index < elements.Length; index++)
            {
                string target = elements[index].Trim();

                if (!target.StartsWith(byDayKeyword, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                string[] days = target.Split('=', StringSplitOptions.RemoveEmptyEntries);

                if (days.Length != 2)
                {
                    continue;
                }

                return GetByDayInternal(days[1]);
            }

            // default to emtpy list
            return new List<RruleDayOfWeek>();
        }

        private static List<RruleDayOfWeek> GetByDayInternal(string input)
        {
            string[] days = input.Split(',', StringSplitOptions.RemoveEmptyEntries);
            List<RruleDayOfWeek> result = new List<RruleDayOfWeek>();

            for (int index = 0; index < days.Length; index++)
            {
                if (RruleDayOfWeek.TryGetShortDayOfWeek(days[index], out RruleDayOfWeek value))
                {
                    result.Add(value);
                }
            }

            return result;
        }
    }
}
