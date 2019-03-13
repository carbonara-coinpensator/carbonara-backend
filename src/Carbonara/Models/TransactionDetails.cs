namespace Carbonara.Models
{
    public class TransactionDetails
    {
        public string txid { get; set; }
        public string blockhash { get; set; }
        public int blockheight { get; set; }
        public int confirmations { get; set; }
        public int time { get; set; }
        public int blocktime { get; set; }
    }
}