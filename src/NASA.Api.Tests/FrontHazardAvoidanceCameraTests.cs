using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NASA.Api.Cameras;
using NASA.Api.Utilities;
using Xunit;

namespace NASA.Api.Tests
{
    public class FrontHazardAvoidanceCameraTests
    {
        private readonly FrontHazardAvoidanceCamera _camera;
        private readonly Mock<IRequestBuilder> _requestBuilderMock;

        public FrontHazardAvoidanceCameraTests()
        {
            _requestBuilderMock = new Mock<IRequestBuilder>();

            _requestBuilderMock
                    .Setup(m => m.AddQueryParameter("camera", It.IsAny<string>()))
                    .Returns(_requestBuilderMock.Object);

            _camera = new FrontHazardAvoidanceCamera(_requestBuilderMock.Object);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void HasName()
        {
            Assert.Equal("Front Hazard Avoidance Camera", _camera.Name);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void HasAbbreviation()
        {
            Assert.Equal("FHAZ", _camera.Abbreviation);
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

            Assert.Empty(await _camera.GetPhotosAsync());
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

            Assert.Empty(await _camera.GetPhotosAsync());
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

            Assert.Single(await _camera.GetPhotosAsync());
        }
    }
}