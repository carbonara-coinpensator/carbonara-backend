using System.Collections.Generic;
using System.Threading.Tasks;
using Carbonara.Models.Country;

namespace Carbonara.Services.CountryCo2EmissionService
{
    public interface ICountryCo2EmissionService
    {
        Task<List<Country>> GetCountriesCo2EmissionAsync();
    }
}