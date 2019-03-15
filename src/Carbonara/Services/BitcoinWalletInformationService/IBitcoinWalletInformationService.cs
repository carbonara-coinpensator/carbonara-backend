using System.Threading.Tasks;
using Carbonara.Models.BitcoinWalletInformation;

namespace Carbonara.Services.BitcoinWalletInformationService
{
    public interface IBitcoinWalletInformationService : IWalletInformationService
    {
        Task<BitcoinWalletInformation> GetInformation(string address);
    }
}