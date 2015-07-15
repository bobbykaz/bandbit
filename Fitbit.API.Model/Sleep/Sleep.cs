using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitbit.API.Model.Sleep
{
    public class Sleep
    {
        public bool isMainSleep { get; set; }
        public int logId { get; set; }
        public int efficiency { get; set; }
        public DateTime startTime { get; set; }
        public int duration { get; set; }
        public int minutesToFallAsleep { get; set; }
        public int minutesAsleep { get; set; }
        public int minutesAwake { get; set; }
        public int minutesAfterWakeup { get; set; }
        public int awakeCount { get; set; }
        public int awakeDuration { get; set; }
        public int restlessCount { get; set; }
        public int restlessDuration { get; set; }
        public int timeInBed { get; set; }
        public List<MinuteData> minuteData { get; set; }

    }

    public class MinuteData
    {
        public DateTime dateTime { get; set; }
        public int value { get; set; }
    }

    public class Summary 
    {
        public int totalSleepRecords { get; set; }
        public int totalMinutesAsleep { get; set; }
        public int totalTimeInBed { get; set; }
    }
}
