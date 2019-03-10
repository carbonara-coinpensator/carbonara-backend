using System.Threading.Tasks;
using Carbonara.Models;
using Newtonsoft.Json;

namespace Carbonara.Services
{
    public class BlockchainInfoService : IBlockchainInfoService
    {
        private readonly IHttpClientHandler _httpClient;

        public BlockchainInfoService(IHttpClientHandler httpClient)
        {
            _httpClient = httpClient;    
        }

        public async Task<TransactionDetails> GetTransactionDetailsAsync(string txHash)
        {
            var url = $"https://blockchain.info/rawtx/{txHash}";

            var response = await _httpClient.GetAsync(url);

            var responseContent = await response.Content.ReadAsStringAsync();

            var transactionDetails = JsonConvert.DeserializeObject<TransactionDetails>(responseContent);

            return transactionDetails;
        }
    }
}