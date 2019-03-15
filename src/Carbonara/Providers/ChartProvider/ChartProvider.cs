using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Carbonara.Models.Chart;
using Newtonsoft.Json;

namespace Carbonara.Providers.ChartProvider
{
    public class ChartProvider : IChartProvider
    {
        public async Task<List<ChartValue>> GetPriceChartAsync()
        {
            var chart = await ReadDevicesFromFile($"BitcoinPriceChart.json");
            return chart;
        }

        public async Task<List<ChartValue>> GetCo2EmissionChartAsync()
        {
            var chart = await ReadDevicesFromFile($"BitcoinCo2EmissionChart.json");
            return chart;
        }

        private async Task<List<ChartValue>> ReadDevicesFromFile(string filename)
        {
            var _path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            using (StreamReader reader = new StreamReader($"{_path}/{filename}"))
            {
                var json = await reader.ReadToEndAsync();
                return JsonConvert.DeserializeObject<List<ChartValue>>(json);
            }
        }
    }
}