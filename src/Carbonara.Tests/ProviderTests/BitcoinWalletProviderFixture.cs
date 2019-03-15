using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Carbonara.Models;
using Carbonara.Models.MiningHardware;
using Carbonara.Providers;
using Carbonara.Providers.BitcoinWalletProvider;
using Carbonara.Services;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace Carbonara.Tests.ProviderTests
{
    /// <summary>
    /// Integration Tests to the BitcoinWalletProvider.await These tests run against the real API!
    /// </summary>
    public class BitcoinWalletProviderFixture
    {
        private readonly IBitcoinWalletProvider _bitcoinWalletProvider;
        public BitcoinWalletProviderFixture()
        {
            _bitcoinWalletProvider = new BitcoinWalletProvider(new Services.HttpClientHandler.HttpClientHandler());
        }

        [Fact]
        public async Task TestGetInformation()
        {
            var result = await _bitcoinWalletProvider.GetInformation("1aa5cmqmvQq8YQTEqcTmW7dfBNuFwgdCD");
            Assert.NotEmpty(result.data.txs);
            Assert.All(result.data.txs, item => item.txid.Should().NotBeNullOrEmpty());
        }

        [Fact]
        public async Task TestGetTransactions()
        {
            var txs = await _bitcoinWalletProvider.GetAllTransactionHashes("1HMwiTpDwBXaP7wKrLKPzuJ9S3szrBNgAS");
            Assert.NotEmpty(txs);
            Assert.All(txs, item => item.Should().NotBeNullOrEmpty());
        }

    }
}
