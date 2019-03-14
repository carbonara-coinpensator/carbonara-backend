
using System.Collections.Generic;
using System.Threading.Tasks;
using Carbonara.Models;

namespace Carbonara.Services.BitcoinWalletInformationService
{
    public interface IBitcoinWalletInformationService : IWalletInformationService
    {
        Task<BitcoinWalletInformation> GetInformation(string address);
    }
}