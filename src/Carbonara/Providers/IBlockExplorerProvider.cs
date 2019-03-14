using System.Threading.Tasks;
using Carbonara.Models.BlockDetails;
using Carbonara.Models.TransactionDetails;

namespace Carbonara.Providers
{
    public interface IBlockExplorerProvider
    {
        Task<TransactionDetails> GetTransactionDetailsAsync(string txHash);
        Task<BlockDetails> GetBlockDetailsAsync(string blockHash);
    }
}