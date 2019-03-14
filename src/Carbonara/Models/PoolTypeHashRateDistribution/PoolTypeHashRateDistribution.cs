using System.Collections.Generic;

namespace Carbonara.Models.PoolTypeHashRateDistribution
{
    public class PoolTypeHashRateDistribution
    {
        public string PoolType { get; set; }
        public ICollection<HashRateDistributionPerCountry> DistributionPerCountry { get; set; }
    }
}