using System;
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

        public async Task<FormulaParameters> GetFormulaParametersAsync(string txHash)
        {
            var transactionDetails = await GetTransactionDetailsAsync(txHash);

            var blockDetails = await GetBlockDetailsAsync(transactionDetails.blockhash);

            var previousBlockDetails = await GetBlockDetailsAsync(blockDetails.previousblockhash);

            var blockTimeInSeconds = (blockDetails.time - previousBlockDetails.time) * 60;

            var hashRateOfDayTxWasMined = await GetDailyHashRateInPastAsync(blockDetails.time);

            return new FormulaParameters
            {
                BlockTimeInSeconds = blockTimeInSeconds,
                HashRateOfDayTxWasMined = hashRateOfDayTxWasMined,
                NumberOfTransactionsInBlock = blockDetails.tx.Count
            };
        }

        private async Task<BlockDetails> GetBlockDetailsAsync(string blockHash)
        {
            var url = $"{_configuration["Api:BlockExplorer"]}/block/{blockHash}";
            var responseContent = await GetResponseContent(url);

            var blockDetails = JsonConvert.DeserializeObject<BlockDetails>(responseContent);

            return blockDetails;
        }

        private async Task<TransactionDetails> GetTransactionDetailsAsync(string txHash)
        {
            var url = $"{_configuration["Api:BlockExplorer"]}/tx/{txHash}";
            var responseContent = await GetResponseContent(url);

            var transactionDetails = JsonConvert.DeserializeObject<TransactionDetails>(responseContent);

            return transactionDetails;
        }

        private async Task<double> GetDailyHashRateInPastAsync(int blockTime)
        {
            var dateOfTransaction = DateTime.UnixEpoch.AddSeconds(blockTime);
            var dateDiff = DateTime.Now - dateOfTransaction;

            var url = $"{_configuration["Api:GlobalHashRate"]}?timespan={dateDiff.Days + 1}days&format=json";
            var responseContent = await GetResponseContent(url);

            var hashRate = JsonConvert.DeserializeObject<GlobalHashRate>(responseContent);
            var hashRateOfFirstDayInPeriod = hashRate.values[0].y;

            return hashRateOfFirstDayInPeriod;
        }

        private async Task<int> GetTxBlockTimeInSecondsAsync(string blockHash)
        {
            var blockDetails = await GetBlockDetailsAsync(blockHash);

            var previousBlockDetails = await GetBlockDetailsAsync(blockDetails.previousblockhash);

            return (blockDetails.time - previousBlockDetails.time) * 60;
        }

        private async Task<string> GetResponseContent(string url)
        {
            var response = await _httpClient.GetAsync(url);

            var responseContent = await response.Content.ReadAsStringAsync();
            return responseContent;
        }
    }
}