namespace Carbonara.Models
{
    public class TransactionDetails
    {
        public string relayed_by;
        
        public int time;

        public int block_height { get; set; }
    }
}