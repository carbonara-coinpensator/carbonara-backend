using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Carbonara.Models;
using Carbonara.Services;
using Microsoft.AspNetCore.Mvc;

namespace Carbonara.Controllers
{
    [Route("api/[controller]")]
    public class CarbonaraController : ControllerBase
    {
        private readonly IBlockParametersService _blockParametersService;
        private readonly INetworkHashRateService _networkHashRateService;

        public CarbonaraController(IBlockParametersService blockParametersService, INetworkHashRateService networkHashRateService)
        {
            _blockParametersService = blockParametersService;
            _networkHashRateService = networkHashRateService;
        }

        /// <summary>
        /// Get years selection for approximation of miners hashrate\energy consumption
        /// </summary>
        /// <response code="200">List of integers representing years</response>
        [HttpGet("MinningGearYearsSelection")]
        public async Task<IActionResult> GetMinningGearYearsSelection()
        {
            var years = await Task.FromResult(new List<int> { 2009, 2010, 2011, 2012, 2013, 2014, 2015, 2016, 2017, 2018, 2019 });
            return Ok(years);
        }

        /// <summary>
        /// Get energy standards selection for approximation of CO2 emmissions per KWH
        /// </summary>
        /// <response code="200">List of string representing countries with the approximate CO2 consumption</response>
        [HttpGet("CountriesCO2EmissionSelection")]
        public async Task<IActionResult> GetCountriesCO2EmissionSelection()
        {
            var countries = await Task.FromResult(new List<string> { "CN", "USA", "EU" });
            return Ok(countries);
        }

        /// <summary>
        /// Get the CO2 emission for a given transaction hash
        /// </summary>
        /// <param name="txHash">transaction hash</param>
        /// <param name="minningGearYear">(Optional) Year for which the minning gear hashrate\energy consumtion approximation should be taken into account </param>
        /// <param name="hashingAlgorithm">(Optional) Hashing alg to be used for the minning gear approximation</param>
        /// <param name="cO2EmissionCountry">(Optional) Country for which the CO2 emission per KWH appoximation should be taken into account</param>
        /// <response code="200">Formula parameters</response>
        [HttpGet("Calculate")]
        public async Task<IActionResult> GetFormulaParametersAsync(
            [FromQuery(Name = "TxHash")]string txHash,
            [FromQuery(Name = "MinningGearYear")]int? minningGearYear = null,
            [FromQuery(Name="HashingAlgorithm")]string hashingAlgorithm = "0",
            [FromQuery(Name = "CO2EmissionCountry")]string cO2EmissionCountry = null)
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