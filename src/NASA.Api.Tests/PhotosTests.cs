using NASA.Api.Utilities;
using Newtonsoft.Json;
using Xunit;

namespace NASA.Api.Tests
{
    public class PhotosTests
    {
        [Theory]
        [Trait("Category", "Integration")]
        [JsonFileData("photos.json")]
        public void CanDeserializePhotos(string data)
        {
            var response = JsonConvert.DeserializeObject<PhotosResponse>(data);
            Assert.NotNull(response);
            Assert.NotEmpty(response.Photos);
        }
    }
}