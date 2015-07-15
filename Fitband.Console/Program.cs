using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitBand.Developer.Secrets;
using System.IO;
using Fitbit.API.Model.Activities;
using Fitbit.API.Model.Common.Enums.Activities;
using Fitbit.API.Model.Sleep;

namespace Fitband.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TestSleep();
            Console.WriteLine("Done");
        }

        public static void TestSleep()
        {
            Fitbit.API.Client.FitbitClient client = new Fitbit.API.Client.FitbitClient(Secrets.ClientID, Secrets.ClientConsumerSecret, Secrets.CallbackURI);
            client.SetBearerAuthorizationHeader(Secrets.TestAccessToken);
            DateTime sampleDay = DateTime.Now;

            LogSleepRequest req = new LogSleepRequest(){startTime = DateTime.Now, date = DateTime.Now, duration = 600000};

            var sleepLog = client.LogSleep(req).ConfigureAwait(false).GetAwaiter().GetResult();

            var sleeps = client.GetSleep(req.date).ConfigureAwait(false).GetAwaiter().GetResult();

            client.DeleteSleep(sleepLog.sleep.logId).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public static void TestActivity()
        {
            Fitbit.API.Client.FitbitClient client = new Fitbit.API.Client.FitbitClient(Secrets.ClientID, Secrets.ClientConsumerSecret, Secrets.CallbackURI);
            client.SetBearerAuthorizationHeader(Secrets.TestAccessToken);

            DateTime sampleDay = DateTime.Now;

            var availableActivities = client.GetAvailableActivities().ConfigureAwait(false).GetAwaiter().GetResult();

            var walkActivity = availableActivities.categories.Where(c => c.name == "Walking").FirstOrDefault();
            int targetId = walkActivity.id;

            PostActivityRequest newAct = new PostActivityRequest()
            {
                activityId = 17151,//90013,
                distance = 98,
                distanceUnit = DistanceUnit.Steps,
                durationMillis = 120000,
                startTime = DateTime.Now
            };

            var myActivites = client.GetUserActivityLogs(sampleDay).ConfigureAwait(false).GetAwaiter().GetResult();

            var updatedLog = client.UpdateActivityLog(myActivites.activities.FirstOrDefault().logId, newAct).ConfigureAwait(false).GetAwaiter().GetResult();

            client.DeleteActivityLog(updatedLog.activityLog.logId).ConfigureAwait(false).GetAwaiter().GetResult();

            var newAccLog = client.CreateActivityLog(newAct).ConfigureAwait(false).GetAwaiter().GetResult();

            client.DeleteActivityLog(newAccLog.activityLog.logId).ConfigureAwait(false).GetAwaiter().GetResult();

            var user = client.GetUser().ConfigureAwait(false).GetAwaiter().GetResult();

        }

    }
}
