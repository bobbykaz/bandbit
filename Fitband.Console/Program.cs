using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitBand.Developer.Secrets;
using System.IO;

namespace Fitband.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Fitbit.API.Client.FitbitClient client = new Fitbit.API.Client.FitbitClient(Secrets.ClientID, Secrets.ClientConsumerSecret, Secrets.CallbackURI);
            client.SetBearerAuthorizationHeader(Secrets.TestAccessToken);

            DateTime sampleDay = new DateTime(2015, 6, 30);
            var activites = client.GetActivities(sampleDay).ConfigureAwait(false).GetAwaiter().GetResult();

            var user = client.GetUser().ConfigureAwait(false).GetAwaiter().GetResult();


            Console.WriteLine("Done");
        }
    }
}
