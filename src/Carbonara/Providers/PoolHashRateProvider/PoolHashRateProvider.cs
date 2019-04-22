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
    public class PoolHashRateProvider : BaseJsonFileProvider, IPoolHashRateProvider
    {
        public PoolHashRateProvider()
        {
        }

        public async Task<List<Pool>> GetDistributionBasedOnDateAsync(DateTime date)
        {
            var distribution = await ReadFromFileAndDeserialize<PoolHashRateDistribution>("HashRateDistribution.json");

            var yearPeriod = date.Month <= 6 ? 1 : 2;

            var txDistribution = distribution.Distribution.First(d => d.Year == date.Year && d.YearPeriod == yearPeriod);

            return txDistribution.PoolInformation.ToList();
        }
    }
}