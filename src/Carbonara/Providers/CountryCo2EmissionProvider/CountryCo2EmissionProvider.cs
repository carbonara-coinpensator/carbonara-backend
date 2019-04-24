using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Carbonara.Models.Country;
using Newtonsoft.Json;

namespace Carbonara.Providers.CountryCo2EmissionProvider
{
    public class CountryCo2EmissionProvider : BaseLiteDbProvider, ICountryCo2EmissionProvider
    {
        public CountryCo2EmissionProvider()
        {
        }

        public async Task<List<Country>> GetCountriesCo2EmissionAsync()
        {
            var result = await ReadCollectionFromDb<Country>("countries");
            return result.ToList();
        }
    }
}