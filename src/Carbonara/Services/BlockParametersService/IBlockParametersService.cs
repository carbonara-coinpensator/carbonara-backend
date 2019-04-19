using System.Threading.Tasks;
using Carbonara.Models.Formula;

namespace Carbonara.Services.BlockParametersService
{
    public interface IBlockParametersService
    {
        Task<BlockParameters> GetBlockParametersByTxHash(string txHash);
        Task<BlockParameters> GetBlockParametersByBlockHash(string blockHash);
    }
}