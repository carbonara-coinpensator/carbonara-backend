using System.Collections.Generic;
using System.Threading.Tasks;
using Carbonara.Models.MiningHardware;

namespace Carbonara.Providers
{
    public interface IJsonHardwareProvider
    {
        Task<List<MiningDevice>> GetHardwareByMiningAlgorithm(MiningAlgorithm alogrithm);
        Task<List<MiningDevice>> GetAll();
    }
}