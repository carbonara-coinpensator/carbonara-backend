using System.Threading.Tasks;
using Carbonara.Models.Calculation;

namespace Carbonara.Services.CalculationService
{
    public interface ICalculationService {
        Task<CalculationResult> Calculate(string txHash, int minningGearYear, string hashingAlg, string cO2EmissionCountry);
    }
}
