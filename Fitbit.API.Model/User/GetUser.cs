using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fitbit.API.Model.Common.MeasurementSystems;
using Fitbit.API.Model.Common.Enums.UnitSystems;
using Fitbit.API.Model.Common.Enums.Profile;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Fitbit.API.Model.User
{
    public class User
    {
        public string fullName { get; set; }
        public string displayName { get; set; }
        public string nickname { get; set; }

        [JsonConverter(typeof(StringEnumConverter))] 
        public Gender gender { get; set; }
        
        public DateTime dateOfBirth { get; set; }
        public string aboutMe { get; set; }

        public string encodedId { get; set; }

        public string avatar { get; set; }
        public string avatar150 { get; set; }
        
        public string city { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string timezone { get; set; }
        public int offsetFromUTCMillis { get; set; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        public DayOfWeek startDayOfWeek { get; set; }

        public DateTime memberSince { get; set; }

        public double height { get; set; }
        public double weight { get; set; }
        public double strideLengthRunning { get; set; }
        public double strideLengthWalking { get; set; }

        [JsonConverter(typeof(StringEnumConverter))] 
        public Locale? locale { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Locale? distanceUnit { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Locale? heightUnit { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Locale? weightUnit { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Locale? waterUnit { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Locale? glucoseUnit { get; set; }
    }

    public class GetUserResponse
    {
        public User user { get; set; }
    }
}
