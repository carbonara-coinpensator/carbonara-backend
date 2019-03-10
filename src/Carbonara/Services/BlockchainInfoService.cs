using System.Threading.Tasks;

namespace Carbonara.Services
{
    public class BlockchainInfoService : IBlockchainInfoService
    {
        private readonly IHttpClientHandler _httpClient;

        public BlockchainInfoService(IHttpClientHandler httpClient)
        {
            _httpClient = httpClient;    
        }

        public async Task<string> GetTransactionDetailsAsync(string txHash)
        {
            var url = $"https://blockchain.info/rawtx/{txHash}";

            var response = await _httpClient.GetAsync(url);

            var responseContent = await response.Content.ReadAsStringAsync();

            return responseContent;
        }
    }
}