namespace Carbonara.Models.MiningHardware
{
    public class MiningDevice
    {
        public string Name { get; set; }
        public long HashRate { get; set; }
        public int PowerConsumption { get; set; }
        public HardwareType HardwareType { get; set; }
    }
}