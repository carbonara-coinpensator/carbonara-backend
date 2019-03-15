
using System.Threading.Tasks;
using Carbonara.Models;
using Carbonara.Providers.WalletProvider;

namespace Carbonara.Providers.BitcoinWalletProvider
{
    public interface IBitcoinWalletProvider : IWalletProvider
    {
        Task<BitcoinWalletInformation> GetInformation(string address);
    }
}