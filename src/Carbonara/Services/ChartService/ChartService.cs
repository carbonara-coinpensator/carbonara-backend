using System.Threading.Tasks;
using Carbonara.Models.Chart;
using Carbonara.Providers.ChartProvider;

namespace Carbonara.Services.ChartService
{
    public class ChartService : IChartService
    {
        private readonly IChartProvider _chartProvider;

        public ChartService(IChartProvider chartProvider)
        {
            _chartProvider = chartProvider;    
        }

        public async Task<BitcoinCharts> GetBitcoinChartsAsync()
        {
            var priceChart = await _chartProvider.GetPriceChartAsync();
            var co2EmissionChart = await _chartProvider.GetCo2EmissionChartAsync();

            return new BitcoinCharts
            {
                PriceChart = priceChart,
                Co2EmissionChart = co2EmissionChart
            };
        }
    }
}