using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NASA.Api;
using NASA.Api.Rovers;
using NLog;

namespace NASA.PhotoImporter
{
    public class RoverPhotoImporter : IPhotoImporter
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
                var photoPaths = new List<string>();

                foreach (var rover in _rovers)
                {
                    try
                    {
                        var photos = await rover.GetPhotosAsync(date);

                        if (photos == null) continue;

                        photoPaths.AddRange(photos.Select(p => p.Source)
                                                  .Where(s => !string.IsNullOrEmpty(s)));
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex, $"Failed to get photos from '{rover.Name}' rover. Error: {ex.Message}");
                    }
                }

                return photoPaths.Distinct();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Failed to get photos from rovers. Error: {ex.Message}");
                return Enumerable.Empty<string>();
            }
        }
    }
}