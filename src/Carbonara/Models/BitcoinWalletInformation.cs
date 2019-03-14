using System.Collections.Generic;

namespace Carbonara.Models
{
    public class BitcoinWalletInformation
    {
        public string hash160 { get; set; }
        public string address { get; set; }
        public long n_tx { get; set; }
        public long total_received { get; set; }
        public long total_sent { get; set; }
        public long final_balance { get; set; }
        public List<Tx> txs { get; set; }
    }

    public class SpendingOutpoint
{
    public long tx_index { get; set; }
    public long n { get; set; }
}

public class PrevOut
{
    public bool spent { get; set; }
    public List<SpendingOutpoint> spending_outpoints { get; set; }
    public long tx_index { get; set; }
    public long type { get; set; }
    public string addr { get; set; }
    public long value { get; set; }
    public long n { get; set; }
    public string script { get; set; }
}

public class Input
{
    public object sequence { get; set; }
    public string witness { get; set; }
    public PrevOut prev_out { get; set; }
    public string script { get; set; }
}

public class SpendingOutpoint2
{
    public long tx_index { get; set; }
    public long n { get; set; }
}

public class Out
{
    public bool spent { get; set; }
    public List<SpendingOutpoint2> spending_outpoints { get; set; }
    public long tx_index { get; set; }
    public long type { get; set; }
    public string addr { get; set; }
    public long value { get; set; }
    public long n { get; set; }
    public string script { get; set; }
}

public class Tx
{
    public long ver { get; set; }
    public List<Input> inputs { get; set; }
    public long weight { get; set; }
    public long block_height { get; set; }
    public string relayed_by { get; set; }
    public List<Out> @out { get; set; }
    public long lock_time { get; set; }
    public long result { get; set; }
    public long size { get; set; }
    public long block_index { get; set; }
    public long time { get; set; }
    public long tx_index { get; set; }
    public long vin_sz { get; set; }
    public string hash { get; set; }
    public long vout_sz { get; set; }
}
}