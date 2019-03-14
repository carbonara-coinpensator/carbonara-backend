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
        private readonly string _path;

        public JsonHardwareProvider()
        {
            _path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        }
        public async Task<List<MiningDevice>> GetHardwareByMiningAlgorithm(MiningAlgorithm alogrithm = MiningAlgorithm.SHA256)
        {
            var devices = await ReadDevicesFromFile($"{_path}/MiningHardware.json");
            return devices.Where(i => i.Algorithm == alogrithm).ToList();
        }

        public async Task<List<MiningDevice>> GetAll()
        {
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            var devices = await ReadDevicesFromFile($"{_path}/MiningHardware.json");
            return devices.ToList();
        }        

        private async Task<List<MiningDevice>> ReadDevicesFromFile(string filename)
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                var json = await reader.ReadToEndAsync();
                return JsonConvert.DeserializeObject<List<MiningDevice>>(json);
            }
        }
    }
}