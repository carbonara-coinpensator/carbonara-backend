using System.Threading.Tasks;

public interface ICalculationService {
    Task<decimal> Calculate(string txHash, int? minningGearYear, string hashingAlg, string cO2EmissionCountry);
}