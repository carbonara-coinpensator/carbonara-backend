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

            var blockDetails = await _blockExplorerProvider.GetBlockDetailsAsync(transactionDetails.blockhash);

            var previousBlockDetails = await _blockExplorerProvider.GetBlockDetailsAsync(blockDetails.previousblockhash);

            var blockTimeInSeconds = (blockDetails.time - previousBlockDetails.time) * 60;

            return new BlockParameters
            {
                NumberOfTransactionsInBlock = blockDetails.tx.Count,
                BlockTimeInSeconds = blockTimeInSeconds,
                TimeOfBlockMining = blockDetails.time
            };
        }
    }
}