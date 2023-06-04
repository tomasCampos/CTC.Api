using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace CTC.Integration.Test.Shared
{
    internal abstract class IntegrationTestBase
    {
        private readonly HttpClient _httpClient;
        private const string CTC_API_BASE_ADDRESS = "https://ctc-api.up.railway.app";

        protected IntegrationTestBase()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(CTC_API_BASE_ADDRESS)
            };

            _httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
        }

        protected async Task<(HttpStatusCode statusCode, T result)> MakeGetRequest<T>(string requestUri) 
        {
            var response = await _httpClient.GetAsync(requestUri);

            var result = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());

            response.EnsureSuccessStatusCode();
            return (response.StatusCode, result);
        }

        protected async Task<HttpStatusCode> MakePostRequest(string requestUri, object? requestBody = null)
        {
            var contentString = BuildHttpRequestStringContent(requestBody);
            var response = await _httpClient.PostAsync(requestUri, contentString);

            response.EnsureSuccessStatusCode();
            return response.StatusCode;
        }

        protected async Task<HttpStatusCode> MakePutRequest(string requestUri, object? requestBody = null)
        {
            var contentString = BuildHttpRequestStringContent(requestBody);
            var response = await _httpClient.PutAsync(requestUri, contentString);

            response.EnsureSuccessStatusCode();
            return response.StatusCode;
        }

        protected async Task<HttpStatusCode> MakeDeleteRequest(string requestUri)
        {
            var response = await _httpClient.DeleteAsync(requestUri);

            response.EnsureSuccessStatusCode();
            return response.StatusCode;
        }

        private async Task<string> GetAuthToken()
        {
            var stringContent = BuildHttpRequestStringContent(new { userEmail = "integration.test@gmail.com", userPassword = "1234567" });
            var response = await _httpClient.PostAsync("/User/Autorize", stringContent);
        }

        private static StringContent BuildHttpRequestStringContent(object? requestBody)
        {
            var jsonContent = JsonConvert.SerializeObject(requestBody);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            return contentString;
        }
    }
}
