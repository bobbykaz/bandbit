using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fitbit.API.Model.Sleep;

namespace Fitbit.API.Client
{
    public partial class FitbitClient : BaseClient
    {
        public async Task<GetSleepResponse> GetSleep(DateTime date)
        {
            string query = string.Format("/1/user/-/sleep/date/{0:yyyy-MM-dd}.json", date);
            return await GetAsync<GetSleepResponse>(query);
        }

        public async Task<LogSleepResponse> LogSleep(LogSleepRequest request)
        {
            string query = "1/user/-/sleep.json?";
            string queryParams = SerializeToQueryString<LogSleepRequest>(request);
            return await PostAsync<LogSleepResponse>(query+queryParams);
        }

        public async Task DeleteSleep(int sleepLogId)
        {
            string query = string.Format("/1/user/-/sleep/{0}.json", sleepLogId);
            await DeleteAsync(query);
        }
    }
}
