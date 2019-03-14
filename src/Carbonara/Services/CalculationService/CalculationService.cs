using System;
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
        var transactionDate = DateTime.Now; // Transaction date based on the hash // var blockParameters = await _blockParametersService.GetBlockParameters(txHash);
        var noOftransactionsInTheBlock = 2000; // No of transcations in the same block // NumberOfTransactionsInBlock = blockParameters.NumberOfTransactionsInBlock
        var blockMiningTimeInSeconds = 600; // Time it took to mine the block in seconds //     BlockTimeInSeconds = blockParameters.BlockTimeInSeconds,
        var networkHashRateInTHs = 43141132; // Hashrate of the network for a fetched tranasaction date in TH/s // var hashRate = await _networkHashRateService.GetDailyHashRateInPastAsync(blockParameters.BlockTimeInSeconds);
        var avgMachineHashRateInTHs = 14; // Average hashrate of a machine TH/s
        var avgMachineEnergyConsumptionInKWH = 1.372m; // Average machine energy consumption KW/h

        // var hashRateDistributionPerPool // A list of pools with geo category and their participation in the hash rate 
        // var geoDistributionOfHashratePerPoolCategory // A list of geo categories with their participation in the hashrate per region
        // var c02EmissionsPerRegion // A list of countries with their latest co2 emission per kwh

        var noOfMachinesDoingTheMinning = networkHashRateInTHs / avgMachineHashRateInTHs; // The number of machines that were doing the mining for that block, under the assumption that all of them mined
        var energyConsumptionPerMachinePerBlockInKWH = avgMachineEnergyConsumptionInKWH * blockMiningTimeInSeconds / 3600; // The energy used by one machine to mine that block

        var fullEnergyConsumptionPerTransactionInKWH = noOfMachinesDoingTheMinning * energyConsumptionPerMachinePerBlockInKWH / noOftransactionsInTheBlock;

        var result = await Task.FromResult(201.2m);
        return result;
    }
}