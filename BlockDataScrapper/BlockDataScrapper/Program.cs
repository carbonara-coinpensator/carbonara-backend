using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BlockDataScrapper
{
     public class Dto 
     {
        public string Timestamp { get; set; }
        public string Hash { get; set; }
     }

    class Program
    {
        static void Main(string[] args)
        {
            //GetBitcoinData().GetAwaiter().GetResult();
            GetCarbonaraData().GetAwaiter().GetResult();
        }

        private static async Task GetBitcoinData()
        {
            var client = new HttpClient();
            var startDate = new DateTime(2013, 1, 1);
            var endDate = new DateTime(2013, 1, 10);

            var sb = new StringBuilder();

            try
            {
                while (startDate < endDate)
                {
                    var monthString = startDate.Month < 10 ? $"0{startDate.Month}" : startDate.Month.ToString();
                    var dayString = startDate.Day < 10 ? $"0{startDate.Day}" : startDate.Day.ToString();

                    var response = await client.GetAsync($"https://chain.api.btc.com/v3/block/date/{startDate.Year}{monthString}{dayString}");
                    var data = await response.Content.ReadAsStringAsync();
                    var d = JsonConvert.DeserializeObject<RootObject>(data);

                    var random = new Random();
                    var selectedBlockIndex = random.Next(0, d.data.Count - 1);
                    var selectedBlock = d.data[selectedBlockIndex];

                    var blockData = selectedBlock.hash + " " + selectedBlock.timestamp + " " + DateTime.UnixEpoch.AddSeconds(selectedBlock.timestamp);
                    sb.AppendLine(blockData);
                    startDate = startDate.AddDays(1);

                    Thread.Sleep(3000);
                }

                File.WriteAllText("data.txt", sb.ToString());

                Console.Read();
            }

            catch (Exception e)
            {
                Console.Write(e.Message);
            }           
        }

        private static async Task GetCarbonaraData()
        {
            var bitcoinData = File.ReadAllLines("data.txt");
            var client = new HttpClient();


            foreach (var line in bitcoinData)
            {
                var hash = line.Split(' ')[0];

                var response = await client.GetAsync($"https://localhost:5001/api/carbonara/block/calculation?blockHash={hash}");
                var content = await response.Content.ReadAsStringAsync();
            }
        }
    }
}
