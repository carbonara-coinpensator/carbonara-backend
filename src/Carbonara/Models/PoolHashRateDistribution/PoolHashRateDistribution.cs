using System.Collections.Generic;

namespace Carbonara.Models.PoolHashRateDistribution
{
    public class PoolHashRateDistribution
    {
        public ICollection<Distribution> Distribution { get; set; } 
    }
}