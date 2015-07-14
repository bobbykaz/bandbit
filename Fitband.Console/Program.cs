using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitBand.Developer.Secrets;
using System.IO;
using Fitbit.API.Model.Activities;

namespace Fitband.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Fitbit.API.Client.FitbitClient client = new Fitbit.API.Client.FitbitClient(Secrets.ClientID, Secrets.ClientConsumerSecret, Secrets.CallbackURI);
            client.SetBearerAuthorizationHeader(Secrets.TestAccessToken);

            DateTime sampleDay = new DateTime(2015, 6, 30);

            var availableActivities = client.GetAvailableActivities().ConfigureAwait(false).GetAwaiter().GetResult();

            var walkActivity = availableActivities.categories.Where(c => c.name == "Walking").FirstOrDefault();
            int targetId = walkActivity.id;

            PostActivityRequest newAct = new PostActivityRequest()
            {
                activityId = 17151,//90013,
                distance = 100,
                distanceUnit = PostActivityRequest.DistanceUnit.Steps,
                durationMillis = 120000,
                startTime = DateTime.Now
            };

            var newAccLog = client.CreateActivityLog(newAct).ConfigureAwait(false).GetAwaiter().GetResult();

            var myActivites = client.GetUserActivityLogs(sampleDay).ConfigureAwait(false).GetAwaiter().GetResult();

            var user = client.GetUser().ConfigureAwait(false).GetAwaiter().GetResult();


            Console.WriteLine("Done");
        }
    }
}
