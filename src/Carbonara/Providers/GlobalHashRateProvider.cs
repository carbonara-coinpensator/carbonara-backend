using System.Threading.Tasks;
using Carbonara.Models;
using Carbonara.Services;
using Carbonara.Services.HttpClientHandler;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Carbonara.Providers
{
    public class GlobalHashRateProvider : IGlobalHashRateProvider
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientHandler _httpClient;

        public GlobalHashRateProvider(IConfiguration configuration, IHttpClientHandler httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<decimal> GetDailyHashRateAsync(int numberOfDays)
        {
            var url = $"{_configuration["Api:GlobalHashRate"]}?timespan={numberOfDays}days&format=json";
            var responseContent = await GetResponseContent(url);

            var hashRate = JsonConvert.DeserializeObject<GlobalHashRate>(responseContent);

            var hashRateOfFirstDayInPeriod = hashRate.values[0].y;

            return (decimal) hashRateOfFirstDayInPeriod;
        }

        private async Task<string> GetResponseContent(string url)
        {
            var response = await _httpClient.GetAsync(url);

            var responseContent = await response.Content.ReadAsStringAsync();
            return responseContent;
        }
    }
}