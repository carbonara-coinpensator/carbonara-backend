using System.Threading.Tasks;
using Carbonara.Models.Formula;
using Carbonara.Providers;

namespace Carbonara.Services
{
    public class BlockParametersService : IBlockParametersService
    {
        private readonly IBlockExplorerProvider _blockExplorerProvider;

        public BlockParametersService(IBlockExplorerProvider blockExplorerProvider)
        {
            _blockExplorerProvider = blockExplorerProvider;
        }

        public async Task<BlockParameters> GetBlockParameters(string txHash)
        {
            var transactionDetails = await _blockExplorerProvider.GetTransactionDetailsAsync(txHash);

            var blockDetails = await _blockExplorerProvider.GetBlockDetailsAsync(transactionDetails.data.blockhash);

            var previousBlockDetails = await _blockExplorerProvider.GetBlockDetailsAsync(blockDetails.data.previous_blockhash);

            var blockTimeInSeconds = blockDetails.data.time - previousBlockDetails.data.time;

            return new BlockParameters
            {
                NumberOfTransactionsInBlock = blockDetails.data.txs.Count,
                BlockTimeInSeconds = blockTimeInSeconds,
                TimeOfBlockMining = blockDetails.data.time
            };
        }
    }
}