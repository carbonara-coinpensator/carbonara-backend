using System.Collections.Generic;

namespace Carbonara.Models.Chart
{
    public class BitcoinCharts
    {
        public List<ChartValue> PriceChart { get; set; }

        public List<ChartValue> Co2EmissionChart { get; set; }
    }
}