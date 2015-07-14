using Fitbit.API.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitbit.API.Client
{
    public partial class FitbitClient : BaseClient
    {
        public async Task<GetUserResponse> GetUser()
        {
            string query = "/1/user/-/profile.json";
            return await GetAsync<GetUserResponse>(query);
        }

    }
}
