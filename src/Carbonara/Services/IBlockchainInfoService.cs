using System.Threading.Tasks;
using Carbonara.Models;

namespace Carbonara.Services
{
    public interface IBlockchainInfoService
    {
        Task<TransactionDetails> GetTransactionDetailsAsync(string txHash);
    }
}