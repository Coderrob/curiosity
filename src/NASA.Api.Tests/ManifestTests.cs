using NASA.Api.Utilities;
using Newtonsoft.Json;
using Xunit;

namespace NASA.Api.Tests
{
    public class ManifestTests
    {
        [Theory]
        [Trait("Category", "Integration")]
        [JsonFileData("manifest.json")]
        public void CanDeserializeManifest(string data)
        {
            var response = JsonConvert.DeserializeObject<ManifestResponse>(data);
            Assert.NotNull(response);
            Assert.NotNull(response.Manifest);
            Assert.NotEmpty(response.Manifest.Photos);
        }
    }
}