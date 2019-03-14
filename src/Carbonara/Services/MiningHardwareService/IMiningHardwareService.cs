using System.Collections.Generic;
using System.Threading.Tasks;
using Carbonara.Models.MiningHardware;

namespace Carbonara.Services.MiningHardwareService
{
    public interface IMiningHardwareService
    {
        Task<List<MiningDevice>> GetHardwareByMiningAlgorithm(MiningAlgorithm algorithm);
        Task<List<MiningDevice>> GetHardwareByAlgorithmAndYear(MiningAlgorithm algorithm, int year);
        Task<List<MiningDevice>> GetAll();
    }
}