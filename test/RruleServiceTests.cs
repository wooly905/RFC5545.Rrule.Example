using System;
using System.Collections.Generic;
using RruleTool.Repository;
using RruleTool.Rules;
using Xunit;

namespace RRule.Tests
{
    public class RruleServiceTests
    {
        [Theory]
        [InlineData("2019.06.19", 0, "")]
        [InlineData("2019.06.20", 2, "[10:00-13:00,14:00-17:00]")]
        [InlineData("2019.06.21", 1, "[12:00-16:00]")]
        [InlineData("2019.07.08", 1, "[10:00-19:00]")]
        [InlineData("2019.07.13", 0, "")]
        [InlineData("2019.07.18", 1, "[15:00-19:00]")]
        public void OutputTests(string input, int timePeriodCount, string expected)
        {
            DataAccessRepository repository = new DataAccessRepository();
            RruleService service = new RruleService(repository);

            if (DateTime.TryParse(input, out DateTime result))
            {
                IReadOnlyList<RruleTimes> times = service.GetAvailableTimesAsync(result).GetAwaiter().GetResult();
                Assert.Equal(timePeriodCount, times.Count);

                if (timePeriodCount > 0)
                {
                    string actual = $"[{string.Join(",", times)}]";
                    Assert.Equal(expected, actual);
                }
            }
        }
    }
}
