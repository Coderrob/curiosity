using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NASA.Api.Cameras;
using NASA.Api.Rovers;
using NASA.Api.Utilities;
using Xunit;

namespace NASA.Api.Tests
{
    public class CuriosityRoverTests
    {
        private readonly Mock<IRequestBuilder> _requestBuilderMock;
        private readonly Curiosity _rover;

        public CuriosityRoverTests()
        {
            _requestBuilderMock = new Mock<IRequestBuilder>();

            _requestBuilderMock
                    .Setup(m => m.AddPath("Curiosity"))
                    .Returns(_requestBuilderMock.Object);

            _requestBuilderMock
                    .Setup(m => m.Clone())
                    .Returns(_requestBuilderMock.Object);

            _rover = new Curiosity(_requestBuilderMock.Object);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void HasName()
        {
            Assert.Equal("Curiosity", _rover.Name);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void HasCameras()
        {
            Assert.NotEmpty(_rover.GetCameras());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void HasExpectedCameras()
        {
            var cameras = _rover.GetCameras();

            var knownCameras = new[] { "FHAZ", "RHAZ", "MAST", "CHEMCAM", "MAHLI", "MARDI", "NAVCAM" };

            Assert.Equal(7, cameras.Count());
            Assert.True(!knownCameras.Except(cameras.Select(c => c.Abbreviation)).Any());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void HasNoUnexpectedCameras()
        {
            var cameras = _rover.GetCameras();

            var knownCameras = new[] { "PANCAM", "MINITES" };

            Assert.True(knownCameras.Except(cameras.Select(c => c.Abbreviation)).Any());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void GetCameraReturnsNullCameraIfUnknown()
        {
            Assert.IsType<NullCamera>(_rover.GetCamera("PANCAM"));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void GetCameraReturnsKnownCamera()
        {
            Assert.IsType<FrontHazardAvoidanceCamera>(_rover.GetCamera("FHAZ"));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetPhotosHandlesNullResponse()
        {
            _requestBuilderMock
                    .Setup(m => m.AddPath("photos"))
                    .Returns(_requestBuilderMock.Object);

            _requestBuilderMock
                    .Setup(m => m.AddQueryParameter(It.IsAny<string>(), It.IsAny<string>()))
                    .Returns(_requestBuilderMock.Object);

            _requestBuilderMock
                    .Setup(m => m.MakeRequest<PhotosResponse>())
                    .ReturnsAsync((PhotosResponse) null);

            Assert.Empty(await _rover.GetPhotosAsync());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetPhotosHandlesNullPhotos()
        {
            _requestBuilderMock
                    .Setup(m => m.AddPath("photos"))
                    .Returns(_requestBuilderMock.Object);

            _requestBuilderMock
                    .Setup(m => m.AddQueryParameter(It.IsAny<string>(), It.IsAny<string>()))
                    .Returns(_requestBuilderMock.Object);

            _requestBuilderMock
                    .Setup(m => m.MakeRequest<PhotosResponse>())
                    .ReturnsAsync(new PhotosResponse());

            Assert.Empty(await _rover.GetPhotosAsync());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetPhotosReturnsPhotos()
        {
            _requestBuilderMock
                    .Setup(m => m.AddPath("photos"))
                    .Returns(_requestBuilderMock.Object);

            _requestBuilderMock
                    .Setup(m => m.AddQueryParameter(It.IsAny<string>(), It.IsAny<string>()))
                    .Returns(_requestBuilderMock.Object);

            _requestBuilderMock
                    .Setup(m => m.MakeRequest<PhotosResponse>())
                    .ReturnsAsync(new PhotosResponse
                    {
                        Photos = new List<Photo> { new Photo() }
                    });

            Assert.Single(await _rover.GetPhotosAsync());
        }

        //[Fact]
        [Trait("Category", "INtegration")]
        public async Task GetPhotos()
        {
            var client = new RoverClient();
            var rovers = client.GetRovers();
            var rover = rovers.First(r => string.Equals(r.Name, "Curiosity"));
            var photos = await rover.GetPhotosAsync(new DateTime(2015, 6, 3));
            Assert.NotEmpty(photos);
        }
    }
}