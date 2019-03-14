using System.Collections.Generic;
using System.Threading.Tasks;
using Carbonara.Models.MiningHardware;

namespace Carbonara.Providers
{
    public interface IJsonHardwareProvider
    {
        Task<List<MiningDevice>> GetHardwareByAlgorithmAndYear(MiningAlgorithm algorithm, int year);
        Task<List<MiningDevice>> GetHardwareByMiningAlgorithm(MiningAlgorithm algorithm);
        Task<List<MiningDevice>> GetAll();
        Task<List<int>> GetAvailableYears();
    }
}