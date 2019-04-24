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
            var energyConsumptionChart = await _chartProvider.GetEnergyConsumptionChartAsync();

            return new BitcoinCharts
            {
                PriceChart = priceChart,
                EnergyConsumptionChart = energyConsumptionChart
            };
        }
    }
}