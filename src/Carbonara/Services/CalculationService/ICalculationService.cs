using System.Threading.Tasks;

namespace Carbonara.Services.CalculationService
{
    public interface ICalculationService {
        Task<decimal> Calculate(string txHash, int minningGearYear, string hashingAlg, string cO2EmissionCountry);
    }
}
