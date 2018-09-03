using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace NASA.PhotoImporter.Tests
{
    public class PhotoSyncServiceTests
    {
        private readonly Mock<IDateImporter> _dateImporterMock;
        private readonly Mock<IPhotoImporter> _photoImporterMock;
        private readonly PhotoSyncService _service;

        public PhotoSyncServiceTests()
        {
            _dateImporterMock = new Mock<IDateImporter>();
            _photoImporterMock = new Mock<IPhotoImporter>();
            _service = new PhotoSyncService(
                _dateImporterMock.Object,
                _photoImporterMock.Object);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void CanCreateService()
        {
            Assert.NotNull(new PhotoSyncService(
                _dateImporterMock.Object,
                _photoImporterMock.Object));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ThrowsExceptionIfDateImporterIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new PhotoSyncService(null, _photoImporterMock.Object));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ThrowsExceptionIfPhotoImporterIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new PhotoSyncService(_dateImporterMock.Object, null));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void GetFileNameFromSourceUrlReturnsEmpty()
        {
            Assert.Equal(string.Empty, _service.GetImageFileNameFromSourceUrl(null));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void GetFileNameFromSourceUrlReturnsExpectedFileName()
        {
            var url = "http://mars.jpl.nasa.gov/msl-raw-images/proj/msl/redops/ods/surface/sol/01004/opgs/edr/fcam/FLB_486615455EDR_F0481570FHAZ00323M_.JPG";
            var fileName = "FLB_486615455EDR_F0481570FHAZ00323M_.JPG".ToLower();

            Assert.Equal(fileName, _service.GetImageFileNameFromSourceUrl(url));
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
        [Trait("Category", "Integration")]
        public async Task CanExport()
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