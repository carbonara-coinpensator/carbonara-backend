using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carbonara.Models.BitcoinWalletInformation;
using Carbonara.Services.HttpClientHandler;

namespace Carbonara.Providers.BitcoinWalletProvider
{
    public class BitcoinWalletProvider : BaseHttpProvider, IBitcoinWalletProvider
    {
        protected override string Endpoint => "https://chain.so/api/v2/get_tx_spent/btc";

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
            return walletInformation.data.txs.Select(i => i.txid).ToList();
        }
    }
}