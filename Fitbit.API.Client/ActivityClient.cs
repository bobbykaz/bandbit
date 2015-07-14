using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fitbit.API.Model.Activities;
using Newtonsoft.Json;

namespace Fitbit.API.Client
{
    public partial class FitbitClient : BaseClient
    {
        public async Task<GetActivitiesResponse> GetAvailableActivities()
        { 
            string query = "/1/activities.json";
            return await GetAsync<GetActivitiesResponse>(query);
        }

        public async Task<GetUserActivityLogsResponse> GetUserActivityLogs(DateTime date)
        {
            string year = date.Year.ToString();
            string month = date.Month < 10 ? "0" + date.Month.ToString() : date.Month.ToString();
            string day = date.Day < 10 ? "0" + date.Day.ToString() : date.Day.ToString();
            string query = string.Format("1/user/-/activities/date/{0}-{1}-{2}.json", year, month, day);
            return await GetAsync<GetUserActivityLogsResponse>(query);
        }

        public async Task<ActivityLog> CreateActivityLog(PostActivityRequest createRequest)
        {
            string query = "1/user/-/activities.json?";

            string json = JsonConvert.SerializeObject(createRequest, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            Dictionary<string, string> dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            bool first = true;
            foreach(string key in dict.Keys)
            {
                if (!first)
                    query += "&";

                query += key + "=" + dict[key];
                
                first = false;
            }

            return await PostAsync<ActivityLog>(query);
        }


    }
}
