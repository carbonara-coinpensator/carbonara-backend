using System.Collections.Generic;
using System.Threading.Tasks;
using Carbonara.Models.Country;

namespace Carbonara.Providers.CountryCo2EmissionProvider
{
    public interface ICountryCo2EmissionProvider
    {
        Task<List<Country>> GetCountriesCo2EmissionAsync();
    }
}