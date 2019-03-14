using System.Collections.Generic;
using System.Threading.Tasks;
using Carbonara.Models;
using Carbonara.Models.Formula;
using Carbonara.Providers;

namespace Carbonara.Services
{
    public class BitcoinWalletInformationService : IBitcoinWalletInformationService
    {
        private readonly IBitcoinWalletProvider _bitcoinWalletProvider;

        public BitcoinWalletInformationService(IBitcoinWalletProvider bitcoinWalletProvider)
        {
            _bitcoinWalletProvider = bitcoinWalletProvider;
        }

        public async Task<List<string>> GetAllTransactionHashes(string address)
        {
            return await _bitcoinWalletProvider.GetAllTransactionHashes(address);
        }

        public async Task<BitcoinWalletInformation> GetInformation(string address)
        {
            return await _bitcoinWalletProvider.GetInformation(address);
        }
    }
}