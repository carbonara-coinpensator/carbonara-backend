using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Carbonara.Models.MiningHardware;
using Carbonara.Providers;
using Carbonara.Services;
using Xunit;

namespace Carbonara.Tests
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
        [InlineData(0, 2018)]
        [InlineData(0, 2013)]
        public async Task TestGetByAlgorithmAndYear(int algo, int year)
        {
            var algorithm = (MiningAlgorithm)algo;
            var result = await _jsonHardwareProvider.GetHardwareByAlgorithmAndYear(algorithm, year);
            Assert.NotEmpty(result);
            Assert.All(result, item => Assert.True(item.Algorithm == algorithm && item.ProductionYear == year));
        }

        [Theory]
        [InlineData(1, 2018)]
        public async Task TestGetByAlgorithmAndYearReturnsEmpty(int algo, int year)
        {
            var algorithm = (MiningAlgorithm)algo;
            var result = await _jsonHardwareProvider.GetHardwareByAlgorithmAndYear(algorithm, year);
            Assert.Empty(result);
        }

        [Theory]
        [InlineData(0)]
        public async Task TestGetByAlgorithm(int algo)
        {
            var algorithm = (MiningAlgorithm)algo;
            var result = await _jsonHardwareProvider.GetHardwareByMiningAlgorithm(algorithm);
            Assert.NotEmpty(result);
            Assert.All(result, item => Assert.True(item.Algorithm == algorithm));
        }

        [Theory]
        [InlineData(1)]
        public async Task TestGetByAlgorithmReturnsEMpty(int algo)
        {
            var algorithm = (MiningAlgorithm)algo;
            var result = await _jsonHardwareProvider.GetHardwareByMiningAlgorithm(algorithm);
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
