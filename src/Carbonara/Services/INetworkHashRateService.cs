using System.Threading.Tasks;

namespace Carbonara.Services
{
    public interface INetworkHashRateService
    {
        Task<double> GetDailyHashRateInPastAsync(int blockTime);
    }
}