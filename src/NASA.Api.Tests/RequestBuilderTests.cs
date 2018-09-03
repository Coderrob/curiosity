using System;
using NASA.Api.Utilities;
using Xunit;

namespace NASA.Api.Tests
{
    public class RequestBuilderTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void CanCreateRequestBuilder()
        {
            Assert.NotNull(new RequestBuilder("http://examples.com"));
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
            var baseUrl = "http://examples.com";
            var builder = new RequestBuilder(baseUrl);
            Assert.Equal(baseUrl, builder.BaseUrl);
            Assert.Equal(baseUrl, builder.GetRequest().ToString());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ReturnsRequestUriWithNewPath()
        {
            var baseUrl = "http://examples.com";
            var builder = (RequestBuilder) new RequestBuilder(baseUrl)
                    .AddPath("testing");

            Assert.Equal(baseUrl, builder.BaseUrl);
            Assert.Equal($"{baseUrl}/testing", builder.GetRequest().ToString());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ReturnsRequestUriWithNewPathAndQueryParameter()
        {
            var baseUrl = "http://examples.com";
            var builder = (RequestBuilder) new RequestBuilder(baseUrl)
                                           .AddPath("testing")
                                           .AddQueryParameter("test", "true");

            Assert.Equal(baseUrl, builder.BaseUrl);
            Assert.Equal($"{baseUrl}/testing?test=true", builder.GetRequest().ToString());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ReturnsRequestUriWithNewPathAndQueryParameterRegardlessOfCallOrdering()
        {
            var baseUrl = "http://examples.com";
            var builder = (RequestBuilder) new RequestBuilder(baseUrl)
                                           .AddQueryParameter("test", "true")
                                           .AddPath("testing");

            Assert.Equal(baseUrl, builder.BaseUrl);
            Assert.Equal($"{baseUrl}/testing?test=true", builder.GetRequest().ToString());
        }
    }
}