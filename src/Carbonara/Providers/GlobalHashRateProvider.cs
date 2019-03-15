using System.Threading.Tasks;
using Carbonara.Models;
using Carbonara.Services;
using Carbonara.Services.HttpClientHandler;
using Newtonsoft.Json;

namespace Carbonara.Providers
{
    public class GlobalHashRateProvider : BaseHttpProvider, IGlobalHashRateProvider
    {
        protected override string Endpoint => "https://api.blockchain.info/charts/hash-rate";

        public GlobalHashRateProvider(IHttpClientHandler httpClient)
            : base(httpClient)
        {
        }

        public async Task<decimal> GetDailyHashRateAsync(int numberOfDays)
        {
            var url = $"{Endpoint}?timespan={numberOfDays}days&format=json";
            var hashRate = await GetResponseAndDeserialize<GlobalHashRate>(url);

            return (decimal)hashRate.values[0].y;
        }
    }
}