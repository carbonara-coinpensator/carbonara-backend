using System.Threading.Tasks;
using Carbonara.Models;
using Carbonara.Services;
using Carbonara.Services.HttpClientHandler;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Carbonara.Providers
{
    public class BlockExplorerProvider : IBlockExplorerProvider
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientHandler _httpClient;

        public BlockExplorerProvider(IConfiguration configuration, IHttpClientHandler httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<TransactionDetails> GetTransactionDetailsAsync(string txHash)
        {
            var url = $"{_configuration["Api:BlockExplorer"]}/tx/{txHash}";
            var responseContent = await GetResponseContent(url);

            var transactionDetails = JsonConvert.DeserializeObject<TransactionDetails>(responseContent);

            return transactionDetails;
        }

        public async Task<BlockDetails> GetBlockDetailsAsync(string blockHash)
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