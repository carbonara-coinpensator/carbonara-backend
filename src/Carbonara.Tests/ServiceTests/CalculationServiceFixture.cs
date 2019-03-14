using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Carbonara.Models.Country;
using Carbonara.Models.Formula;
using Carbonara.Models.MiningHardware;
using Carbonara.Models.PoolHashRateDistribution;
using Carbonara.Models.PoolTypeHashRateDistribution;
using Carbonara.Providers;
using Carbonara.Services;
using Carbonara.Services.BlockParametersService;
using Carbonara.Services.CountryCo2EmissionService;
using Carbonara.Services.HashRatePerPoolService;
using Carbonara.Services.MiningHardwareService;
using Carbonara.Services.NetworkHashRateService;
using Carbonara.Services.PoolHashRateService;
using Moq;
using Xunit;

namespace Carbonara.Tests.ServiceTests
{
    public class CalculationServiceFixture
    {
        private readonly ICalculationService _calculationService;
        private string txHash = "x123";

        public CalculationServiceFixture()
        {
            var blockParametersServiceMock = new Mock<IBlockParametersService>();
            blockParametersServiceMock.Setup(x=> x.GetBlockParameters(txHash))
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
            networkHashRateServiceMock.Setup(x=> x.GetDailyHashRateInPastAsync(999999))
                .Returns(Task.FromResult(43141132m));

            var poolHashRateServiceMock = new Mock<IPoolHashRateService>();
            poolHashRateServiceMock.Setup(x=> x.GetPoolHashRateDistributionForTxDateAsync(999999))
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
            countryCo2EmissionServiceMock.Setup(x=> x.GetCountriesCo2EmissionAsync())
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
            miningHardwareServiceMock.Setup(x=> x.GetHardwareByAlgorithmAndYear(MiningAlgorithm.SHA256, 2013))
                .Returns(Task.FromResult
                (
                    new List<MiningDevice>() 
                    {
                        new MiningDevice { HashRate = 14000000000000, PowerConsumption = 1375 },
                    }
                )
            );
        
            var hashRatePerPoolServiceMock = new Mock<IHashRatePerPoolService>();
            hashRatePerPoolServiceMock.Setup(x=> x.GetHashRatePerPoolAsync())
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
                hashRatePerPoolServiceMock.Object
            );
        }

        [Fact]
        public async Task TesCalculate()
        {
            var result = await _calculationService.Calculate(txHash, null, null, null);
            Assert.Equal(192, Math.Round(result));
        }
    }
}
