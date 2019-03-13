using System;
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

        [HttpGet("tx")]
        public async Task<IActionResult> GetTransactionDetailsAsync(string tx)
        {
            var formulaParameters = await _blockchainInfoService.GetFormulaParametersAsync(tx);

            return Ok(formulaParameters);
        }
    }
}