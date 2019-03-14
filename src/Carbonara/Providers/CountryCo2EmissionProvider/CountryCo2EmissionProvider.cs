using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Carbonara.Models.Country;
using Newtonsoft.Json;

namespace Carbonara.Providers.CountryCo2EmissionProvider
{
    public class CountryCo2EmissionProvider : ICountryCo2EmissionProvider
    {
        private readonly string _path;

        public CountryCo2EmissionProvider()
        {
            _path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        }

        public async Task<List<Country>> GetCountriesCo2EmissionAsync()
        {
            return await ReadCountriesCo2EmissionFromFile($"{_path}/CountryCo2EmissionPerKwh.json");
        }

        private async Task<List<Country>> ReadCountriesCo2EmissionFromFile(string filename)
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                var json = await reader.ReadToEndAsync();
                return JsonConvert.DeserializeObject<List<Country>>(json);
            }
        }
    }
}