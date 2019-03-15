using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Carbonara.Models.PoolTypeHashRateDistribution;
using Newtonsoft.Json;

namespace Carbonara.Providers.HashRatePerPoolProvider
{
    public class HashRatePerPoolProvider : BaseJsonFileProvider, IHashRatePerPoolProvider
    {
        public HashRatePerPoolProvider()
        {
        }

        public async Task<List<PoolTypeHashRateDistribution>> GetHashRatePerPoolAsync()
        {
            return await ReadFromFileAndDeserialize<List<PoolTypeHashRateDistribution>>("HashRateDistributionPerPool.json");
        }
    }
}