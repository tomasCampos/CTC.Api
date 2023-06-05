using CTC.Integration.Test.Shared.Dtos;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace CTC.Integration.Test.Shared
{
    public abstract class IntegrationTestBase
    {
        private readonly HttpClient _httpClient;
        private const string CTC_API_BASE_ADDRESS = "https://ctc-api.up.railway.app";

        protected IntegrationTestBase()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(CTC_API_BASE_ADDRESS)
            };

            var bearerToken = GetAuthToken().Result;
            _httpClient.DefaultRequestHeaders.Add("Authorization", bearerToken);
        }

        protected async Task<HttpResponseDto<T>> MakeGetRequest<T>(string requestUri) 
        {
            var response = await _httpClient.GetAsync(requestUri);

            var responseBody = JsonConvert.DeserializeObject<HttpResponseDto<T>>(await response.Content.ReadAsStringAsync());

            return responseBody;
        }

        protected async Task<HttpStatusCode> MakePostRequest(string requestUri, object? requestBody = null)
        {
            var contentString = BuildHttpRequestStringContent(requestBody);
            var response = await _httpClient.PostAsync(requestUri, contentString);

            return response.StatusCode;
        }

        protected async Task<HttpStatusCode> MakePutRequest(string requestUri, object? requestBody = null)
        {
            var contentString = BuildHttpRequestStringContent(requestBody);
            var response = await _httpClient.PutAsync(requestUri, contentString);

            return response.StatusCode;
        }

        protected async Task<HttpStatusCode> MakeDeleteRequest(string requestUri)
        {
            var response = await _httpClient.DeleteAsync(requestUri);

            return response.StatusCode;
        }

        private async Task<string> GetAuthToken()
        {
            var stringContent = BuildHttpRequestStringContent(new { userEmail = "integration.test@gmail.com", userPassword = "1234567" });
            var response = await _httpClient.PostAsync("/User/Authorize", stringContent);

            var result = JsonConvert.DeserializeObject<HttpResponseDto<AuthorizationDto>>(await response.Content.ReadAsStringAsync());

            return $"Bearer {result!.Body!.BearerToken}";
        }

        private static StringContent BuildHttpRequestStringContent(object? requestBody)
        {
            var jsonContent = JsonConvert.SerializeObject(requestBody);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            return contentString;
        }
    }
}
