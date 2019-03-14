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

        /// <summary>
        /// Get parameters for formula of given transaction
        /// </summary>
        /// <param name="txHash">transaction hash</param>
        /// <response code="200">Formula parameters</response>
        [HttpGet("txHash")]
        public async Task<IActionResult> GetFormulaParametersAsync(string txHash)
        {
            var formulaParameters = await _blockchainInfoService.GetFormulaParametersAsync(txHash);

            return Ok(formulaParameters);
        }

        /// <summary>
        /// TEST
        /// </summary>
        /// <param name="txHash">transaction hash</param>
        /// <response code="200">Formula parameters</response>
        [HttpGet("hardware")]
        public async Task<IActionResult> GetHardware()
        {
            var formulaParameters = await _blockchainInfoService.GetFormulaParametersAsync(txHash);

            return Ok(formulaParameters);
        }
    }
}