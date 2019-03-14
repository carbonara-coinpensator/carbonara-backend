using System.Collections.Generic;
using System.Threading.Tasks;
using Carbonara.Models;
using Carbonara.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Carbonara.Providers.WalletProvider
{
    public interface IWalletProvider
    {
        Task<List<string>> GetAllTransactionHashes(string address);
    }
}