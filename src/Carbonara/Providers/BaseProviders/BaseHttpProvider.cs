using System.Threading.Tasks;
using Carbonara.Services.HttpClientHandler;
using Newtonsoft.Json;

namespace Carbonara.Providers.BaseProviders
{
    public abstract class BaseHttpProvider
    {
        protected readonly IHttpClientHandler _httpClient;
        protected abstract string Endpoint { get; }

        protected BaseHttpProvider(IHttpClientHandler httpClient)
        {
            _httpClient = httpClient;
        }

        protected async Task<T> GetResponseAndDeserialize<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new ThirdPartyApiUnreachableException($"Failed to communicate with {url}: {response.ReasonPhrase}");
            }

            try
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (JsonSerializationException ex)
            {
                throw new JsonSerializationException($"failed to deserialize the response of the {url}", ex);
            }
        }
    }
}