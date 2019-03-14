using System.Collections.Generic;
using System.Threading.Tasks;
using Carbonara.Models.Country;
using Carbonara.Providers.CountryCo2EmissionProvider;

namespace Carbonara.Services.CountryCo2EmissionService
{
    public class CountryCo2EmissionService : ICountryCo2EmissionService
    {
        private readonly ICountryCo2EmissionProvider _countryCo2EmissionProvider;
        
        public CountryCo2EmissionService(ICountryCo2EmissionProvider countryCo2EmissionProvider)
        {
            _countryCo2EmissionProvider = countryCo2EmissionProvider;
        }

        public async Task<ICollection<Country>> GetCountriesCo2EmissionAsync()
        {
            return await _countryCo2EmissionProvider.GetCountriesCo2EmissionAsync();
        }
    }
}