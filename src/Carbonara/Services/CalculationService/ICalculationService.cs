using System.Collections.Generic;
using System.Threading.Tasks;
using Carbonara.Models.Calculation;

namespace Carbonara.Services.CalculationService
{
    public interface ICalculationService {
        Task<TotalCalculationResult> Calculate(string txHash, string hashingAlg, string cO2EmissionCountry);
        Task<decimal> CalculateBlockEnergyConsumptionAsync(string blockHash);
    }
}
