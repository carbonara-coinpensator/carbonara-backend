using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Carbonara.Models;
using Carbonara.Models.MiningHardware;
using Carbonara.Providers;
using Carbonara.Services;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace Carbonara.Tests.ProviderTests
{
    public class BitcoinWalletProviderFixture
    {
        private readonly IBitcoinWalletProvider _bitcoinWalletProvider;
        private Mock<IConfiguration> ConfigurationMock = new Mock<IConfiguration>();

        public BitcoinWalletProviderFixture()
        {
            _bitcoinWalletProvider = new BitcoinWalletProvider(ConfigurationMock.Object, new Services.HttpClientHandler());

            ConfigurationMock.Setup(i => i["Api:BitcoinWalletAddressApi"]).Returns("https://blockchain.info/rawaddr");
        }

        [Fact(Skip = "integration")]
        public async Task TestGetInformation()
        {
            var result = await _bitcoinWalletProvider.GetInformation("1aa5cmqmvQq8YQTEqcTmW7dfBNuFwgdCD");
            Assert.NotEmpty(result.txs);
            Assert.All(result.txs, item => item.hash.Should().NotBeNullOrEmpty());
        }

        [Fact(Skip = "integration")]
        public async Task TestGetTransactions()
        {
            var txs = await _bitcoinWalletProvider.GetAllTransactionHashes("1HMwiTpDwBXaP7wKrLKPzuJ9S3szrBNgAS");
            Assert.NotEmpty(txs);
            Assert.All(txs, item => item.Should().NotBeNullOrEmpty());
        }

    }
}
