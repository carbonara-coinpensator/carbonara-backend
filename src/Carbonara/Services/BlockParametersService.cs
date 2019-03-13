using System.Threading.Tasks;
using Carbonara.Models;
using Carbonara.Models.Formula;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Carbonara.Services
{
    public class BlockParametersService : IBlockParametersService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientHandler _httpClient;

        public BlockParametersService(IConfiguration configuration, IHttpClientHandler httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<BlockParameters> GetBlockParameters(string txHash)
        {
            var transactionDetails = await GetTransactionDetailsAsync(txHash);

            var blockDetails = await GetBlockDetailsAsync(transactionDetails.blockhash);

            var previousBlockDetails = await GetBlockDetailsAsync(blockDetails.previousblockhash);

            var blockTimeInSeconds = (blockDetails.time - previousBlockDetails.time) * 60;

            return new BlockParameters
            {
                NumberOfTransactionsInBlock = blockDetails.tx.Count,
                BlockTimeInSeconds = blockTimeInSeconds
            };
        }

        private async Task<TransactionDetails> GetTransactionDetailsAsync(string txHash)
        {
            var url = $"{_configuration["Api:BlockExplorer"]}/tx/{txHash}";
            var responseContent = await GetResponseContent(url);

            var transactionDetails = JsonConvert.DeserializeObject<TransactionDetails>(responseContent);

            return transactionDetails;
        }

        private async Task<BlockDetails> GetBlockDetailsAsync(string blockHash)
        {
            var url = $"{_configuration["Api:BlockExplorer"]}/block/{blockHash}";
            var responseContent = await GetResponseContent(url);

            var blockDetails = JsonConvert.DeserializeObject<BlockDetails>(responseContent);

            return blockDetails;
        }

        private async Task<string> GetResponseContent(string url)
        {
            var response = await _httpClient.GetAsync(url);

            var responseContent = await response.Content.ReadAsStringAsync();
            return responseContent;
        }
    }
}