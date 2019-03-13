using System.Threading.Tasks;
using Carbonara.Models;

namespace Carbonara.Providers
{
    public interface IBlockExplorerProvider
    {
        Task<TransactionDetails> GetTransactionDetailsAsync(string txHash);
        Task<BlockDetails> GetBlockDetailsAsync(string blockHash);
    }
}