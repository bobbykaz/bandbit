using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitbit.API.Model.Activities
{
    public class GetActivitiesResponse
    {
        public List<ActivityCategory> categories { get; set; }

    }

    public class GetUserActivityLogsResponse
    {
        public List<ActivityLog> activities { get; set; }
        public Goals goals { get; set; }
    }
}
