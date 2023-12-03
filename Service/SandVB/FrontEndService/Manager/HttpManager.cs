using FrontEndService.Manager.Interface;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FrontEndService.Manager
{
    public class HttpManager : IHttpManager
    {
        public HttpClient _httpClient;
        public IConfiguration _config;
        //public IHttpContextAccessor _httpContextAccessor;
        private JsonSerializerOptions _jsonSerializerOptions;
        public ILogger<HttpManager> _logger;
        public HttpManager(IConfiguration config, ILogger<HttpManager> logger)
        {
            _httpClient = new HttpClient();
            _config = config;
            _logger = logger;

            _jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                AllowTrailingCommas = true
            };
            _jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        }

        public async Task<Q> MakePostAsync<T, Q>(string baseUrl, string endpoint, object body)
            where T : class
            where Q : class
        {
            var response = await _httpClient.PostAsJsonAsync(baseUrl + endpoint, body as T);
            return await HandleResponse<Q>(baseUrl + endpoint, response);
        }

        public async Task<T> MakePostAsync<T>(string baseUrl, string endpoint, object body) where T : class
        {
            return await MakePostAsync<T, T>(baseUrl, endpoint, body) as T;
        }
        public async Task<Q> MakePutAsync<T, Q>(string baseUrl, string endpoint, object body)
            where T : class
            where Q : class
        {
            var response = await _httpClient.PutAsJsonAsync(baseUrl + endpoint, body as T);
            return await HandleResponse<Q>(baseUrl + endpoint, response);
        }

        public async Task<T> MakePutAsync<T>(string baseUrl, string endpoint, object body)
            where T : class
        {
            return await MakePutAsync<T, T>(baseUrl, endpoint, body) as T;
        }

        public async Task<HttpStatusCode> MakeDelete(string baseUrl, string endpoint)
        {
            var response = await _httpClient.DeleteAsync(baseUrl + endpoint);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                await HandleError(response);
            }
            return response.StatusCode;
        }

        public async Task<T> MakeGetAsync<T>(string baseUrl, string endpoint) where T : class
        {
            HttpResponseMessage response = await _httpClient.GetAsync(baseUrl + endpoint);
            return await HandleResponse<T>(baseUrl + endpoint, response);
        }

        private async Task<T> HandleResponse<T>(string requestUri, HttpResponseMessage response) where T : class
        {
            // no content response
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return null;
            }

            // handle error responses
            if (response.StatusCode != HttpStatusCode.OK)
            {
                await HandleError(response);
            }

            // if HttpResponseMessage return type is specified, return the response so caller can access
            if (typeof(T) == typeof(HttpResponseMessage))
            {
                return response as T;
            }

            //Special cases that can't be converted to JSON
            if (typeof(T) == typeof(MemoryStream))
            {
                return await response.Content.ReadAsStreamAsync() as T;
            }

            if (typeof(T) == typeof(string) || typeof(T) == typeof(String))
            {
                return await response.Content.ReadAsStringAsync() as T;
            }

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions);
        }

        private async Task HandleError(HttpResponseMessage response)
        {
            var errorBody = await response.Content.ReadAsStringAsync();

            var ex = new Exception("Error calling SandVB service");

            throw ex;
        }


    }
}
