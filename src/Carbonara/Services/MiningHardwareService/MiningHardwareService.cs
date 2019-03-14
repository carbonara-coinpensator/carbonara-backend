using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Carbonara.Models.MiningHardware;
using Carbonara.Providers;
using Newtonsoft.Json;

namespace Carbonara.Services
{
    public class MiningHardwareService : IMiningHardwareService
    {
        private readonly IJsonHardwareProvider jsonHardwareProvider;

        public MiningHardwareService(IJsonHardwareProvider jsonHardwareProvider)
        {
            this.jsonHardwareProvider = jsonHardwareProvider;
        }

        public async Task<List<MiningDevice>> GetHardwareByMiningAlgorithm(MiningAlgorithm alogrithm)
        {
            return await jsonHardwareProvider.GetHardwareByMiningAlgorithm(alogrithm);
        }
    }
}