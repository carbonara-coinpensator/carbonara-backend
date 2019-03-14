using System.Threading.Tasks;
using Carbonara.Models.Formula;

namespace Carbonara.Services.BlockParametersService
{
    public interface IBlockParametersService
    {
        Task<BlockParameters> GetBlockParameters(string txHash);
    }
}