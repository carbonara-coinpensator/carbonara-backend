using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Carbonara.Models.MiningHardware;
using Carbonara.Providers;
using Carbonara.Services;
using Xunit;

namespace Carbonara.Tests.ProviderTests
{
    public class HardwareProviderFixture
    {
        private readonly IHardwareProvider _hardwareProvider;

        public HardwareProviderFixture()
        {
            _hardwareProvider = new HardwareProvider();
        }

        [Fact]
        public async Task TestGetAll()
        {
            var result = await _hardwareProvider.GetAll();
            Assert.All(result, item => Assert.True(item.HashRate > 0));
        }

        [Theory]
        [InlineData(MiningAlgorithm.SHA256, 2018)]
        [InlineData(MiningAlgorithm.SHA256, 2013)]
        public async Task TestGetByAlgorithmAndYear(MiningAlgorithm algo, int year)
        {
            var result = await _hardwareProvider.GetHardwareByAlgorithmAndYear(algo, year);
            Assert.NotEmpty(result);
            Assert.All(result, item => Assert.True(item.Algorithm == algo && item.ProductionYear == year));
        }

        [Theory]
        [InlineData(MiningAlgorithm.SCRYPT, 2018)]
        public async Task TestGetByAlgorithmAndYearReturnsEmpty(MiningAlgorithm algo, int year)
        {
            var result = await _hardwareProvider.GetHardwareByAlgorithmAndYear(algo, year);
            Assert.Empty(result);
        }

        [Theory]
        [InlineData(MiningAlgorithm.SHA256)]
        public async Task TestGetByAlgorithm(MiningAlgorithm algo)
        {
            var result = await _hardwareProvider.GetHardwareByMiningAlgorithm(algo);
            Assert.NotEmpty(result);
            Assert.All(result, item => Assert.True(item.Algorithm == algo));
        }

        [Theory]
        [InlineData(MiningAlgorithm.SCRYPT)]
        public async Task TestGetByAlgorithmReturnsEMpty(MiningAlgorithm algo)
        {
            var result = await _hardwareProvider.GetHardwareByMiningAlgorithm(algo);
            Assert.Empty(result);
        }

        [Fact]
        public async Task TestGetAllYears()
        {
            var result = await _hardwareProvider.GetAvailableYears();
            Assert.NotEmpty(result);
            Assert.All(result, item => Assert.True(item > 2010));
        }

    }
}
