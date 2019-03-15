using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carbonara.Models;
using Carbonara.Services;
using Carbonara.Services.HttpClientHandler;
using Newtonsoft.Json;

namespace Carbonara.Providers.BitcoinWalletProvider
{
    public class BitcoinWalletProvider : BaseHttpProvider, IBitcoinWalletProvider
    {
        protected override string Endpoint => "https://blockchain.info/rawaddr";

        public BitcoinWalletProvider(IHttpClientHandler httpClient)
            : base(httpClient)
        {
        }

        public async Task<BitcoinWalletInformation> GetInformation(string address)
        {
            var url = $"{Endpoint}/{address}";
            return await GetResponseAndDeserialize<BitcoinWalletInformation>(url);
        }

        public async Task<List<string>> GetAllTransactionHashes(string address)
        {
            var walletInformation = await GetInformation(address);
            return walletInformation.txs.Select(i => i.hash).ToList();
        }
    }
}