using System.Collections.Generic;
using System.Threading.Tasks;
using Carbonara.Models.PoolTypeHashRateDistribution;
using Carbonara.Providers.HashRatePerPoolProvider;

namespace Carbonara.Services.HashRatePerPoolService
{
    public class HashRatePerPoolService : IHashRatePerPoolService
    {
        private readonly IHashRatePerPoolProvider _hashRatePerPoolProvider;

        public HashRatePerPoolService(IHashRatePerPoolProvider hashRatePerPoolProvider)
        {
            _hashRatePerPoolProvider = hashRatePerPoolProvider;
        }

        public async Task<List<PoolTypeHashRateDistribution>> GetHashRatePerPoolAsync()
        {
            return await _hashRatePerPoolProvider.GetHashRatePerPoolAsync();
        }
    }
}