using System;
using System.Threading.Tasks;
using Carbonara.Models;
using Carbonara.Services;
using Microsoft.AspNetCore.Mvc;

namespace Carbonara.Controllers
{
    [Route("api/[controller]")]
    public class BlockchainInfoController : ControllerBase
    {
        private readonly IBlockParametersService _blockParametersService;
        private readonly INetworkHashRateService _networkHashRateService;

        public BlockchainInfoController(IBlockParametersService blockParametersService, INetworkHashRateService networkHashRateService)
        {
            _blockParametersService = blockParametersService;
            _networkHashRateService = networkHashRateService;
        }

        /// <summary>
        /// Get parameters for formula of given transaction
        /// </summary>
        /// <param name="txHash">transaction hash</param>
        /// <response code="200">Formula parameters</response>
        [HttpGet("txHash")]
        public async Task<IActionResult> GetFormulaParametersAsync(string txHash)
        {
            var blockParameters = await _blockParametersService.GetBlockParameters(txHash);
            var hashRate = await _networkHashRateService.GetDailyHashRateInPastAsync(blockParameters.BlockTimeInSeconds);

            var formulaParameters = new FormulaParameters
            {
                BlockTimeInSeconds = blockParameters.BlockTimeInSeconds,
                HashRateOfDayTxWasMined = hashRate,
                NumberOfTransactionsInBlock = blockParameters.NumberOfTransactionsInBlock
            };

            return Ok(formulaParameters);
        }
    }
}