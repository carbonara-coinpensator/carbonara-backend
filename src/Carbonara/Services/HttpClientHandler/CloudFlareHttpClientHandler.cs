using System.Net.Http;
using System.Threading.Tasks;
using CloudflareSolverRe;

namespace Carbonara.Services.HttpClientHandler
{
    public class CloudFlareHttpClientHandler : ICloudFlareHttpClientHandler
    {
        private readonly HttpClient _httpClient;

        public CloudFlareHttpClientHandler()
        {
            var handler = new ClearanceHandler
            {
                MaxTries = 5
            };

            _httpClient = new HttpClient(handler);
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await _httpClient.GetAsync(url);
        }
    }
}