using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Carbonara.Models.PoolHashRateDistribution;

namespace Carbonara.Providers.PoolHashRateProvider
{
    public interface IPoolHashRateProvider
    {
        Task<List<Pool>> GetDistributionBasedOnDateAsync(DateTime date);
    }
}