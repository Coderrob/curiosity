using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NASA.Api;
using NASA.Api.Rovers;
using NASA.PhotoImporter.Importers;
using Xunit;

namespace NASA.PhotoImporter.Tests
{
    public class RoverPhotoImporterTests
    {
        private readonly RoverPhotoImporter _importer;
        private readonly Mock<IRoverClient> _roverClientMock;
        private readonly Mock<IRover> _roverMock;

        public RoverPhotoImporterTests()
        {
            _roverClientMock = new Mock<IRoverClient>();
            _roverMock = new Mock<IRover>();

            _roverClientMock
                    .Setup(m => m.GetRovers())
                    .Returns(new List<IRover>
                    {
                        _roverMock.Object
                    });

            _importer = new RoverPhotoImporter(_roverClientMock.Object);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ThrowsExceptionIfRoverClientNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RoverPhotoImporter(null));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void CanGetRovers()
        {
            Assert.NotNull(new RoverPhotoImporter(_roverClientMock.Object));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task CanGetPhotosFromRover()
        {
            _roverMock
                    .Setup(m => m.GetPhotosAsync(It.IsAny<DateTime?>()))
                    .ReturnsAsync(new List<Photo>
                    {
                        new Photo { Source = "SpaceAliens.jpg" }
                    });

            Assert.Single(await _importer.GetPhotos(DateTime.Now));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetPhotosFromRoverReturnsNull()
        {
            _roverMock
                    .Setup(m => m.GetPhotosAsync(It.IsAny<DateTime?>()))
                    .ReturnsAsync((IEnumerable<Photo>) null);

            Assert.Empty(await _importer.GetPhotos(DateTime.Now));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetPhotosFromRoverReturnsEmpty()
        {
            _roverMock
                    .Setup(m => m.GetPhotosAsync(It.IsAny<DateTime?>()))
                    .ReturnsAsync(Enumerable.Empty<Photo>());

            Assert.Empty(await _importer.GetPhotos(DateTime.Now));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetPhotosFromRoverReturnsEmptyWhenPhotoSourceIsNull()
        {
            _roverMock
                    .Setup(m => m.GetPhotosAsync(It.IsAny<DateTime?>()))
                    .ReturnsAsync(new List<Photo>
                    {
                        new Photo()
                    });

            Assert.Empty(await _importer.GetPhotos(DateTime.Now));
        }
    }
}