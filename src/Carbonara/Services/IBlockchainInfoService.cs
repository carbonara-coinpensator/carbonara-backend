using System.Threading.Tasks;

namespace Carbonara.Services
{
    public interface IBlockchainInfoService
    {
        Task<string> GetTransactionDetailsAsync(string txHash);
    }
}