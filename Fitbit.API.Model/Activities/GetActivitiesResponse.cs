using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitbit.API.Model.Activities
{
    public class Activity
    {
        public int activityId { get; set; }
        public int activityPArentId { get; set; }
        public int calories { get; set; }
        public double distance { get; set; }
        public string description { get; set; }
        public int duration { get; set; }
        public bool hasStartTime { get; set; }
        public bool isFavorite { get; set; }
        public int logId { get; set; }
        public string name { get; set; }
        public string startTime { get; set; }
        public int steps { get; set; }
    }

    public class Goals
    { 
        public int caloriesOut { get; set; }
        public double distance { get; set; }
        public int floors { get; set; }
        public int steps { get; set; }
    }

    public class Summary 
    { 
    
    }

    public class GetActivitiesResponse
    {
        List<Activity> Activities { get; set; }
        Goals goals { get; set; }
        public string summary { get; set; }
    }
}
