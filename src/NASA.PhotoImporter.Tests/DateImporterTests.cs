using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NASA.PhotoImporter.Importers;
using Xunit;

namespace NASA.PhotoImporter.Tests
{
    public class DateImporterTests
    {
        private readonly string _testFilePath;

        public DateImporterTests()
        {
            _testFilePath = GetTestFilePath();
        }

        private static string GetTestFilePath()
        {
            return Directory.GetCurrentDirectory() +
                   Path.DirectorySeparatorChar +
                   "dates.txt";
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ThrowsArgumentNullExceptionIfFileNameIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DateFileImporter(null));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ThrowsFileNotFoundExceptionIfFileDoesNotExist()
        {
            Assert.Throws<FileNotFoundException>(() => new DateFileImporter($"{Guid.NewGuid():D}.txt"));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void CanCreateImporter()
        {
            Assert.NotNull(new DateFileImporter(_testFilePath));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task CanGetDatesFromFile()
        {
            var importer = new DateFileImporter(_testFilePath);

            Assert.NotEmpty(await importer.GetDates());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task HasExpectedDatesFromFile()
        {
            var importer = new DateFileImporter(_testFilePath);

            var dates = await importer.GetDates();

            Assert.Equal(3, dates.Count());
        }
    }
}