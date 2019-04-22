using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Carbonara.Models.MiningHardware;
using Carbonara.Providers;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Carbonara.Providers
{
    public class HardwareProvider : BaseLiteDbProvider, IHardwareProvider
    {
        
        public HardwareProvider()
        {
        }

        public async Task<List<MiningDevice>> GetHardwareByMiningAlgorithm(MiningAlgorithm algorithm = MiningAlgorithm.SHA256)
        {
            var devices = await GetAll();
            return devices.Where(i => i.Algorithm == algorithm).ToList();
        }

        public async Task<List<MiningDevice>> GetAll()
        {
            var result = await ReadCollectionFromDb<MiningDevice>("miningdevices");
            return result.ToList();
        }

        public async Task<List<MiningDevice>> GetHardwareByAlgorithmAndYear(MiningAlgorithm algorithm, int year)
        {
            var devices = await GetAll();
            return devices.Where(i => i.Algorithm == algorithm && i.ProductionYear == year).ToList();
        }

        public async Task<List<int>> GetAvailableYears()
        {
            var devices = await GetAll();
            return devices.Select(i => i.ProductionYear).OrderByDescending(i => i).ToList();
        }
    }
}