using System.Collections.Generic;
using System.Threading.Tasks;
using Carbonara.Models.Chart;

namespace Carbonara.Providers.ChartProvider
{
    public interface IChartProvider
    {
        Task<List<ChartValue>> GetPriceChartAsync();
        Task<List<ChartValue>> GetEnergyConsumptionChartAsync();
    }
}