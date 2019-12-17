using System;

namespace RruleTool.Models
{
    public class AvailabilityDataModel
    {
        public DateTime EndDate { get; set; }

        public DateTime EndTime { get; set; }

        public string Rule { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime StartTime { get; set; }
    }
}
