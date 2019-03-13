namespace Carbonara.Models 
{
    public class BlockDetails 
    {
        public string hash { get; set; }
        public int size { get; set; }
        public int height { get; set; }
        public int time { get; set; }
        public string previousblockhash { get; set; }
        public bool isMainChain { get; set; }
        public PoolInfo poolInfo { get; set; }
    }
}