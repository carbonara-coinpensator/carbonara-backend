using System.Threading.Tasks;

namespace Carbonara.Services
{
    public interface INetworkHashRateService
    {
        Task<decimal> GetDailyHashRateInPastAsync(int blockTime);
    }
}