using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carbonara.Models.Country;
using Carbonara.Models.Calculation;
using Carbonara.Models.PoolHashRateDistribution;
using Carbonara.Models.PoolTypeHashRateDistribution;
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

        var hashRateDistributionPerPool = new List<Pool>() {
            new Pool { Name = "BTC.COM", Percent = 18.98m, PoolType = "BTC"  },
            new Pool { Name = "F2Pool", Percent = 14.6m, PoolType = "BTC"  },
            new Pool { Name = "Poolin", Percent = 10.95m, PoolType = "BTC"  },
            new Pool { Name = "ViaBTC", Percent = 10.95m, PoolType = "BTC"  },
            new Pool { Name = "SlushPool", Percent = 7.3m, PoolType = "SLUSH"  },
            new Pool { Name = "BTC.TOP", Percent = 5.84m, PoolType = "BTC"  },
            new Pool { Name = "unknown", Percent = 5.84m, PoolType = "SLUSH"  },
            new Pool { Name = "AntPool", Percent = 5.11m, PoolType = "BTC"  },
            new Pool { Name = "BitClub", Percent = 4.38m, PoolType = "SLUSH"  },
            new Pool { Name = "Huobi.pool", Percent = 4.38m, PoolType = "SLUSH"  },
            new Pool { Name = "WAYI.CN", Percent = 2.92m, PoolType = "CN"  },
            new Pool { Name = "Bitcoin.com", Percent = 2.19m, PoolType = "US"  },
            new Pool { Name = "DPOOL", Percent = 2.19m, PoolType = "CN"  },
            new Pool { Name = "BitFury", Percent = 1.46m, PoolType = "SLUSH"  },
            new Pool { Name = "Bixin", Percent = 1.46m, PoolType = "CN"  },
            new Pool { Name = "sigmapool.com", Percent = 0.73m, PoolType = "SLUSH"  },
            new Pool { Name = "tigerpool.net", Percent = 0.73m, PoolType = "CN"  },
        }; // A list of pools with geo category (pool type) and their participation in the hash rate 

        var countriesWithAvgCo2Emission = new List<Country>() {
            new Country { CountryCode = "CA", Co2Emission = 158.42m },
            new Country { CountryCode = "CN", Co2Emission = 711.3686m },
            new Country { CountryCode = "EU", Co2Emission = 336.8498m },
            new Country { CountryCode = "JP", Co2Emission = 571.5443m },
            new Country { CountryCode = "SG", Co2Emission = 431.3m },
            new Country { CountryCode = "US", Co2Emission = 489.4282m }
        }; // A list of countries with their latest co2 emission per kwh

        var geoDistributionOfHashratePerPoolType = new List<PoolTypeHashRateDistribution>() {
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
                },
            new PoolTypeHashRateDistribution()
                {
                    PoolType = "SLUSH",
                    DistributionPerCOuntry = new List<HashRateDistributionPerCountry>()
                        {
                            new HashRateDistributionPerCountry { CountryCode = "CA", Percentage = 14.65m },
                            new HashRateDistributionPerCountry { CountryCode = "CN", Percentage = 5.38m },
                            new HashRateDistributionPerCountry { CountryCode = "EU", Percentage = 45.65m },
                            new HashRateDistributionPerCountry { CountryCode = "JP", Percentage = 1.37m },
                            new HashRateDistributionPerCountry { CountryCode = "SG", Percentage = 0.94m },
                            new HashRateDistributionPerCountry { CountryCode = "US", Percentage = 31.99m }
                        }
                },
            new PoolTypeHashRateDistribution()
                {
                    PoolType = "CN",
                    DistributionPerCOuntry = new List<HashRateDistributionPerCountry>()
                        {
                            new HashRateDistributionPerCountry { CountryCode = "CA", Percentage = 0m },
                            new HashRateDistributionPerCountry { CountryCode = "CN", Percentage = 100m },
                            new HashRateDistributionPerCountry { CountryCode = "EU", Percentage = 0m },
                            new HashRateDistributionPerCountry { CountryCode = "JP", Percentage = 0m },
                            new HashRateDistributionPerCountry { CountryCode = "SG", Percentage = 0m },
                            new HashRateDistributionPerCountry { CountryCode = "US", Percentage = 0m }
                        }
                },
            new PoolTypeHashRateDistribution()
                {
                    PoolType = "US",
                    DistributionPerCOuntry = new List<HashRateDistributionPerCountry>()
                        {
                            new HashRateDistributionPerCountry { CountryCode = "CA", Percentage = 0m },
                            new HashRateDistributionPerCountry { CountryCode = "CN", Percentage = 0m },
                            new HashRateDistributionPerCountry { CountryCode = "EU", Percentage = 0m },
                            new HashRateDistributionPerCountry { CountryCode = "JP", Percentage = 0m },
                            new HashRateDistributionPerCountry { CountryCode = "SG", Percentage = 0m },
                            new HashRateDistributionPerCountry { CountryCode = "US", Percentage = 100m }
                        }
                }
             }; // A list of geo categories (pool types) with their participation in the hashrate per country

        var noOfMachinesDoingTheMinning = networkHashRateInTHs / avgMachineHashRateInTHs; // The number of machines that were doing the mining for that block, under the assumption that all of them mined
        var energyConsumptionPerMachinePerBlockInKWH = avgMachineEnergyConsumptionInKWH * blockMiningTimeInSeconds / 3600; // The energy used by one machine to mine that block

        var fullEnergyConsumptionPerTransactionInKWH = noOfMachinesDoingTheMinning * energyConsumptionPerMachinePerBlockInKWH / noOftransactionsInTheBlock;

        var energyConsumptionPerPool = 
            this.DistributeEnergyPerPool(fullEnergyConsumptionPerTransactionInKWH, hashRateDistributionPerPool);

        var energyConsumptionPerCountry = 
            this.DistributeEnergyPerCountry(energyConsumptionPerPool, geoDistributionOfHashratePerPoolType);
        
        var co2EmissionPerCountry = 
            this.CalculateEmissionPerCountry(energyConsumptionPerCountry, countriesWithAvgCo2Emission);

        var worldWideEmission = 
            co2EmissionPerCountry.Sum(c => c.Co2Emission);

        var result = await Task.FromResult(worldWideEmission);
        return result;
    }

    private List<EnergyConsumptionPerPool> DistributeEnergyPerPool(decimal fullEnergyForTransaction, List<Pool> hashRateDistributionPerPool) 
    {
        var energyConsumptionPerPoolPerTransactionInKwh = new List<EnergyConsumptionPerPool>();

        foreach (var pool in hashRateDistributionPerPool)
        {
            var poolEnergyConsumption = fullEnergyForTransaction * pool.Percent / 100;

            var energyConsumptionPerPool = new EnergyConsumptionPerPool()
            {
                Pool = pool,
                EnergyConsumption = poolEnergyConsumption
            };

            energyConsumptionPerPoolPerTransactionInKwh.Add(energyConsumptionPerPool);
        }

        return energyConsumptionPerPoolPerTransactionInKwh;
    }

    private List<EnergyConsumptionPerCountry> DistributeEnergyPerCountry(
        List<EnergyConsumptionPerPool> energyConsumptionPerPool,
        List<PoolTypeHashRateDistribution> geoDistributionOfHashratePerPoolType) 
    {
        var energyConsumptionPerCountryPerTransactionInKwh = new List<EnergyConsumptionPerCountry>();

        foreach (var energyPerPool in energyConsumptionPerPool) // Distribute pool energy for transcation per regions\countries
        {
            var geoDistributionOfHashRateForSinglePool = geoDistributionOfHashratePerPoolType
                .First(p => p.PoolType == energyPerPool.Pool.PoolType).DistributionPerCOuntry;

            foreach (var geoPoolDistribution in geoDistributionOfHashRateForSinglePool)
            {
                var consumptionPerCountry = energyConsumptionPerCountryPerTransactionInKwh.FirstOrDefault(c => c.CountryCode == geoPoolDistribution.CountryCode);

                if (consumptionPerCountry != null)
                {
                    consumptionPerCountry.EnergyConsumption += energyPerPool.EnergyConsumption * geoPoolDistribution.Percentage / 100;
                }
                else
                {
                    energyConsumptionPerCountryPerTransactionInKwh.Add(
                        new EnergyConsumptionPerCountry
                        {
                            CountryCode = geoPoolDistribution.CountryCode,
                            EnergyConsumption = energyPerPool.EnergyConsumption * geoPoolDistribution.Percentage / 100
                        }
                    );
                }
            }
        }

        return energyConsumptionPerCountryPerTransactionInKwh;
    }

    private List<Co2EmissionPerCountry> CalculateEmissionPerCountry(
        List<EnergyConsumptionPerCountry> energyConsumptionPerCountry,
        List<Country> countriesWithAvgCo2Emission)
    {
        var co2PerCountry = new List<Co2EmissionPerCountry>();
        
        foreach(var consumptionPerCountry in energyConsumptionPerCountry) // Translate energy consumption per country to co2 emissions per country
        {
            var avgEmissionPerEnergyInGrams = countriesWithAvgCo2Emission.First(c => c.CountryCode == consumptionPerCountry.CountryCode).Co2Emission;

            co2PerCountry.Add(
                new Co2EmissionPerCountry 
                {
                    CountryCode = consumptionPerCountry.CountryCode,
                    Co2Emission = consumptionPerCountry.EnergyConsumption * avgEmissionPerEnergyInGrams / 1000
                }
            );
        }

        return co2PerCountry;
    }
}