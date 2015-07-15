using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitbit.API.Model.Activities
{
    public class ActivityCategory
    {
        public string name { get; set; }
        public int id { get; set; }
        public List<Activity> activities { get; set; }
        public List<ActivityCategory> subCategories { get; set; }
    }

    public class ActivityLevel
    {
        public int id { get; set; }
        public string name { get; set; }
        public double minSpeedMPH { get; set; }
        public double maxSpeedMPH { get; set; }
        public double mets { get; set; }
    }

    public class Activity
    {
        public int id { get; set; }
        public int activityParentId { get; set; }
        public bool hasSpeed { get; set; }
        public List<ActivityLevel> activityLevels { get; set; }
        public string name { get; set; }
        public int steps { get; set; }
    }

    public class ActivityLog
    {
        public int activityId { get; set; }
        public int activityParentId { get; set; }
        public int calories { get; set; }
        public double distance { get; set; }
        public string description { get; set; }
        public int duration { get; set; }
        public bool hasStartTime { get; set; }
        public bool hasSpeed { get; set; }
        public List<ActivityLevel> activityLevels { get; set; }
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
}
