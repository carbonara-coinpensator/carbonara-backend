using System.Threading.Tasks;
using Carbonara.Models.Chart;

namespace Carbonara.Services.ChartService
{
    public interface IChartService
    {
        Task<BitcoinCharts> GetBitcoinChartsAsync()
    }
}