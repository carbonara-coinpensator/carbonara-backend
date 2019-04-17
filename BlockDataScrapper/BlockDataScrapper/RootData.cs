using System.Collections.Generic;

namespace BlockDataScrapper
{
    public class BlockInfo
    {
        public int height { get; set; }
        public int timestamp { get; set; }
        public string hash { get; set; }
        public int created_at { get; set; }
    }

    public class RootObject
    {
        public List<BlockInfo> data { get; set; }
    }
}
