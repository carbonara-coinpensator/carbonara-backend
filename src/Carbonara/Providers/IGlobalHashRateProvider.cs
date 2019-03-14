using System.Threading.Tasks;

namespace Carbonara.Providers
{
    public interface IGlobalHashRateProvider
    {
        Task<decimal> GetDailyHashRateAsync(int blockTime);
    }
}