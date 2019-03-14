using System.Collections.Generic;
using System.Threading.Tasks;
using Carbonara.Models.PoolTypeHashRateDistribution;

namespace Carbonara.Services.HashRatePerPoolService
{
    public interface IHashRatePerPoolService
    {
        Task<ICollection<PoolTypeHashRateDistribution>> GetHashRatePerPoolAsync();
    }
}