using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carbonara.Models;
using Carbonara.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Carbonara.Providers
{
    public class BitcoinWalletProvider : IBitcoinWalletProvider
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientHandler _httpClient;

        public BitcoinWalletProvider(IConfiguration configuration, IHttpClientHandler httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<BitcoinWalletInformation> GetInformation(string address)
        {
            string responseContent = await GetWalletInformationResponse(address);
            return JsonConvert.DeserializeObject<BitcoinWalletInformation>(responseContent);
        }

        public async Task<List<string>> GetAllTransactionHashes(string address)
        {
            var walletInformation = await GetInformation(address);
            return walletInformation.txs.Select(i => i.hash).ToList();
        }

        private async Task<string> GetResponseContent(string url)
        {
            var response = await _httpClient.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }        

        private async Task<string> GetWalletInformationResponse(string address)
        {
            var url = $"{_configuration["Api:BitcoinWalletAddressApi"]}/{address}";
            return await GetResponseContent(url);
        }
    }
}