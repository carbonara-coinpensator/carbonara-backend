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
        private readonly IHardwareProvider hardwareProvider;

        public MiningHardwareService(IHardwareProvider hardwareProvider)
        {
            this.hardwareProvider = hardwareProvider;
        }

        public async Task<List<MiningDevice>> GetAll()
        {
            return await hardwareProvider.GetAll();
        }

        public async Task<List<int>> GetAvailableYears()
        {
            return await hardwareProvider.GetAvailableYears();
        }

        public async Task<List<MiningDevice>> GetHardwareByAlgorithmAndYear(MiningAlgorithm algorithm, int year)
        {
            return await hardwareProvider.GetHardwareByAlgorithmAndYear(algorithm, year);
        }

        public async Task<List<MiningDevice>> GetHardwareByMiningAlgorithm(MiningAlgorithm algorithm)
        {
            return await hardwareProvider.GetHardwareByMiningAlgorithm(algorithm);
        }
    }
}