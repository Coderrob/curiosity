using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NASA.Api;
using NASA.Api.Rovers;
using NLog;

[assembly: InternalsVisibleTo("NASA.PhotoImporter.Tests")]

namespace NASA.PhotoImporter.Importers
{
    internal class RoverPhotoImporter : IPhotoImporter
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private readonly IEnumerable<IRover> _rovers;

        public RoverPhotoImporter(IRoverClient roverClient)
        {
            if (roverClient == null)
                throw new ArgumentNullException(nameof(roverClient));

            _rovers = roverClient.GetRovers() ?? Enumerable.Empty<IRover>();
        }

        /// <summary>
        ///     Get the source paths for every rover on a specified date.
        /// </summary>
        /// <param name="date"></param>
        /// <returns>A collection of string image paths</returns>
        public async Task<IEnumerable<string>> GetPhotos(DateTime date)
        {
            try
            {
                var photoUrls = new List<string>();

                foreach (var rover in _rovers)
                {
                    try
                    {
                        photoUrls.AddRange(await GetPhotoUrls(rover, date));
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex, $"Failed to get photos from '{rover.Name}' rover. Error: {ex.Message}");
                    }
                }

                return photoUrls.Distinct();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Failed to get photos from rovers. Error: {ex.Message}");
                return Enumerable.Empty<string>();
            }
        }

        public async Task<IEnumerable<string>> GetPhotoUrls(IRover rover, DateTime date)
        {
            if (rover == null)
                return Enumerable.Empty<string>();

            var photos = await rover.GetPhotosAsync(date);

            if (photos == null)
                return Enumerable.Empty<string>();

            return photos.Select(p => p.Source)
                         .Where(s => !string.IsNullOrEmpty(s))
                         .ToList();
        }
    }
}