using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Fitbit.API.Model.Sleep
{
    public class LogSleepRequest
    {
        public int duration { get; set; }

        [JsonIgnore]
        public DateTime startTime { get; set; }

        [JsonProperty("startTime")]
        public string startTimeString { get { return string.Format("{0:HH:mm}", startTime);} }

        [JsonIgnore]
        public DateTime date {get; set;}

        [JsonProperty("date")]
        public string dateString { get { return string.Format("{0:yyyy-MM-dd}", date);} }
    }

    public class LogSleepResponse
    {
        public Sleep sleep { get; set; }
    }
}
