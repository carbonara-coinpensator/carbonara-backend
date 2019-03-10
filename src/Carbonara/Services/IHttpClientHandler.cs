using System.Net.Http;
using System.Threading.Tasks;

namespace Carbonara.Services
{
    public interface IHttpClientHandler
    {
        Task<HttpResponseMessage> GetAsync(string url);
    }
}