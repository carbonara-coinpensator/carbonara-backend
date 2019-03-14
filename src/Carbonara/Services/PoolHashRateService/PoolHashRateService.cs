using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Carbonara.Models.PoolHashRateDistribution;
using Carbonara.Providers.PoolHashRateProvider;

namespace Carbonara.Services.PoolHashRateService
{
    public class PoolHashRateService : IPoolHashRateService
    {
        private readonly IPoolHashRateProvider _poolHashRateProvider;

        public PoolHashRateService(IPoolHashRateProvider poolHashRateProvider)
        {
            _poolHashRateProvider = poolHashRateProvider;
        }

        public async Task<ICollection<Pool>> GetPoolHashRateDistributionForTxDateAsync(int timeOfBlockMining)
        {
            var txDate = DateTime.UnixEpoch.AddSeconds(timeOfBlockMining);

            return await _poolHashRateProvider.GetDistributionBasedOnDateAsync(txDate);
        }
    }
}