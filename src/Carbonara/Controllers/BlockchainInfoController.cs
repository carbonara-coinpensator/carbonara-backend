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
            var transactionDetails = await _blockchainInfoService.GetTransactionDetailsAsync(tx);

            var blockDetails = await _blockchainInfoService.GetBlockDetailsAsync(transactionDetails.blockhash);

            var previousBlockDetails = await _blockchainInfoService.GetBlockDetailsAsync(blockDetails.previousblockhash);

            var timeBetweenBlocksInSeconds = (blockDetails.time - previousBlockDetails.time) * 60;

            var timeOfBlockDate = TimeSpan.FromSeconds(blockDetails.time);

            var dateOfTransaction = DateTime.UnixEpoch.AddSeconds(blockDetails.time);
            var dateDiff = DateTime.Now - dateOfTransaction;

            var hashRateOfDayTxWasMined = await _blockchainInfoService.GetDailyHashRateInPastAsync(dateDiff.Days + 1);

            return Ok(transactionDetails);
        }
    }
}