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
        public int? activityId { get; set; }
        public string activityName { get; set; }
        public int? manualCalories { get; set; }

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
        [JsonProperty("date")]
        public string date {
            get 
            {
                string yr = startTime.Year.ToString();
                string mnth = startTime.Month.ToString();
                if (startTime.Month < 10)
                    mnth = "0" + mnth;

                string day = startTime.Day.ToString();
                if (startTime.Day < 10)
                    day = "0" + day;

                return yr + "-" + mnth + "-" + day;
            }
        }

        public int durationMillis { get; set; }

        [JsonIgnore]
        public decimal distance { get; set; }
        [JsonProperty("distance")]
        public string distanceString {
            get
            {
                return string.Format("{0:0.00}", distance);
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
