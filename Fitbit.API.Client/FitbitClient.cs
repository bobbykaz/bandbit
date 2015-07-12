using Fitbit.API.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Fitbit.API.Client
{
    public partial class FitbitClient : BaseClient
    {
        private string ClientID { get; set; }
        private string ClientSecret { get; set; }
        private string CallbackURI { get; set; }

        public FitbitClient(string clientId, string clientSecret, string callback)
            : base("https://api.fitbit.com/")
        {
            ClientID = clientId;
            ClientSecret = clientSecret;
        }

        public async Task<AuthTokenResponse> Authenticate(string authCode, string redirectUri)
        {
            string query = "oauth2/token";
            Dictionary<string, string> authFlow = new Dictionary<string, string>();
            authFlow["code"] = authCode;
            authFlow["grant_type"] = "authorization_code";
            authFlow["client_id"] = ClientID;
            authFlow["redirect_uri"] = redirectUri;

            SetBasicAuthorizationHeader();

            try
            {
                HttpContent content = new FormUrlEncodedContent(authFlow);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");

                var response = await Client.PostAsync(query, content).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                if (response.Content != null)
                {
                    string resultJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    AuthTokenResponse result = JsonConvert.DeserializeObject<AuthTokenResponse>(resultJson);
                    SetBearerAuthorizationHeader(result.access_token);
                    return result;
                }
                else
                    throw new Exception(string.Format("No content for PostAsync {0}", query));

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<AuthTokenResponse> Authenticate(string refreshToken)
        {
            string query = "oauth2/token";
            Dictionary<string, string> authFlow = new Dictionary<string, string>();
            authFlow["grant_type"] = "refresh_token";
            authFlow["refresh_token"] = refreshToken;

            SetBasicAuthorizationHeader();

            try
            {
                HttpContent content = new FormUrlEncodedContent(authFlow);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");

                var response = await Client.PostAsync(query, content).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                if (response.Content != null)
                {
                    string resultJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    AuthTokenResponse result = JsonConvert.DeserializeObject<AuthTokenResponse>(resultJson);
                    SetBearerAuthorizationHeader(result.access_token);
                    return result;
                }
                else
                    throw new Exception(string.Format("No content for PostAsync {0}", query));

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SetBasicAuthorizationHeader()
        {
            string baseHeaderVal = ClientID + ":" + ClientSecret;
            string b64Encode = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(baseHeaderVal));
            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", b64Encode);
        }

        public void SetBearerAuthorizationHeader(string bearerToken)
        {
            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);
        }
    }

    public class BaseClient
    {
        protected HttpClient Client { get; set; }
        private string AuthToken { get; set; }

        public BaseClient(string baseUrl)
        {
            Client = new HttpClient() { BaseAddress = new Uri(baseUrl) };
        }

        public void SetAuthToken(string authToken, string tokenType)
        {
            AuthToken = authToken;
            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(tokenType, AuthToken);
        }

        protected async Task<TResult> GetAsync<TResult>(string query)
        {
            try
            {
                var result = await Client.GetAsync(query).ConfigureAwait(false);
                result.EnsureSuccessStatusCode();

                string content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                TResult getResult = JsonConvert.DeserializeObject<TResult>(content);
                return getResult;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        protected async Task PostAsync<TRequest>(string query, TRequest request)
        {
            try
            {
                string json = JsonConvert.SerializeObject(request);
                HttpContent content = new StringContent(json);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                var result = await Client.PostAsync(query, content).ConfigureAwait(false);
                result.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        protected async Task<TResult> PostAsync<TRequest, TResult>(string query, TRequest request)
        {
            try
            {
                string json = JsonConvert.SerializeObject(request);
                HttpContent content = new StringContent(json);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                var response = await Client.PostAsync(query, content).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                if (response.Content != null)
                {
                    string resultJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    TResult result = JsonConvert.DeserializeObject<TResult>(resultJson);
                    return result;
                }
                else
                    throw new Exception(string.Format("No content for PostAsync {0}", query));

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
