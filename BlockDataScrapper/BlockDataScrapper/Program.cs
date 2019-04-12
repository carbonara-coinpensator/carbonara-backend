using System;
using System.Collections.Generic;
using System.Net.Http;
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
        public static Uri ApiBaseAddress = new Uri("https://chain.api.btc.com/v3/");
        public static List<RootObject> data = new List<RootObject>();

        static void Main(string[] args)
        {
            GetData().GetAwaiter().GetResult();
        }

        private static async Task GetData()
        {
            HttpClient client = new HttpClient();

            int year = 2015;
            int month = 01;
            int day = 01;

            // TODO: 5 block per week, prosek 

            try
            {
                var response = await client.GetAsync("https://chain.api.btc.com/v3/block/date/20151215");
                var data = await response.Content.ReadAsStringAsync();

                var d = JsonConvert.DeserializeObject<RootObject>(data);
                Console.Write(d);
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }           
        }
    }
}
