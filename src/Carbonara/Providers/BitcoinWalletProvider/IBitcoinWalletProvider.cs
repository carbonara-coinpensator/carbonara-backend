
using System.Threading.Tasks;
using Carbonara.Models.BitcoinWalletInformation;
using Carbonara.Providers.WalletProvider;

namespace Carbonara.Providers.BitcoinWalletProvider
{
    public interface IBitcoinWalletProvider : IWalletProvider
    {
        Task<BitcoinWalletInformation> GetInformation(string address);
    }
}