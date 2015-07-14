using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fitbit.API.Model.Common.MeasurementSystems;
using Fitbit.API.Model.Common.Enums.UnitSystems;
using Fitbit.API.Model.Common.Enums.Profile;
using Newtonsoft.Json;


namespace Fitbit.API.Model.User
{
    public class PutUserRequest
    {
        public string fullName { get; set; }
        public string displayName { get; set; }
        public string nickname { get; set; }
        public Gender? gender { get; set; }
        public DateTime? dateOfBirth { get; set; }
        public string aboutMe { get; set; }

        public string city { get; set; }
        public string country { get; set; }
        public string state { get; set; }

        public DayOfWeek? startDayOfWeek { get; set; }

        public decimal? height { get; set; }
        public decimal? weight { get; set; }
        public int? strideLengthRunning { get; set; }
        public int? strideLengthWalking { get; set; }

        public Locale? locale { get; set; }
        public UnitSystem? distanceUnit { get; set; }
        public UnitSystem? heightUnit { get; set; }
        public UnitSystem? weightUnit { get; set; }
        public UnitSystem? waterUnit { get; set; }
        public UnitSystem? glucoseUnit { get; set; }
    }
}
