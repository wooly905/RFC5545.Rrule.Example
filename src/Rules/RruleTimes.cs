using System;

namespace RruleTool.Rules
{
    public class RruleTimes
    {
        private readonly DateTime _startTime;
        private readonly DateTime _endTime;

        public RruleTimes(DateTime startTime, DateTime endTime)
        {
            _startTime = startTime;
            _endTime = endTime;
        }

        public override string ToString()
        {
            return $"{_startTime.ToString("HH:mm")}-{_endTime.ToString("HH:mm")}";
        }
    }
}
