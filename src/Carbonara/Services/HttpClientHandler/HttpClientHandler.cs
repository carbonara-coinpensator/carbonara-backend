using System.Net.Http;
using System.Threading.Tasks;

namespace Carbonara.Services.HttpClientHandler
{
    public class HttpClientHandler : IHttpClientHandler
    {
        private readonly HttpClient _httpClient = new HttpClient();
        
        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await _httpClient.GetAsync(url);
        }
    }
}