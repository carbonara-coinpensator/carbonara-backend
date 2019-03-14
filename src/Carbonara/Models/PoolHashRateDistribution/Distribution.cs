using System.Collections.Generic;

namespace Carbonara.Models.PoolHashRateDistribution
{
    public class Distribution
    {
        public int Year { get; set; }
  	    public int YearPeriod { get; set; }
  	    public ICollection<Pool> PoolInformation { get; set; }
    }
}