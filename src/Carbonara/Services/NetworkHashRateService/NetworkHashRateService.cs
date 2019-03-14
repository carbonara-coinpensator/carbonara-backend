using System;
using System.Threading.Tasks;
using Carbonara.Providers;

namespace Carbonara.Services.NetworkHashRateService
{
    public class NetworkHashRateService : INetworkHashRateService
    {
        private readonly IGlobalHashRateProvider _globalHashRateProvider;

        public NetworkHashRateService(IGlobalHashRateProvider globalHashRateProvider)
        {
            _globalHashRateProvider = globalHashRateProvider;
        }

        public async Task<decimal> GetDailyHashRateInPastAsync(int blockTime)
        {        
            var dateOfTransaction = DateTime.UnixEpoch.AddSeconds(blockTime);
            var dateDiff = DateTime.Now - dateOfTransaction;

            return await _globalHashRateProvider.GetDailyHashRateAsync(dateDiff.Days + 1);
        }
    }
}