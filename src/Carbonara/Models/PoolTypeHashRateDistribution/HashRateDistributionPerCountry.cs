using System.Collections.Generic;
using Carbonara.Models.Country;

namespace Carbonara.Models.PoolTypeHashRateDistribution
{
    public class HashRateDistributionPerCountry
    {
        public Country.Country Country { get; set; }
        public decimal Percentage {get; set;}
    }
}