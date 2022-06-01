using Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace Infrastructure.Implementation
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HttpService> _logger;

        public HttpService(HttpClient httpClient, ILogger<HttpService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<T> GetDataFromAPIAsync<T>(string requestUri)
        {
            try
            {
                var response = await _httpClient.GetAsync(requestUri);
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return default;
                }
                response.EnsureSuccessStatusCode();
                var body = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
                return body;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}