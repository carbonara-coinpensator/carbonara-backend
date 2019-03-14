using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Carbonara.Models.Country;
using Carbonara.Models.PoolHashRateDistribution;
using Carbonara.Models.PoolTypeHashRateDistribution;
using Carbonara.Services;
using Carbonara.Services.CountryCo2EmissionService;
using Carbonara.Services.PoolHashRateService;

public class CalculationService : ICalculationService
{
    private readonly IBlockParametersService _blockParametersService;
    private readonly INetworkHashRateService _networkHashRateService;
    private readonly IPoolHashRateService _poolHashRateService;
    private readonly ICountryCo2EmissionService _countryCo2EmissionService;

    public CalculationService(
        IBlockParametersService blockParametersService,
        INetworkHashRateService networkHashRateService,
        IPoolHashRateService poolHashRateService,
        ICountryCo2EmissionService countryCo2EmissionService)
    {
        _blockParametersService = blockParametersService;
        _networkHashRateService = networkHashRateService;
        _poolHashRateService = poolHashRateService;
        _countryCo2EmissionService = countryCo2EmissionService;
    }

    public async Task<decimal> Calculate(string txHash, int? minningGearYear, string hashingAlg, string cO2EmissionCountry)
    {
        var transactionDate = DateTime.Now; // Transaction date based on the hash // var blockParameters = await _blockParametersService.GetBlockParameters(txHash);
        var noOftransactionsInTheBlock = 2000; // No of transcations in the same block // NumberOfTransactionsInBlock = blockParameters.NumberOfTransactionsInBlock
        var blockMiningTimeInSeconds = 600; // Time it took to mine the block in seconds //     BlockTimeInSeconds = blockParameters.BlockTimeInSeconds,
        var networkHashRateInTHs = 43141132; // Hashrate of the network for a fetched tranasaction date in TH/s // var hashRate = await _networkHashRateService.GetDailyHashRateInPastAsync(blockParameters.BlockTimeInSeconds);
        var avgMachineHashRateInTHs = 14; // Average hashrate of a machine TH/s
        var avgMachineEnergyConsumptionInKWH = 1.372m; // Average machine energy consumption KW/h

        var hashRateDistributionPerPool = new List<Pool>() {
            new Pool { Name = "BTC.COM", Percent = 18.98m, PoolType = "BTC"  },
            new Pool { Name = "F2Pool", Percent = 14.6m, PoolType = "BTC"  },
            new Pool { Name = "Poolin", Percent = 10.95m, PoolType = "BTC"  },
            new Pool { Name = "ViaBTC", Percent = 10.95m, PoolType = "BTC"  }
        }; // A list of pools with geo category (pool type) and their participation in the hash rate 

        var countriesWithCo2Emission = await _countryCo2EmissionService.GetCountriesCo2EmissionAsync();

        var geoDistributionOfHashratePerPoolCategory = new List<PoolTypeHashRateDistribution>() {
            new PoolTypeHashRateDistribution()
                {
                    PoolType = "BTC",
                    DistributionPerCOuntry = new List<HashRateDistributionPerCountry>()
                        {
                            new HashRateDistributionPerCountry { CountryCode = "CA", Percentage = 0m },
                            new HashRateDistributionPerCountry { CountryCode = "CN", Percentage = 60.8m },
                            new HashRateDistributionPerCountry { CountryCode = "EU", Percentage = 25.2m },
                            new HashRateDistributionPerCountry { CountryCode = "JP", Percentage = 0m },
                            new HashRateDistributionPerCountry { CountryCode = "SG", Percentage = 0m },
                            new HashRateDistributionPerCountry { CountryCode = "US", Percentage = 14m }
                        }
                }
             }; // A list of geo categories (pool types) with their participation in the hashrate per region

        var noOfMachinesDoingTheMinning = networkHashRateInTHs / avgMachineHashRateInTHs; // The number of machines that were doing the mining for that block, under the assumption that all of them mined
        var energyConsumptionPerMachinePerBlockInKWH = avgMachineEnergyConsumptionInKWH * blockMiningTimeInSeconds / 3600; // The energy used by one machine to mine that block

        var fullEnergyConsumptionPerTransactionInKWH = noOfMachinesDoingTheMinning * energyConsumptionPerMachinePerBlockInKWH / noOftransactionsInTheBlock;

        var energyConsumptionPerPoolPerTransactionInKwh = new Dictionary<string, decimal>();

        foreach (var pool in hashRateDistributionPerPool)
        {
            var poolEnergyConsumption = fullEnergyConsumptionPerTransactionInKWH * pool.Percent / 100;
            energyConsumptionPerPoolPerTransactionInKwh.Add(pool.Name, poolEnergyConsumption);
        }



        var result = await Task.FromResult(201.2m);
        return result;
    }
}