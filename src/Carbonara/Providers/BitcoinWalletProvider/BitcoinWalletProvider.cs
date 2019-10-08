using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carbonara.Models.BitcoinWalletInformation;
using Carbonara.Providers.BaseProviders;
using Carbonara.Services.HttpClientHandler;

namespace Carbonara.Providers.BitcoinWalletProvider
{
    public class BitcoinWalletProvider : BaseCloudflareHttpProvider, IBitcoinWalletProvider
    {
        protected override string Endpoint => "https://chain.so/api/v2/get_tx_spent/btc";

        public BitcoinWalletProvider(ICloudFlareHttpClientHandler cloudFlareHttpClientHandler)
            : base(cloudFlareHttpClientHandler)
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
            return walletInformation.data.txs.Select(i => i.txid).ToList();
        }
    }
}