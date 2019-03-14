using System.Collections.Generic;
using System.Threading.Tasks;
using Carbonara.Models;
using Carbonara.Providers.WalletProvider;
using Carbonara.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Carbonara.Providers.BitcoinWalletProvider
{
    public interface IBitcoinWalletProvider : IWalletProvider
    {
        Task<BitcoinWalletInformation> GetInformation(string address);
    }
}