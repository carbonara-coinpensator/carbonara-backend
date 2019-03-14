using System.Collections.Generic;
using System.Threading.Tasks;
using Carbonara.Models;
using Carbonara.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Carbonara.Providers
{
    public interface IBitcoinWalletProvider
    {
        Task<BitcoinWalletInformation> GetInformation(string address);
        Task<List<string>> GetAllTransactionHashes(string address);
    }
}