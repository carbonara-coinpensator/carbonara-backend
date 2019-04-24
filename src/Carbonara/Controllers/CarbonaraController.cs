using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carbonara.Models;
using Carbonara.Models.MiningHardware;
using Carbonara.Services;
using Carbonara.Services.ChartService;
using Carbonara.Services.CalculationService;
using Carbonara.Services.MiningHardwareService;
using Microsoft.AspNetCore.Mvc;
using Carbonara.Services.BitcoinWalletInformationService;

namespace Carbonara.Controllers
{
    [Route("api/[controller]")]
    public class CarbonaraController : ControllerBase
    {
        private readonly ICalculationService _calculationService;
        private readonly IMiningHardwareService _miningHardwareService;
        private readonly IChartService _chartService;
        private readonly IBitcoinWalletInformationService _bitcoinWalletInformationService;

        public CarbonaraController(
            ICalculationService calculationService,
            IMiningHardwareService miningHardwareService,
            IChartService chartService,
            IBitcoinWalletInformationService bitcoinWalletInformationService)
        {
            _calculationService = calculationService;
            _miningHardwareService = miningHardwareService;
            _chartService = chartService;
            _bitcoinWalletInformationService = bitcoinWalletInformationService;
        }

        /// <summary>
        /// Get years selection for approximation of miners hashrate\energy consumption
        /// </summary>
        /// <response code="200">List of integers representing years</response>
        [HttpGet("MinningGearYearsSelection")]
        public async Task<IActionResult> GetMinningGearYearsSelection()
        {
            var years = await _miningHardwareService.GetAvailableYears();
            return Ok(years);
        }

        /// <summary>
        /// Get energy standards selection for approximation of CO2 emmissions per KWH
        /// </summary>
        /// <response code="200">List of strings representing countries with the approximate CO2 consumption</response>
        [HttpGet("CountriesCO2EmissionSelection")]
        public async Task<IActionResult> GetCountriesCO2EmissionSelection()
        {
            var countries = await Task.FromResult(new List<string> { "CN", "US", "EU" });
            return Ok(countries);
        }

        /// <summary>
        /// Get the list of transaction hashes for a given bitcoin address
        /// </summary>
        /// <param name="bitcoinAddress">Bitcoin address for which to provide the hashes (Address can be base58 or hash160)</param>
        /// <response code="200">List of strings representing tx hashes for a given address</response>
        [HttpGet("TransactionList")]
        public async Task<IActionResult> GetBitcoinTransactionsFromWalletAddress(
            [FromQuery(Name = "BitcoinAddress")]string bitcoinAddress)
        {

            var transactions = await _bitcoinWalletInformationService.GetInformation(bitcoinAddress);
            return Ok(transactions.data.txs);
        }

        /// <summary>
        /// Get the CO2 emission for a given transaction hash
        /// </summary>
        /// <param name="txHash">transaction hash</param>
        /// <param name="hashingAlgorithm">(Optional) Hashing alg to be used for the minning gear approximation.
        /// Currently ignored and defaults to SHA256 </param>
        /// <param name="cO2EmissionCountry">(Optional) Country for which the CO2 emission per KWH appoximation should be taken into account.</param>
        /// <response code="200">Returns an approximation of the CO2 emmission in KG for the given transaction hash as well as the energy consumption
        /// per regions and average CO2 emission per region </response>
        [HttpGet("Calculation")]
        public async Task<IActionResult> GetCalculationAsync(
            [FromQuery(Name = "TxHash")]string txHash,
            [FromQuery(Name = "HashingAlgorithm")]string hashingAlgorithm = "0",
            [FromQuery(Name = "CO2EmissionCountry")]string cO2EmissionCountry = null)
        {
            var result = await _calculationService.Calculate(txHash, hashingAlgorithm, cO2EmissionCountry);
            return Ok(result);
        }

        [HttpGet("Block/Calculation")]
        public async Task<IActionResult> GetEnergyConsumptionOfBlock([FromQuery(Name = "BlockHash")] string blockHash)
        {
            var energyConsumption = await _calculationService.CalculateBlockEnergyConsumptionAsync(blockHash);
            return Ok(energyConsumption);
        }

        /// <summary>
        /// Get bitcoin co2 emission and price chart
        /// </summary>
        /// <response code="200">Returns co2 emission and price chart </response>
        [HttpGet("Charts")]
        public async Task<IActionResult> GetBitcoinChartsAsync()
        {
            var bitcoinCharts = await _chartService.GetBitcoinChartsAsync();
            return Ok(bitcoinCharts);
        }
    }
}