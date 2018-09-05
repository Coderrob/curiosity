using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Flurl.Http;
using NASA.PhotoImporter.Importers;
using NLog;

[assembly: InternalsVisibleTo("NASA.PhotoImporter.Tests")]

namespace NASA.PhotoImporter.Services
{
    internal class PhotoSyncService
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

        public async Task Export(string directoryPath)
        {
            try
            {
                var directory = EnsureDirectoryPath(directoryPath);

                foreach (var date in await GetDates())
                {
                    var outputDirectory = EnsureDirectoryPath(directory + date.ToString("yyyy-M-d"));

                    foreach (var photoUrl in await GetPhotos(date))
                    {
                        await DownloadPhoto(outputDirectory, photoUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                var message = $"Failed exporting photos to '{directoryPath}'.";
                Logger.Error(ex, $"{message} Error: {ex.Message}");
                throw new Exception(message, ex);
            }
        }

        public async Task DownloadPhoto(string outputPath, string photoUrl)
        {
            if (string.IsNullOrEmpty(photoUrl))
                return;

            if (string.IsNullOrEmpty(outputPath))
                return;

            try
            {
                var fileName = GetFileNameFromPhotoSourceUrl(photoUrl);

                if (!fileName.IsSupportedImage())
                {
                    Logger.Info($"File '{fileName}' file type is not supported.");
                    return;
                }

                await photoUrl.DownloadFileAsync(outputPath, fileName);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Failed to download to folder '{outputPath}' the requested photo '{photoUrl}'. {ex.Message}");
            }
        }

        public string GetFileNameFromPhotoSourceUrl(string url)
        {
            if (string.IsNullOrEmpty(url)) return string.Empty;

            return Uri.TryCreate(url, UriKind.Absolute, out var uri)
                    ? uri.Segments.LastOrDefault()?.ToLower() ?? string.Empty
                    : string.Empty;
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
                var message = $"Failed to ensure directory directoryPath '{folderPath}' exists.";
                Logger.Error(ex, $"{message} Error: {ex.Message}");
                throw new Exception(message);
            }
        }
    }
}