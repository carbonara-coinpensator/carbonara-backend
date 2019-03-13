using System.Threading.Tasks;
using Carbonara.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Carbonara.Services
{
    public class BlockchainInfoService : IBlockchainInfoService
    {
        private readonly IHttpClientHandler _httpClient;

        private readonly IConfiguration _configuration;

        public BlockchainInfoService(IHttpClientHandler httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;    
            _configuration = configuration;
        }

        public async Task<BlockDetails> GetBlockDetailsAsync(string blockHash)
        {
            var url = $"{_configuration["Api:BlockExplorer"]}/block/{blockHash}";
            var responseContent = await GetResponseContent(url);

            var blockDetails = JsonConvert.DeserializeObject<BlockDetails>(responseContent);

            return blockDetails;
        }

        public async Task<TransactionDetails> GetTransactionDetailsAsync(string txHash)
        {
            var url = $"{_configuration["Api:BlockExplorer"]}/tx/{txHash}";
            var responseContent = await GetResponseContent(url);

            var transactionDetails = JsonConvert.DeserializeObject<TransactionDetails>(responseContent);

            return transactionDetails;
        }

        private async Task<string> GetResponseContent(string url)
        {
            var response = await _httpClient.GetAsync(url);

            var responseContent = await response.Content.ReadAsStringAsync();
            return responseContent;
        }
    }
}