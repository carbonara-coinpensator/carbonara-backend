using System.Collections.Generic;
using System.Threading.Tasks;
using Carbonara.Models.Calculation;

namespace Carbonara.Services.CalculationService
{
    public interface ICalculationService
    {
        Task<TotalCalculationResult> CalculateTotalSummary(List<string> txHashes, string hashingAlg, string countryToUseForCo2EmissionAverage);
        Task<TotalCalculationResult> Calculate(string txHash, string hashingAlg, string cO2EmissionCountry);
        Task<decimal> CalculateBlockEnergyConsumptionAsync(string blockHash);
    }
}
