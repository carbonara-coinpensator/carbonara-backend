using System.Collections.Generic;
using System.Threading.Tasks;
using Carbonara.Models.PoolHashRateDistribution;

namespace Carbonara.Services.PoolHashRateService
{
    public interface IPoolHashRateService
    {
        Task<List<Pool>> GetPoolHashRateDistributionForTxDateAsync(int timeOfBlockMining);
    }
}