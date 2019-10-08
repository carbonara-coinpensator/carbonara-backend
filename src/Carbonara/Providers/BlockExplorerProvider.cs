using Carbonara.Models.BlockDetails;
using Carbonara.Models.TransactionDetails;
using Carbonara.Providers.BaseProviders;
using Carbonara.Services.HttpClientHandler;
using System.Threading.Tasks;

namespace Carbonara.Providers
{
    public class BlockExplorerProvider : BaseCloudflareHttpProvider, IBlockExplorerProvider
    {
        protected override string Endpoint => "https://chain.so/api/v2";

        public BlockExplorerProvider(ICloudFlareHttpClientHandler httpClient)
            : base(httpClient)
        {
        }

        public async Task<TransactionDetails> GetTransactionDetailsAsync(string txHash)
        {
            var url = $"{Endpoint}/get_tx/btc/{txHash}";
            return await GetResponseAndDeserialize<TransactionDetails>(url);
        }

        public async Task<BlockDetails> GetBlockDetailsAsync(string blockHash)
        {
            var url = $"{Endpoint}/get_block/btc/{blockHash}";
            return await GetResponseAndDeserialize<BlockDetails>(url);
        }
    }
}