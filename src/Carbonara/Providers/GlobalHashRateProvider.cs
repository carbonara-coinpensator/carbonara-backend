using Carbonara.Models;
using Carbonara.Providers.BaseProviders;
using Carbonara.Services.HttpClientHandler;
using System.Threading.Tasks;

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