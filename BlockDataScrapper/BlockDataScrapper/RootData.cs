using System;
using System.Collections.Generic;
using System.Text;

namespace BlockDataScrapper
{
    public class Extras
    {
        public string pool_name { get; set; }
        public string pool_link { get; set; }
    }

    public class Datum
    {
        public int height { get; set; }
        public int version { get; set; }
        public string mrkl_root { get; set; }
        public int timestamp { get; set; }
        public int bits { get; set; }
        public object nonce { get; set; }
        public string hash { get; set; }
        public string prev_block_hash { get; set; }
        public string next_block_hash { get; set; }
        public int size { get; set; }
        public object pool_difficulty { get; set; }
        public double difficulty { get; set; }
        public int tx_count { get; set; }
        public object reward_block { get; set; }
        public int reward_fees { get; set; }
        public int created_at { get; set; }
        public int confirmations { get; set; }
        public bool is_orphan { get; set; }
        public int curr_max_timestamp { get; set; }
        public bool is_sw_block { get; set; }
        public int stripped_size { get; set; }
        public int weight { get; set; }
        public Extras extras { get; set; }
    }

    public class RootObject
    {
        public List<Datum> data { get; set; }
        public int err_no { get; set; }
        public object err_msg { get; set; }
    }
}
