using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Carbonara.Models.MiningHardware;
using Carbonara.Providers;
using Newtonsoft.Json;

namespace Carbonara.Services
{
    public class JsonHardwareProvider : IJsonHardwareProvider
    {
        public JsonHardwareProvider()
        {
        }

        public async Task<List<MiningDevice>> GetHardwareByMiningAlgorithm(MiningAlgorithm algorithm = MiningAlgorithm.SHA256)
        {
            var devices = await GetAll();
            return devices.Where(i => i.Algorithm == algorithm).ToList();
        }

        public async Task<List<MiningDevice>> GetAll()
        {
            var devices = await ReadDevicesFromFile($"MiningHardware.json");
            return devices.ToList();
        }

        public async Task<List<MiningDevice>> GetHardwareByAlgorithmAndYear(MiningAlgorithm algorithm, int year)
        {
            var devices = await GetAll();
            return devices.Where(i => i.Algorithm == algorithm && i.ProductionYear == year).ToList();
        }

        public async Task<List<int>> GetAvailableYears()
        {
            var devices = await GetAll();
            return devices.Select(i => i.ProductionYear).ToList();
        }

        private async Task<List<MiningDevice>> ReadDevicesFromFile(string filename)
        {
            var _path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            using (StreamReader reader = new StreamReader($"{_path}/{filename}"))
            {
                var json = await reader.ReadToEndAsync();
                return JsonConvert.DeserializeObject<List<MiningDevice>>(json);
            }
        }
    }
}