using System.Threading.Tasks;
using Carbonara.Services;
using Microsoft.AspNetCore.Mvc;

namespace Carbonara.Controllers
{
    [Route("api/[controller]")]
    public class BlockchainInfoController : ControllerBase
    {
        private readonly IBlockchainInfoService _blockchainInfoService;

        public BlockchainInfoController(IBlockchainInfoService blockchainInfoService)
        {
            _blockchainInfoService = blockchainInfoService;
        }

        [HttpGet("{tx}")]
        public async Task<ActionResult<string>> GetTransactionDetailsAsync(string tx)
        {
            var result = await _blockchainInfoService.GetTransactionDetailsAsync(tx);
            return Ok();
        }
    }
}