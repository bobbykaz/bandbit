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
            //var token = client.Authenticate(code, Secrets.CallbackURI).ConfigureAwait(false).GetAwaiter().GetResult();
            var token = client.Authenticate(Secrets.TestRefreshToken).ConfigureAwait(false).GetAwaiter().GetResult();

            Console.WriteLine("Done");

        }
    }
}
