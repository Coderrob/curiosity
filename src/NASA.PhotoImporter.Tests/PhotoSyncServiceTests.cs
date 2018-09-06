using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Flurl.Http.Testing;
using Moq;
using NASA.PhotoImporter.Importers;
using NASA.PhotoImporter.Services;
using Xunit;

namespace NASA.PhotoImporter.Tests
{
    public class PhotoSyncServiceTests
    {
        private readonly Mock<IDateImporter> _dateImporterMock;
        private readonly Mock<IPhotoImporter> _photoImporterMock;
        private readonly PhotoExportService _service;

        public PhotoSyncServiceTests()
        {
            _dateImporterMock = new Mock<IDateImporter>();
            _photoImporterMock = new Mock<IPhotoImporter>();
            _service = new PhotoExportService(
                _dateImporterMock.Object,
                _photoImporterMock.Object);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void CanCreateService()
        {
            Assert.NotNull(new PhotoExportService(
                _dateImporterMock.Object,
                _photoImporterMock.Object));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ThrowsExceptionIfDateImporterIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new PhotoExportService(null, _photoImporterMock.Object));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ThrowsExceptionIfPhotoImporterIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new PhotoExportService(_dateImporterMock.Object, null));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void GetFileNameFromSourceUrlReturnsEmpty()
        {
            Assert.Equal(string.Empty, _service.GetFileNameFromPhotoSourceUrl(null));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void GetFileNameFromSourceUrlReturnsExpectedFileName()
        {
            var url = "http://mars.jpl.nasa.gov/msl-raw-images/proj/msl/redops/ods/surface/sol/01004/opgs/edr/fcam/FLB_486615455EDR_F0481570FHAZ00323M_.JPG";
            var fileName = "FLB_486615455EDR_F0481570FHAZ00323M_.JPG".ToLower();

            Assert.Equal(fileName, _service.GetFileNameFromPhotoSourceUrl(url));
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void EnsureDirectoryPathExists()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var expectedResult = (currentDirectory + Path.DirectorySeparatorChar).ToLower();

            Assert.Equal(expectedResult, _service.EnsureDirectoryPath(currentDirectory));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void EnsureDirectoryPathThrowsExceptionIfNullPathProvided()
        {
            Assert.Throws<ArgumentNullException>(() => _service.EnsureDirectoryPath(null));
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void EnsureDirectoryPathCreatesDirectory()
        {
            var guid = Guid.NewGuid().ToString("D");
            var newDirectory = Directory.GetCurrentDirectory() +
                               Path.DirectorySeparatorChar +
                               guid +
                               Path.DirectorySeparatorChar;

            var directoryPath = _service.EnsureDirectoryPath(newDirectory);

            Assert.True(Directory.Exists(directoryPath));

            Directory.Delete(directoryPath);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task DownloadPhotoHandlesNullOutputPath()
        {
            await _service.DownloadPhoto(null, "file.jpg");
        }

        [Fact]
        public async Task DownloadPhotoHandlesNullPhotoUrl()
        {
            await _service.DownloadPhoto("folder", null);
        }

        [Fact]
        public async Task DownloadPhotoHandlesInvalidImageTypes()
        {
            await _service.DownloadPhoto("folder", "hacker.exe");
        }

        [Fact]
        public async Task DownloadPhotoHandlesFailedDownloadResponse()
        {
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(status: 500);

                await _service.DownloadPhoto("folder", "file.jpg");
            }
        }

        [Fact]
        public async Task GetDatesReturnsNull()
        {
            _dateImporterMock
                    .Setup(m => m.GetDates())
                    .ReturnsAsync((IEnumerable<DateTime>) null);

            Assert.Empty(await _service.GetDates());
        }

        [Fact]
        public async Task GetDatesReturnsDates()
        {
            _dateImporterMock
                    .Setup(m => m.GetDates())
                    .ReturnsAsync(new List<DateTime>
                    {
                        DateTime.Now
                    });

            Assert.Single(await _service.GetDates());
        }

        [Fact]
        public async Task GetDatesReturnsDistinctDates()
        {
            var date = new DateTime(2015, 6, 3);

            _dateImporterMock
                    .Setup(m => m.GetDates())
                    .ReturnsAsync(new List<DateTime>
                    {
                        date,
                        date
                    });

            Assert.Single(await _service.GetDates());
        }

        [Fact]
        public async Task GetPhotosReturnsNull()
        {
            _photoImporterMock
                    .Setup(m => m.GetPhotos(It.IsAny<DateTime>()))
                    .ReturnsAsync((IEnumerable<string>) null);

            Assert.Empty(await _service.GetDates());
        }

        [Fact]
        public async Task GetPhotosReturnsPhotos()
        {
            _photoImporterMock
                    .Setup(m => m.GetPhotos(It.IsAny<DateTime>()))
                    .ReturnsAsync(new List<string>
                    {
                        "testing.jpg"
                    });

            Assert.Single(await _service.GetPhotos(DateTime.Now));
        }

        [Fact]
        public async Task GetPhotosReturnsDistinctPhotos()
        {
            _photoImporterMock
                    .Setup(m => m.GetPhotos(It.IsAny<DateTime>()))
                    .ReturnsAsync(new List<string>
                    {
                        "testing.jpg",
                        "testing.jpg"
                    });

            Assert.Single(await _service.GetPhotos(DateTime.Now));
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task CanExportFromNASA()
        {
            var testDate = new DateTime(2018, 1, 1);
            var testImage = "http://mars.jpl.nasa.gov/msl-raw-images/proj/msl/redops/ods/surface/sol/01004/opgs/edr/fcam/FLB_486615455EDR_F0481570FHAZ00323M_.JPG";

            _dateImporterMock
                    .Setup(m => m.GetDates())
                    .ReturnsAsync(new List<DateTime> { testDate });

            _photoImporterMock
                    .Setup(m => m.GetPhotos(testDate))
                    .ReturnsAsync(new List<string> { testImage });

            await _service.Export(Directory.GetCurrentDirectory());

            var expectedDirectory = Directory.GetCurrentDirectory() +
                                    Path.DirectorySeparatorChar +
                                    "2018-1-1" +
                                    Path.DirectorySeparatorChar;

            var expectedFilePath = expectedDirectory +
                                   "flb_486615455edr_f0481570fhaz00323m_.jpg";

            Assert.True(Directory.Exists(expectedDirectory));
            Assert.True(File.Exists(expectedFilePath));

            File.Delete(expectedFilePath);
            Directory.Delete(expectedDirectory);
        }
    }
}