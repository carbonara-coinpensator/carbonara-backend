using System.Collections.Generic;

namespace Carbonara.Models
{
    public class BitcoinWalletInformation
    {
        public string hash160 { get; set; }
        public string address { get; set; }
        public int n_tx { get; set; }
        public int total_received { get; set; }
        public int total_sent { get; set; }
        public int final_balance { get; set; }
        public List<Tx> txs { get; set; }
    }

    public class SpendingOutpoint
{
    public int tx_index { get; set; }
    public int n { get; set; }
}

public class PrevOut
{
    public bool spent { get; set; }
    public List<SpendingOutpoint> spending_outpoints { get; set; }
    public int tx_index { get; set; }
    public int type { get; set; }
    public string addr { get; set; }
    public long value { get; set; }
    public int n { get; set; }
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
    public int tx_index { get; set; }
    public int n { get; set; }
}

public class Out
{
    public bool spent { get; set; }
    public List<SpendingOutpoint2> spending_outpoints { get; set; }
    public int tx_index { get; set; }
    public int type { get; set; }
    public string addr { get; set; }
    public long value { get; set; }
    public int n { get; set; }
    public string script { get; set; }
}

public class Tx
{
    public int ver { get; set; }
    public List<Input> inputs { get; set; }
    public int weight { get; set; }
    public int block_height { get; set; }
    public string relayed_by { get; set; }
    public List<Out> @out { get; set; }
    public int lock_time { get; set; }
    public int result { get; set; }
    public int size { get; set; }
    public int block_index { get; set; }
    public int time { get; set; }
    public int tx_index { get; set; }
    public int vin_sz { get; set; }
    public string hash { get; set; }
    public int vout_sz { get; set; }
}
}