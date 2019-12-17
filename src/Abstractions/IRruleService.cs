using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RruleTool.Rules;

namespace RruleTool.Abstractions
{
    public interface IRruleService
    {
        Task<IReadOnlyList<RruleTimes>> GetAvailableTimesAsync(DateTime date);
    }
}
