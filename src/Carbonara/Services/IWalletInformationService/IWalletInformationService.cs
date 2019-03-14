
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Carbonara.Services
{
    public interface IWalletInformationService
    {
        Task<List<string>> GetAllTransactionHashes(string address);
    }
}