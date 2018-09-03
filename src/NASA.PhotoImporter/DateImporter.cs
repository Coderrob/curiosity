using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NLog;

namespace NASA.PhotoImporter
{
    public class DateImporter : IDateImporter
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private readonly string _filePath;

        public DateImporter(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));

            if (!File.Exists(filePath))
                throw new Exception($"File not found at specified path '{filePath}'.");

            _filePath = filePath;
        }

        /// <summary>
        ///     Gets the dates from a file.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<DateTime>> GetDates()
        {
            try
            {
                var dates = new List<DateTime>();

                using (var reader = new StreamReader(_filePath))
                {
                    string nextLine;
                    while ((nextLine = await reader.ReadLineAsync()) != null)
                    {
                        var date = GetDateFromString(nextLine);

                        if (!date.HasValue)
                        {
                            Logger.Warn($"Failed to parse date '{nextLine}' from file '{_filePath}'.");
                            continue;
                        }

                        dates.Add(date.Value);
                    }
                }

                return dates;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Failed to get dates from '{_filePath}'. Error: {ex.Message}");
                return Enumerable.Empty<DateTime>();
            }
        }

        public DateTime? GetDateFromString(string value)
        {
            if (string.IsNullOrEmpty(value)) return null;

            return DateTime.TryParse(value, out var date)
                    ? date
                    : (DateTime?) null;
        }
    }
}