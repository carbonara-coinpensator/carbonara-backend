using System.Collections.Generic;
using System.Threading.Tasks;
using Carbonara.Models.PoolTypeHashRateDistribution;

namespace Carbonara.Providers.HashRatePerPoolProvider
{
    public interface IHashRatePerPoolProvider
    {
        Task<List<PoolTypeHashRateDistribution>> GetHashRatePerPoolAsync();
    }
}