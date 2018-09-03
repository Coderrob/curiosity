using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        public void ThrowsExceptionIfFileNameIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DateImporter(null));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ThrowsExceptionIfFileDoesNotExist()
        {
            Assert.Throws<Exception>(() => new DateImporter($"{Guid.NewGuid():D}.txt"));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void CanCreateImporter()
        {
            Assert.NotNull(new DateImporter(_testFilePath));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task CanGetDatesFromFile()
        {
            var importer = new DateImporter(_testFilePath);

            Assert.NotEmpty(await importer.GetDates());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task HasExpectedDatesFromFile()
        {
            var importer = new DateImporter(_testFilePath);

            var dates = await importer.GetDates();

            Assert.Equal(3, dates.Count());
        }
    }
}