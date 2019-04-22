using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BlockDataScrapper
{
    class Program
    {
        static void Main()
        {
            //GetBitcoinData().GetAwaiter().GetResult();
            GetCarbonaraData().GetAwaiter().GetResult();
        }

        private static async Task GetBitcoinData()
        {
            var client = new HttpClient();
            var startDate = new DateTime(2018, 1, 1);
            var endDate = new DateTime(2018, 1, 2);

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

                File.AppendAllText("data.txt", sb.ToString());
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
            var sb = new StringBuilder();

            for (var i = 0; i < bitcoinData.Length; i+=7)
            {
                var hash1 = bitcoinData[i].Split(' ')[0];
                var hash2 = bitcoinData[i + 3].Split(' ')[0];
                var hash3 = bitcoinData[i + 6].Split(' ')[0];
                
                var response1 = await client.GetAsync($"https://localhost:5001/api/carbonara/block/calculation?blockHash={hash1}");
                var result1 = await response1.Content.ReadAsStringAsync();
                Thread.Sleep(10000);

                var response2 = await client.GetAsync($"https://localhost:5001/api/carbonara/block/calculation?blockHash={hash2}");
                var result2 = await response2.Content.ReadAsStringAsync();
                Thread.Sleep(10000);

                var response3 = await client.GetAsync($"https://localhost:5001/api/carbonara/block/calculation?blockHash={hash3}");
                var result3 = await response3.Content.ReadAsStringAsync();
                Thread.Sleep(10000);

                var average = (decimal.Parse(result1) + decimal.Parse(result2) + decimal.Parse(result3)) / 3m;

                sb.AppendLine($"x: {average}, y: {bitcoinData[i].Split(' ')[1]}, date: {bitcoinData[i].Split(' ')[2]}");
            }

            File.AppendAllText("data-carbonara.txt", sb.ToString());

            Console.Read();
        }
    }
}
