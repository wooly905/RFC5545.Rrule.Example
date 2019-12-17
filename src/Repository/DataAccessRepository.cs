using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RruleTool.Abstractions;
using RruleTool.Models;

namespace RruleTool.Repository
{
    internal class DataAccessRepository : IDataAccessRepository
    {
        public Task<IReadOnlyList<AvailabilityDataModel>> GetAvailabilitiesAsync()
        {
            // This should be some action to read data from database.
            // In this example, we don't have database, so let's assume we can return some example data from this function.

            //1   01.01.2019  31.12.2019  10:00       13:00       FREQ = WEEKLY; INTERVAL = 1; WKST = MO; BYDAY = MO,TU,TH
            //2   01.01.2019  31.12.2019  12:00       16:00       FREQ = WEEKLY; INTERVAL = 1; WKST = MO; BYDAY = FR
            //3   01.01.2019  31.12.2019  14:00       17:00       FREQ = WEEKLY; INTERVAL = 1; WKST = MO; BYDAY = TH
            //4   01.07.2019  31.07.2019  10:00       19:00       FREQ = WEEKLY; INTERVAL = 1; WKST = MO; BYDAY = MO,TU
            //5   18.07.2019  18.07.2019  15:00       19:00       FREQ = WEEKLY; INTERVAL = 1; WKST = MO; BYDAY = TH

            List<AvailabilityDataModel> models = new List<AvailabilityDataModel>();

            AvailabilityDataModel model1 = new AvailabilityDataModel();
            model1.EndDate = new DateTime(2019, 12, 31);
            model1.EndTime = new DateTime(2019, 12, 31, 13, 0, 0);  // dummmy year,month,day
            model1.Rule = "FREQ = WEEKLY; INTERVAL = 1; WKST = MO; BYDAY = MO,TU,TH";
            model1.StartDate = new DateTime(2019, 1, 1);
            model1.StartTime = new DateTime(2019, 1, 1, 10, 0, 0);  // dummy year,month,day
            models.Add(model1);

            AvailabilityDataModel model2 = new AvailabilityDataModel();
            model2.EndDate = new DateTime(2019, 12, 31);
            model2.EndTime = new DateTime(2019, 12, 31, 16, 0, 0);  // dummmy year,month,day
            model2.Rule = "FREQ = WEEKLY; INTERVAL = 1; WKST = MO; BYDAY = FR";
            model2.StartDate = new DateTime(2019, 1, 1);
            model2.StartTime = new DateTime(2019, 1, 1, 12, 0, 0);  // dummy year,month,day
            models.Add(model2);

            AvailabilityDataModel model3 = new AvailabilityDataModel();
            model3.EndDate = new DateTime(2019, 12, 31);
            model3.EndTime = new DateTime(2019, 12, 31, 17, 0, 0);  // dummmy year,month,day
            model3.Rule = "FREQ = WEEKLY; INTERVAL = 1; WKST = MO; BYDAY = TH";
            model3.StartDate = new DateTime(2019, 1, 1);
            model3.StartTime = new DateTime(2019, 1, 1, 14, 0, 0);  // dummy year,month,day
            models.Add(model3);

            AvailabilityDataModel model4 = new AvailabilityDataModel();
            model4.EndDate = new DateTime(2019, 7, 31);
            model4.EndTime = new DateTime(2019, 7, 31, 19, 0, 0);  // dummmy year,month,day
            model4.Rule = "FREQ = WEEKLY; INTERVAL = 1; WKST = MO; BYDAY = MO,TU";
            model4.StartDate = new DateTime(2019, 7, 1);
            model4.StartTime = new DateTime(2019, 7, 1, 10, 0, 0);  // dummy year,month,day
            models.Add(model4);

            AvailabilityDataModel model5 = new AvailabilityDataModel();
            model5.EndDate = new DateTime(2019, 7, 18);
            model5.EndTime = new DateTime(2019, 7, 18, 19, 0, 0);  // dummmy year,month,day
            model5.Rule = "FREQ = WEEKLY; INTERVAL = 1; WKST = MO; BYDAY = TH";
            model5.StartDate = new DateTime(2019, 7, 18);
            model5.StartTime = new DateTime(2019, 7, 18, 15, 0, 0);  // dummy year,month,day
            models.Add(model5);

            return Task.FromResult<IReadOnlyList<AvailabilityDataModel>>(models);
        }
    }
}
