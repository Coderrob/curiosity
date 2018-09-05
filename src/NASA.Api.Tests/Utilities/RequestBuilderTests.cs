using System;
using System.Linq;
using System.Threading.Tasks;
using Flurl.Http.Testing;
using NASA.Api.Tests.Helpers;
using NASA.Api.Utilities;
using Newtonsoft.Json;
using Xunit;

namespace NASA.Api.Tests.Utilities
{
    public class RequestBuilderTests
    {
        private readonly string _testUrl = "http://example.com";

        [Fact]
        [Trait("Category", "Unit")]
        public void CanCreateRequestBuilder()
        {
            Assert.NotNull(new RequestBuilder(_testUrl));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ThrowsExceptionIfNoBaseUrlProvided()
        {
            Assert.Throws<ArgumentNullException>(() => { new RequestBuilder(null); });
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ReturnsBaseUrl()
        {
            var builder = new RequestBuilder(_testUrl);
            Assert.Equal(_testUrl, builder.BaseUrl);
            Assert.Equal(_testUrl, builder.GetRequest().ToString());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ReturnsRequestUriWithNewPath()
        {
            var builder = (RequestBuilder) new RequestBuilder(_testUrl)
                    .AddPath("testing");

            Assert.Equal(_testUrl, builder.BaseUrl);
            Assert.Equal($"{_testUrl}/testing", builder.GetRequest().ToString());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ReturnsRequestUriWithNewPathAndQueryParameter()
        {
            var builder = (RequestBuilder) new RequestBuilder(_testUrl)
                                           .AddPath("testing")
                                           .AddQueryParameter("test", "true");

            Assert.Equal(_testUrl, builder.BaseUrl);
            Assert.Equal($"{_testUrl}/testing?test=true", builder.GetRequest().ToString());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ReturnsRequestUriWithNewPathAndQueryParameterRegardlessOfCallOrdering()
        {
            var builder = (RequestBuilder) new RequestBuilder(_testUrl)
                                           .AddQueryParameter("test", "true")
                                           .AddPath("testing");

            Assert.Equal(_testUrl, builder.BaseUrl);
            Assert.Equal($"{_testUrl}/testing?test=true", builder.GetRequest().ToString());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void CloneCreatesNewInstanceOfBuilder()
        {
            var builder = new RequestBuilder(_testUrl);
            var builderCopy = builder.Clone();
            builder.AddPath("testing");

            Assert.Equal(_testUrl, (builderCopy as RequestBuilder)?.GetRequest().ToString());
            Assert.Equal($"{_testUrl}/testing", builder.GetRequest().ToString());
            Assert.NotEqual(builder, builderCopy);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetPhotosAsyncExtensionThrowsArgumentNullException()
        {
            RequestBuilder builder = null;
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await builder.GetPhotosAsync());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetPhotosAsyncExtensionReturnsEmptyWhenBadResponse()
        {
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(status: 500);

                var builder = new RequestBuilder(_testUrl);

                Assert.Empty(await builder.GetPhotosAsync());
            }
        }

        [Theory]
        [Trait("Category", "Integration")]
        [JsonFileData("photos.json")]
        public async Task GetPhotosAsyncExtensionReturnsPhotos(string data)
        {
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWithJson(body: JsonConvert.DeserializeObject(data));

                var builder = new RequestBuilder(_testUrl);

                var photos = await builder.GetPhotosAsync();

                Assert.Equal(856, photos.Count());
            }
        }
    }
}