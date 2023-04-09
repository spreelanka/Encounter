using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EncounterMobile.NetworkPolicies;
using Newtonsoft.Json;
using Polly;
using Polly.Registry;

namespace EncounterMobile.Services
{
    public abstract class BaseApiService
    {
        protected abstract string BaseUri { get; }//=> "https://example.com" + "/api/";

            
        protected HttpClient Client
        {
            get
            {
                var client = new HttpClient(messageHandler);
                client.BaseAddress = new Uri(BaseUri);
                //client.DefaultRequestHeaders.Add(MobileConstants.ApiConstants.FacebookHeaderAccessTokenKey, _authenticationService.FacebookAccessToken);
                //client.DefaultRequestHeaders.Add(MobileConstants.ApiConstants.UserIdHeaderAccessTokenKey, "test");
                client.MaxResponseContentBufferSize = 256000;
                return client;
            }
        }

        private HttpMessageHandler messageHandler { get; set; }
        private AsyncPolicy policy { get; set; }

        public BaseApiService(HttpMessageHandler messageHandler, IReadOnlyPolicyRegistry<string> policyRegistry)
        {
            this.messageHandler = messageHandler;
            policy = policyRegistry.Get<AsyncPolicy>(PolicyNames.DefaultPolicy);
        }

        public async Task<TResponse> Get<TResponse>(string relativePath)
        {
            var response = await policy.ExecuteAsync(async (context) =>
            {
                return await Client.GetAsync(relativePath);
            }, new Context(relativePath));

            if (!response.IsSuccessStatusCode)
            {
                System.Diagnostics.Debug.WriteLine("Response Code: " + (int)response.StatusCode + " - " + response.StatusCode.ToString());
                return default(TResponse);
            }

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TResponse>(content);
            return result;
        }

        public async Task<System.Net.HttpStatusCode> Put<TRequest>(string relativePath, TRequest request)
        {
            var json = JsonConvert.SerializeObject(request, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var putContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PutAsync(relativePath, putContent);
            var content = await response.Content.ReadAsStringAsync();
            return response.StatusCode;
        }

        public async Task<TResponse> Post<TResponse, TRequest>(string relativePath, TRequest request) where TRequest : class
        {
            string json = string.Empty;

            if (request != null)
            {
                json = JsonConvert.SerializeObject(request, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            }

            var putContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(relativePath, putContent);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TResponse>(content);
            return result;
        }
    }
}
