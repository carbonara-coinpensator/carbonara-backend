using System.Threading.Tasks;
using Carbonara.Models.Formula;

namespace Carbonara.Services
{
    public interface IBlockParametersService
    {
        Task<BlockParameters> GetBlockParameters(string txHash);
    }
}