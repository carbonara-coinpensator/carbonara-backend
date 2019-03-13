using System.Threading.Tasks;

namespace Carbonara.Providers
{
    public interface IGlobalHashRateProvider
    {
        Task<double> GetDailyHashRateAsync(int blockTime);
    }
}