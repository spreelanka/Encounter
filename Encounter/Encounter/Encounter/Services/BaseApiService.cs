using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Encounter.Services
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

        public BaseApiService(HttpMessageHandler messageHandler)
        {
            this.messageHandler = messageHandler;
        }

        public async Task<TResponse> Get<TResponse>(string relativePath)
        {
            var response = await Client.GetAsync(relativePath);

            if (!response.IsSuccessStatusCode)
            {
                System.Diagnostics.Debug.WriteLine("Response Code: " + (int)response.StatusCode + " - " + response.StatusCode.ToString());
                return default(TResponse);
            }

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TResponse>(content);
            return result;
        }

        public async Task Put<TRequest>(string relativePath, TRequest request)
        {
            var json = JsonConvert.SerializeObject(request, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var putContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PutAsync(relativePath, putContent);
            var content = await response.Content.ReadAsStringAsync();
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
