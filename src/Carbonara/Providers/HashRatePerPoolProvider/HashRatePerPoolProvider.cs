using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Carbonara.Models.PoolTypeHashRateDistribution;
using Newtonsoft.Json;

namespace Carbonara.Providers.HashRatePerPoolProvider
{
    public class HashRatePerPoolProvider : BaseLiteDbProvider, IHashRatePerPoolProvider
    {
        public HashRatePerPoolProvider()
        {
        }

        public async Task<List<PoolTypeHashRateDistribution>> GetHashRatePerPoolAsync()
        {
            var results = await ReadCollectionFromDb<PoolTypeHashRateDistribution>("hashrateperpooldistributions");
            return results.ToList();
        }
    }
}