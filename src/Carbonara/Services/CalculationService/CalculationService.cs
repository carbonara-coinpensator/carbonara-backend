using System.Threading.Tasks;
using Carbonara.Services;

public class CalculationService : ICalculationService
{
    private readonly IBlockParametersService _blockParametersService;
    private readonly INetworkHashRateService _networkHashRateService;

    public CalculationService(IBlockParametersService blockParametersService, INetworkHashRateService networkHashRateService)
    {
        _blockParametersService = blockParametersService;
        _networkHashRateService = networkHashRateService;
    }

    public async Task<decimal> Calculate(string txHash, int? minningGearYear, string hashingAlg, string cO2EmissionCountry)
    {
        // var blockParameters = await _blockParametersService.GetBlockParameters(txHash);
            // var hashRate = await _networkHashRateService.GetDailyHashRateInPastAsync(blockParameters.BlockTimeInSeconds);

            // var formulaParameters = new FormulaParameters
            // {
            //     BlockTimeInSeconds = blockParameters.BlockTimeInSeconds,
            //     HashRateOfDayTxWasMined = hashRate,
            //     NumberOfTransactionsInBlock = blockParameters.NumberOfTransactionsInBlock
            // };
        var result = await Task.FromResult(201.2m);
        return result;
    }
}