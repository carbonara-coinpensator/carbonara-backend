
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Carbonara.Providers
{
    public class BaseJsonFileProvider
    {
        protected string DirectoryPath { get; private set; }

        public BaseJsonFileProvider()
        {
            DirectoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        protected async Task<T> ReadFromFileAndDeserialize<T>(string filename)
        {
            var location = $"{DirectoryPath}/{filename}";
            try
            {
                using (StreamReader reader = new StreamReader(location))
                {
                    var json = await reader.ReadToEndAsync();
                    return JsonConvert.DeserializeObject<T>(json);
                }
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException($"Failed to read file {location}", ex);
            }
            catch (JsonSerializationException ex)
            {
                throw new JsonSerializationException($"Failed to deserialize the file {location}", ex);
            }
        }
    }
}