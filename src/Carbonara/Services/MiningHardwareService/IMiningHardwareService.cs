using System.Collections.Generic;
using System.Threading.Tasks;
using Carbonara.Models.MiningHardware;

namespace Carbonara.Services
{
    public interface IMiningHardwareService
    {
        Task<List<MiningDevice>> GetHardwareByMiningAlgorithm(MiningAlgorithm alogrithm);
    }
}