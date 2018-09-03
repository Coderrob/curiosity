using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Flurl.Http;
using NLog;

namespace NASA.PhotoImporter
{
    public class PhotoSyncService
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private readonly IDateImporter _dateImporter;
        private readonly IPhotoImporter _photoImporter;

        public PhotoSyncService(
            IDateImporter dateImporter,
            IPhotoImporter photoImporter)
        {
            _dateImporter = dateImporter ?? throw new ArgumentNullException(nameof(dateImporter));
            _photoImporter = photoImporter ?? throw new ArgumentNullException(nameof(photoImporter));
        }

        public async Task Export(string path)
        {
            try
            {
                var directory = EnsureDirectoryPath(path);

                foreach (var date in await GetDates())
                {
                    var outputDirectory = EnsureDirectoryPath(directory + date.ToString("yyyy-M-d"));

                    foreach (var photoUrl in await GetPhotos(date))
                    {
                        if (string.IsNullOrEmpty(photoUrl))
                            continue;

                        var fileName = GetImageFileNameFromSourceUrl(photoUrl);

                        if (!fileName.IsSupportedImage())
                        {
                            Logger.Info($"File '{fileName}' file type is not supported.");
                            continue;
                        }

                        await photoUrl.DownloadFileAsync(outputDirectory, fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Failed exporting photos to '{path}'. Error: {ex.Message}");
            }
        }

        public string GetImageFileNameFromSourceUrl(string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;

            return value.ToLower()
                        .Split('/')
                        .Last();
        }

        public async Task<IEnumerable<DateTime>> GetDates()
        {
            var dates = await _dateImporter.GetDates();

            return dates?.Distinct() ?? Enumerable.Empty<DateTime>();
        }

        public async Task<IEnumerable<string>> GetPhotos(DateTime date)
        {
            var photos = await _photoImporter.GetPhotos(date);

            return photos?.Distinct() ?? Enumerable.Empty<string>();
        }

        public string EnsureDirectoryPath(string folderPath)
        {
            if (string.IsNullOrEmpty(folderPath))
                throw new ArgumentNullException(nameof(folderPath));

            try
            {
                var directoryPath = folderPath.ToLower();

                if (!directoryPath.EndsWith(Path.DirectorySeparatorChar))
                    directoryPath += Path.DirectorySeparatorChar;

                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);

                return directoryPath;
            }
            catch (Exception ex)
            {
                var message = $"Failed to ensure directory path '{folderPath}' exists.";
                Logger.Error(ex, $"{message} Error: {ex.Message}");
                throw new Exception(message);
            }
        }
    }
}