using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Fitbit.API.Model.Activities
{
    public class PostActivityRequest
    {
        public int activityId { get; set; }
        public string activityName { get; set; }
        public int manualCalories { get; set; }

        [JsonIgnore]
        public DateTime startTime { get; set; }
        [JsonProperty("startTime")]
        public string stringTime { 
            get 
            {
                string hh = startTime.Hour.ToString();
                if (startTime.Hour < 10)
                    hh = "0" + hh;

                string mm = startTime.Minute.ToString();
                if (startTime.Minute < 10)
                    mm = "0" + mm;

                return hh + ":" + mm;
            } 
        }

        public int durationMillis { get; set; }

        [JsonIgnore]
        public decimal distance { get; set; }
        [JsonProperty("distance")]
        public string distanceString {
            get
            {
                return string.Format("{0:C}", distance);
            }
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public DistanceUnit distanceUnit { get; set; }

       public enum DistanceUnit
       {
           Steps,
           Miles,
           Kilometers
       }
    }
}
