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

        public RruleController(IRruleService service)
        {
            _ruleServie = service;
        }

        // GET api/rrule
        [HttpGet("{input}")]
        public async Task<ActionResult<IEnumerable<string>>> Get(string input)
        {
            IReadOnlyList<RruleTimes> times = null;

            if (DateTime.TryParse(input, out DateTime result))
            {
                times = await _ruleServie.GetAvailableTimesAsync(result).ConfigureAwait(false);
            }

            if (times == null || times.Count == 0)
            {
                return Array.Empty<string>();
            }

            List<string> results = new List<string>();

            foreach (RruleTimes time in times)
            {
                results.Add(time.ToString());
            }

            return results.ToArray();
        }
    }
}
