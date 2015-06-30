using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Fitbit.API.Model
{
    public class AuthCodeAuthRequest
    {
        public string code { get; set; }
        public string client_id { get; set; }
        public string redirect_uri { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public grant_type grant_type { get { return Model.grant_type.authorization_code; } }
    }

    public class RefreshTokenAuthRequest
    {
        public string refresh_token { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public grant_type grant_type { get { return Model.grant_type.refresh_token; } }
    }

    public enum grant_type
    {
        authorization_code,
        refresh_token
    }

    public class AuthTokenResponse 
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
    }
}
