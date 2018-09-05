using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;

namespace NASA.Api.Utilities
{
    public static class RequestBuilderExtensions
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        internal static async Task<IEnumerable<Photo>> GetPhotosAsync(this IRequestBuilder builder, DateTime? date = null)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            try
            {
                var response = await builder
                                     .Clone()
                                     .AddPath("photos")
                                     .AddQueryParameter("earth_date", date.ToEarthDateString())
                                     .MakeRequest<PhotosResponse>();

                return response?.Photos?.ToList() ?? Enumerable.Empty<Photo>();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Failed to get photos from date '{date.ToEarthDateString()}'. {ex.Message}");
                return Enumerable.Empty<Photo>();
            }
        }
    }
}