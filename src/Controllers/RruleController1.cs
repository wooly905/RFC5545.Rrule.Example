using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RruleTool.Abstractions;
using RruleTool.Rules;

namespace RruleTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RruleController : ControllerBase
    {
        private readonly IRruleService _ruleServie;
        private readonly ISimpleDataCache _cache;

        public RruleController(IRruleService service, ISimpleDataCache cache)
        {
            _ruleServie = service;
            _cache = cache;
        }

        // GET api/rrule
        [HttpGet("{input}")]
        public async Task<ActionResult<IEnumerable<string>>> Get(string input)
        {
            IReadOnlyList<RruleTimes> times = null;

            if (!DateTime.TryParse(input, out DateTime inputDate))
            {
                return Array.Empty<string>();
            }

            if (_cache.TryGet(inputDate.ToString("MM/dd/yyyy"), out object value)
                && value is string[] data)
            {
                return data;
            }

            times = await _ruleServie.GetAvailableTimesAsync(inputDate).ConfigureAwait(false);

            if (times == null || times.Count == 0)
            {
                _cache.Set(inputDate.ToString("MM/dd/yyyy"), Array.Empty<string>());
                return Array.Empty<string>();
            }

            List<string> results = new List<string>();

            foreach (RruleTimes time in times)
            {
                results.Add(time.ToString());
            }

            string[] output = results.ToArray();
            _cache.Set(inputDate.ToString("MM/dd/yyyy"), output);

            return output;
        }
    }
}
