using System;
using System.Threading.Tasks;
using Carbonara.Models.Formula;
using Carbonara.Providers;

namespace Carbonara.Services.BlockParametersService
{
    public class BlockParametersService : IBlockParametersService
    {
        private readonly IBlockExplorerProvider _blockExplorerProvider;

        public BlockParametersService(IBlockExplorerProvider blockExplorerProvider)
        {
            _blockExplorerProvider = blockExplorerProvider;
        }

        public async Task<BlockParameters> GetBlockParametersByTxHash(string txHash)
        {
            var transactionDetails = await _blockExplorerProvider.GetTransactionDetailsAsync(txHash);

            return await GetBlockParameters(transactionDetails.data.blockhash);
        }

        public async Task<BlockParameters> GetBlockParametersByBlockHash(string blockHash)
        {
            return await GetBlockParameters(blockHash);
        }

        private async Task<BlockParameters> GetBlockParameters(string blockHash)
        {
            var blockDetails = await _blockExplorerProvider.GetBlockDetailsAsync(blockHash);

            var previousBlockDetails = await _blockExplorerProvider.GetBlockDetailsAsync(blockDetails.data.previous_blockhash);

            var blockTimeInSeconds = Math.Abs(blockDetails.data.time - previousBlockDetails.data.time);

            return new BlockParameters
            {
                NumberOfTransactionsInBlock = blockDetails.data.txs.Count,
                BlockTimeInSeconds = blockTimeInSeconds,
                TimeOfBlockMining = blockDetails.data.time
            };
        }
    }
}