using System.Net.Http;
using System.Threading.Tasks;

namespace Carbonara.Services.HttpClientHandler
{
    public interface ICloudFlareHttpClientHandler
    {
        Task<HttpResponseMessage> GetAsync(string url);
    }
}