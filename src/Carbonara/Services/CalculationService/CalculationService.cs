using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carbonara.Models.Country;
using Carbonara.Models.Calculation;
using Carbonara.Models.PoolHashRateDistribution;
using Carbonara.Models.PoolTypeHashRateDistribution;
using Carbonara.Services;
using Carbonara.Services.CountryCo2EmissionService;
using Carbonara.Services.PoolHashRateService;
using Carbonara.Models.MiningHardware;
using Carbonara.Services.HashRatePerPoolService;

public class CalculationService : ICalculationService
{
    private readonly IBlockParametersService _blockParametersService;
    private readonly INetworkHashRateService _networkHashRateService;
    private readonly IPoolHashRateService _poolHashRateService;
    private readonly ICountryCo2EmissionService _countryCo2EmissionService;
    private readonly IMiningHardwareService _miningHardwareService;
    private readonly IHashRatePerPoolService _hashRatePerPoolService;

    public CalculationService(
        IBlockParametersService blockParametersService,
        INetworkHashRateService networkHashRateService,
        IPoolHashRateService poolHashRateService,
        ICountryCo2EmissionService countryCo2EmissionService,
        IMiningHardwareService miningHardwareService,
        IHashRatePerPoolService hashRatePerPoolService)
    {
        _blockParametersService = blockParametersService;
        _networkHashRateService = networkHashRateService;
        _poolHashRateService = poolHashRateService;
        _countryCo2EmissionService = countryCo2EmissionService;
        _miningHardwareService = miningHardwareService;
        _hashRatePerPoolService = hashRatePerPoolService;
    }

    public async Task<decimal> Calculate(string txHash, int? minningGearYear, string hashingAlg, string cO2EmissionCountry)
    {
        var blockParameters = await _blockParametersService.GetBlockParameters(txHash);
        var noOftransactionsInTheBlock = blockParameters.NumberOfTransactionsInBlock; // 2000;
        var blockMiningTimeInSeconds = blockParameters.BlockTimeInSeconds; // 600;
        var networkHashRateInTHs = await _networkHashRateService.GetDailyHashRateInPastAsync(blockParameters.BlockTimeInSeconds); // 43141132;
        
        var hardware = await _miningHardwareService.GetHardwareByAlgorithmAndYear(MiningAlgorithm.SHA256, 2013); // Assumption is antminer s9 for now
        var avgMachineHashRateInTHs = hardware.First().HashRate / 1000000; // 14; // Average hashrate of a machine TH/s
        var avgMachineEnergyConsumptionInKWH = hardware.First().PowerConsumption / 1000; // 1.372m; // Average machine energy consumption KW/h

        var hashRateDistributionPerPool = await _poolHashRateService.GetPoolHashRateDistributionForTxDateAsync(blockParameters.TimeOfBlockMining);

        // A list of geo categories (pool types) with their participation in the hashrate per region
        var geoDistributionOfHashratePerPoolType = await _hashRatePerPoolService.GetHashRatePerPoolAsync();

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

    private List<EnergyConsumptionPerPool> DistributeEnergyPerPool(
        decimal fullEnergyForTransaction, 
        ICollection<Pool> hashRateDistributionPerPool) 
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

    private ICollection<EnergyConsumptionPerCountry> DistributeEnergyPerCountry(
        ICollection<EnergyConsumptionPerPool> energyConsumptionPerPool,
        ICollection<PoolTypeHashRateDistribution> geoDistributionOfHashratePerPoolType) 
    {
        var energyConsumptionPerCountryPerTransactionInKwh = new List<EnergyConsumptionPerCountry>();

        foreach (var energyPerPool in energyConsumptionPerPool) // Distribute pool energy for transcation per regions\countries
        {
            var geoDistributionOfHashRateForSinglePool = geoDistributionOfHashratePerPoolType
                .First(p => p.PoolType == energyPerPool.Pool.PoolType).DistributionPerCountry;

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
        ICollection<EnergyConsumptionPerCountry> energyConsumptionPerCountry,
        ICollection<Country> countriesWithAvgCo2Emission)
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