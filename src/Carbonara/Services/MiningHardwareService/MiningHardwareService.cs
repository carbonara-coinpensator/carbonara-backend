using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Carbonara.Models.MiningHardware;
using Carbonara.Providers;
using Newtonsoft.Json;

namespace Carbonara.Services.MiningHardwareService
{
    public class MiningHardwareService : IMiningHardwareService
    {
        private readonly IJsonHardwareProvider jsonHardwareProvider;

        public MiningHardwareService(IJsonHardwareProvider jsonHardwareProvider)
        {
            this.jsonHardwareProvider = jsonHardwareProvider;
        }

        public async Task<List<MiningDevice>> GetAll()
        {
            return await jsonHardwareProvider.GetAll();
        }

        public async Task<List<int>> GetAvailableYears()
        {
            return await jsonHardwareProvider.GetAvailableYears();
        }

        public async Task<List<MiningDevice>> GetHardwareByAlgorithmAndYear(MiningAlgorithm algorithm, int year)
        {
            return await jsonHardwareProvider.GetHardwareByAlgorithmAndYear(algorithm, year);
        }

        public async Task<List<MiningDevice>> GetHardwareByMiningAlgorithm(MiningAlgorithm algorithm)
        {
            return await jsonHardwareProvider.GetHardwareByMiningAlgorithm(algorithm);
        }
    }
}