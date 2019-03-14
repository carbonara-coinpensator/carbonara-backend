using System.Collections.Generic;

namespace Carbonara.Models.BlockDetails
{
    public class Data
    {
        public int time { get; set; }
        public List<string> txs { get; set; }
        public string previous_blockhash { get; set; }
    }
}