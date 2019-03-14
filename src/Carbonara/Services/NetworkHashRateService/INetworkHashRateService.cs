using System.Threading.Tasks;

namespace Carbonara.Services.NetworkHashRateService
{
    public interface INetworkHashRateService
    {
        Task<decimal> GetDailyHashRateInPastAsync(int blockTime);
    }
}