using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Carbonara.Providers;
using Carbonara.Services;
using Xunit;

namespace Carbonara.Tests
{
    public class JsonHardwareProviderFixture
    {
        private readonly IJsonHardwareProvider _jsonHardwareProvider;

        public JsonHardwareProviderFixture()
        {
            _jsonHardwareProvider = new JsonHardwareProvider();
        }

        [Fact]
        public async Task ReturnFalseGivenValueOf1()
        {
            var _path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var x = Directory.GetCurrentDirectory();
            var result = await _jsonHardwareProvider.GetAll();

            Assert.All(result, item => Assert.True(item.HashRate > 0));
        }


    }
}
