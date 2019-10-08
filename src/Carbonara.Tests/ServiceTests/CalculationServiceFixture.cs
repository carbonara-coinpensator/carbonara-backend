using Carbonara.Models.Country;
using Carbonara.Models.Formula;
using Carbonara.Models.MiningHardware;
using Carbonara.Models.PoolHashRateDistribution;
using Carbonara.Models.PoolTypeHashRateDistribution;
using Carbonara.Services.BlockParametersService;
using Carbonara.Services.CalculationService;
using Carbonara.Services.CountryCo2EmissionService;
using Carbonara.Services.HashRatePerPoolService;
using Carbonara.Services.MiningHardwareService;
using Carbonara.Services.NetworkHashRateService;
using Carbonara.Services.PoolHashRateService;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Carbonara.Tests.ServiceTests
{
    public class CalculationServiceFixture
    {
        private readonly ICalculationService _calculationService;
        private string txHash = "x123";

        public CalculationServiceFixture()
        {
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.SetupGet(c => c["RequestWaitingTime"]).Returns("300");

            var blockParametersServiceMock = new Mock<IBlockParametersService>();
            blockParametersServiceMock.Setup(x => x.GetBlockParametersByTxHash(txHash))
                .Returns(Task.FromResult
                (
                    new BlockParameters
                    {
                        BlockTimeInSeconds = 600,
                        NumberOfTransactionsInBlock = 2000,
                        TimeOfBlockMining = 999999
                    }
                ));

            var networkHashRateServiceMock = new Mock<INetworkHashRateService>();
            networkHashRateServiceMock.Setup(x => x.GetDailyHashRateInPastAsync(999999))
                .Returns(Task.FromResult(43141132m));

            var poolHashRateServiceMock = new Mock<IPoolHashRateService>();
            poolHashRateServiceMock.Setup(x => x.GetPoolHashRateDistributionForTxDateAsync(999999))
                .Returns(Task.FromResult
                (
                    new List<Pool>() {
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
                        new Pool { Name = "tigerpool.net", Percent = 0.73m, PoolType = "CN"  }
                    }
                )
            );

            var countryCo2EmissionServiceMock = new Mock<ICountryCo2EmissionService>();
            countryCo2EmissionServiceMock.Setup(x => x.GetCountriesCo2EmissionAsync())
                .Returns(Task.FromResult
                (
                    new List<Country>()
                    {
                        new Country { CountryCode = "CA", Co2Emission = 158.42m },
                        new Country { CountryCode = "CN", Co2Emission = 711.3686m },
                        new Country { CountryCode = "EU", Co2Emission =  336.8498m },
                        new Country { CountryCode = "JP", Co2Emission = 571.5443m },
                        new Country { CountryCode = "SG", Co2Emission = 431.3m },
                        new Country { CountryCode = "US", Co2Emission = 489.4282m },
                    }
                )
            );

            var miningHardwareServiceMock = new Mock<IMiningHardwareService>();
            miningHardwareServiceMock.Setup(x => x.GetHardwareByMiningAlgorithm(MiningAlgorithm.SHA256))
                .Returns(Task.FromResult
                (
                    new List<MiningDevice>()
                    {
                        new MiningDevice { HashRate = 14000000000000, PowerConsumption = 1375, ProductionYear = 2013 },
                    }
                )
            );

            var hashRatePerPoolServiceMock = new Mock<IHashRatePerPoolService>();
            hashRatePerPoolServiceMock.Setup(x => x.GetHashRatePerPoolAsync())
                .Returns(Task.FromResult
                (
                   new List<PoolTypeHashRateDistribution>()
                   {
                        new PoolTypeHashRateDistribution()
                            {
                                PoolType = "BTC",
                                DistributionPerCountry = new List<HashRateDistributionPerCountry>()
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
                                DistributionPerCountry = new List<HashRateDistributionPerCountry>()
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
                                DistributionPerCountry = new List<HashRateDistributionPerCountry>()
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
                                DistributionPerCountry = new List<HashRateDistributionPerCountry>()
                                    {
                                        new HashRateDistributionPerCountry { CountryCode = "CA", Percentage = 0m },
                                        new HashRateDistributionPerCountry { CountryCode = "CN", Percentage = 0m },
                                        new HashRateDistributionPerCountry { CountryCode = "EU", Percentage = 0m },
                                        new HashRateDistributionPerCountry { CountryCode = "JP", Percentage = 0m },
                                        new HashRateDistributionPerCountry { CountryCode = "SG", Percentage = 0m },
                                        new HashRateDistributionPerCountry { CountryCode = "US", Percentage = 100m }
                                    }
                            }
                        }
                )
            );

            _calculationService = new CalculationService(
                blockParametersServiceMock.Object,
                networkHashRateServiceMock.Object,
                poolHashRateServiceMock.Object,
                countryCo2EmissionServiceMock.Object,
                miningHardwareServiceMock.Object,
                hashRatePerPoolServiceMock.Object,
                configurationMock.Object
            );
        }

        [Fact]
        public async Task TestCalculateWithEmissionsPerCountry()
        {
            var result = await _calculationService.Calculate(txHash, null, null);
            Assert.Equal(192, Math.Round(result.CalculationPerYear[2013].FullCo2EmissionInKg));
        }

        [Theory]
        [InlineData(2)]
        [InlineData(6)]
        public async Task TestCalculateMultipleTransactionsWithEmissions(int count)
        {
            var txHashes = new List<string> { txHash };
            var result = await _calculationService.CalculateTotalSummary(txHashes, null, null);
            var emissionsIn2013 = result.CalculationPerYear[2013].FullCo2EmissionInKg;
            var consumptionIn2013 = result.CalculationPerYear[2013].EnergyConsumptionPerCountryInKWh.First(item => item.CountryCode.Equals("CA")).EnergyConsumptionInKWh;

            for (var i = 1; i < count; i++) txHashes.Add(txHash); // adding the same hash multiple times should result to multiple co2 emission

            var result2 = await _calculationService.CalculateTotalSummary(txHashes, null, null);
            var doubleEmissionsIn2013 = result2.CalculationPerYear[2013].FullCo2EmissionInKg;
            var doubleConsumptionForCAin2013 = result2.CalculationPerYear[2013].EnergyConsumptionPerCountryInKWh.First(item => item.CountryCode.Equals("CA")).EnergyConsumptionInKWh;

            Assert.Equal(emissionsIn2013 * count, doubleEmissionsIn2013);
            Assert.Equal(consumptionIn2013 * count, doubleConsumptionForCAin2013);
        }

        [Fact]
        public async Task TestCalculateWithChinaEmissions()
        {
            var result = await _calculationService.Calculate(txHash, null, "CN");
            Assert.Equal(251, Math.Round(result.CalculationPerYear[2013].FullCo2EmissionInKg));
        }

        [Theory]
        [InlineData("CA")]
        [InlineData("CN")]
        [InlineData("EU")]
        [InlineData("JP")]
        [InlineData("SG")]
        [InlineData("US")]
        public async Task TestCalculateMultipleTransactionsSpecificCountryEmissions(string country)
        {
            var txHashes = new List<string> { txHash };
            var result = await _calculationService.CalculateTotalSummary(txHashes, null, country);
            var emissionsIn2013 = result.CalculationPerYear[2013].FullCo2EmissionInKg;
            var consumptionIn2013 = result.CalculationPerYear[2013].EnergyConsumptionPerCountryInKWh.First(item => item.CountryCode.Equals(country)).EnergyConsumptionInKWh;

            txHashes.Add(txHash); // twice the same hash should result to double the co2 emission
            var result2 = await _calculationService.CalculateTotalSummary(txHashes, null, country);
            var doubleEmissionsIn2013 = result2.CalculationPerYear[2013].FullCo2EmissionInKg;
            var doubleConsumptionIn2013 = result2.CalculationPerYear[2013].EnergyConsumptionPerCountryInKWh.First(item => item.CountryCode.Equals(country)).EnergyConsumptionInKWh;

            Assert.Equal(emissionsIn2013 * 2, doubleEmissionsIn2013);
            Assert.Equal(consumptionIn2013 * 2, doubleConsumptionIn2013);
        }

        [Fact]
        public async Task TestCalculateWithEUEmissions()
        {
            var result = await _calculationService.Calculate(txHash, null, "EU");
            Assert.Equal(119, Math.Round(result.CalculationPerYear[2013].FullCo2EmissionInKg));
        }
    }
}
