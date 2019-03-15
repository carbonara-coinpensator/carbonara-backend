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
    public class JsonHardwareProviderFixture
    {
        private readonly IJsonHardwareProvider _jsonHardwareProvider;

        public JsonHardwareProviderFixture()
        {
            _jsonHardwareProvider = new JsonHardwareProvider();
        }

        [Fact]
        public async Task TestGetAll()
        {
            var result = await _jsonHardwareProvider.GetAll();
            Assert.All(result, item => Assert.True(item.HashRate > 0));
        }

        [Theory]
        [InlineData(MiningAlgorithm.SHA256, 2018)]
        [InlineData(MiningAlgorithm.SHA256, 2013)]
        public async Task TestGetByAlgorithmAndYear(MiningAlgorithm algo, int year)
        {
            var result = await _jsonHardwareProvider.GetHardwareByAlgorithmAndYear(algo, year);
            Assert.NotEmpty(result);
            Assert.All(result, item => Assert.True(item.Algorithm == algo && item.ProductionYear == year));
        }

        [Theory]
        [InlineData(MiningAlgorithm.SCRYPT, 2018)]
        public async Task TestGetByAlgorithmAndYearReturnsEmpty(MiningAlgorithm algo, int year)
        {
            var result = await _jsonHardwareProvider.GetHardwareByAlgorithmAndYear(algo, year);
            Assert.Empty(result);
        }

        [Theory]
        [InlineData(MiningAlgorithm.SHA256)]
        public async Task TestGetByAlgorithm(MiningAlgorithm algo)
        {
            var result = await _jsonHardwareProvider.GetHardwareByMiningAlgorithm(algo);
            Assert.NotEmpty(result);
            Assert.All(result, item => Assert.True(item.Algorithm == algo));
        }

        [Theory]
        [InlineData(MiningAlgorithm.SCRYPT)]
        public async Task TestGetByAlgorithmReturnsEMpty(MiningAlgorithm algo)
        {
            var result = await _jsonHardwareProvider.GetHardwareByMiningAlgorithm(algo);
            Assert.Empty(result);
        }

        [Fact]
        public async Task TestGetAllYears()
        {
            var result = await _jsonHardwareProvider.GetAvailableYears();
            Assert.NotEmpty(result);
            Assert.All(result, item => Assert.True(item > 2010));
        }

    }
}
