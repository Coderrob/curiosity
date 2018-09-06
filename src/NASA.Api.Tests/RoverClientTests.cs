using System;
using System.Linq;
using Xunit;

namespace NASA.Api.Tests
{
    public class RoverClientTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void CanCreateRoverClient()
        {
            Assert.NotNull(new RoverClient());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void CanGetRoversFromClient()
        {
            var client = new RoverClient();
            Assert.NotEmpty(client.GetRovers());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ClientThrowsExceptionWithNullApiKey()
        {
            Assert.Throws<ArgumentNullException>(() => { new RoverClient(null); });
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ClientThrowsExceptionWithNullBaseUrl()
        {
            Assert.Throws<ArgumentNullException>(() => { new RoverClient(null, "ApiKeyValue"); });
        }

        [Fact]
        public void GetRoversReturnsThreeRovers()
        {
            var client = new RoverClient();
            Assert.Equal(3, client.GetRovers().Count());
        }
    }
}