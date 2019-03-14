using System.Collections.Generic;
using System.Threading.Tasks;
using Carbonara.Models.PoolTypeHashRateDistribution;

namespace Carbonara.Services.HashRatePerPoolService
{
    public interface IHashRatePerPoolService
    {
        Task<List<PoolTypeHashRateDistribution>> GetHashRatePerPoolAsync();
    }
}