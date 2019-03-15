namespace Carbonara.Models.BitcoinWalletInformation
{
    public class Tx
    {
        public string txid { get; set; }
        public int input_no { get; set; }
        public string value { get; set; }
        public int time { get; set; }
    }
}