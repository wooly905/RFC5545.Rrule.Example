using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RruleTool.Abstractions;
using RruleTool.Models;

namespace RruleTool.Rules
{
    public class RruleService : IRruleService
    {
        private readonly IDataAccessRepository _repository;

        public RruleService(IDataAccessRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<RruleTimes>> GetAvailableTimesAsync(DateTime date)
        {
            // get data from database or cache
            IReadOnlyList<AvailabilityDataModel> models = await _repository.GetAvailabilitiesAsync();

            if (models == null || models.Count == 0)
            {
                return null;
            }

            // get Rrule by calling RruleParser
            IReadOnlyList<BaseRuleAvailableDatesSelector> operators = GetRuleOperators(models);

            if (operators.Count == 0)
            {
                return null;
            }

            List<BaseRuleAvailableDatesSelector> verifiedDatesSelectors = new List<BaseRuleAvailableDatesSelector>();

            foreach (BaseRuleAvailableDatesSelector op in operators)
            {
                if (op.ContainsDate(date))
                {
                    verifiedDatesSelectors.Add(op);
                }
            }

            List<RruleTimes> times = new List<RruleTimes>();
            int duration = int.MaxValue;

            // get times by selectors
            foreach (BaseRuleAvailableDatesSelector selector in verifiedDatesSelectors.OrderBy(x=>x.Duration))
            {
                if (selector.ContainsDate(date) && selector.Duration <= duration)
                {
                    times.Add(new RruleTimes(selector.StartTime, selector.EndTime));
                    duration = selector.Duration;
                }
            }

            return times;
        }

        private IReadOnlyList<BaseRuleAvailableDatesSelector> GetRuleOperators(IReadOnlyList<AvailabilityDataModel> models)
        {
            List<BaseRuleAvailableDatesSelector> operators = new List<BaseRuleAvailableDatesSelector>();

            foreach (AvailabilityDataModel model in models)
            {
                BaseRuleAvailableDatesSelector op = RuleAvailableDatesSelectorFactory.GetRruleOperator(model.StartDate,
                                                                                                       model.EndDate,
                                                                                                       model.StartTime,
                                                                                                       model.EndTime,
                                                                                                       model.Rule);
                if (op != null)
                {
                    operators.Add(op);
                }
            }

            return operators;
        }
    }
}
