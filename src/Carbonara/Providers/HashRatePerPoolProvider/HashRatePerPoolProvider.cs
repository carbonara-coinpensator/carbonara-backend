using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Carbonara.Models.PoolTypeHashRateDistribution;
using Newtonsoft.Json;

namespace Carbonara.Providers.HashRatePerPoolProvider
{
    public class HashRatePerPoolProvider : IHashRatePerPoolProvider
    {
        private readonly string _path;

        public HashRatePerPoolProvider()
        {
            _path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        }

        public async Task<ICollection<PoolTypeHashRateDistribution>> GetHashRatePerPoolAsync()
        {
            return await ReadHashRatePerPoolFromFile($"{_path}/HashRateDistributionPerPool.json");
        }

        private async Task<ICollection<PoolTypeHashRateDistribution>> ReadHashRatePerPoolFromFile(string filename)
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                var json = await reader.ReadToEndAsync();
                return JsonConvert.DeserializeObject<List<PoolTypeHashRateDistribution>>(json);
            }
        }
    }
}