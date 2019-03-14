using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Carbonara.Models.PoolHashRateDistribution;
using Newtonsoft.Json;

namespace Carbonara.Providers.PoolHashRateProvider
{
    public class PoolHashRateProvider : IPoolHashRateProvider
    {
        private readonly string _path;

        public PoolHashRateProvider()
        {
            _path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        }

        public async Task<ICollection<Pool>> GetDistributionBasedOnDateAsync(DateTime date)
        {
            var distribution = await ReadDistributionFromFile($"{_path}/PoolHashRateDistribution.json");

            var yearPeriod = date.Month <= 6 ? 1 : 2;

            var txDistribution = distribution.Distribution.First(d => d.Year == date.Year && d.YearPeriod == yearPeriod);

            return txDistribution.PoolInformation;
        }

        private async Task<PoolHashRateDistribution> ReadDistributionFromFile(string filename)
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                var json = await reader.ReadToEndAsync();
                return JsonConvert.DeserializeObject<PoolHashRateDistribution>(json);
            }
        }
    }
}