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
        private readonly ISimpleDataCache _dataCache;
        private readonly IDataAccessRepository _repository;
        private readonly string _cacheKey = "demoUserKey";

        public RruleService(ISimpleDataCache dataCache, IDataAccessRepository repository)
        {
            _dataCache = dataCache;
            _repository = repository;
        }

        public async Task<IReadOnlyList<RruleTimes>> GetAvailableTimesAsync(DateTime date)
        {
            // get data from database or cache
            IReadOnlyList<AvailabilityDataModel> models = await GetDataFromCacheOrRepositoryAsync();

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

        private async Task<IReadOnlyList<AvailabilityDataModel>> GetDataFromCacheOrRepositoryAsync()
        {
            if (TryGetFromCache(_cacheKey, out IReadOnlyList<AvailabilityDataModel> result))
            {
                return result;
            }

            result = await _repository.GetAvailabilitiesAsync();
            _dataCache.Set(_cacheKey, result);

            return result;
        }

        private bool TryGetFromCache(string key, out IReadOnlyList<AvailabilityDataModel> result)
        {
            if (_dataCache.TryGet(key, out object value)
                && value is IReadOnlyList<AvailabilityDataModel> data)
            {
                result = data;
                return true;
            }

            result = null;
            return false;
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
