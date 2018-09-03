using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace NASA.Api.Utilities
{
    public static class RequestBuilderExtensions
    {
        internal static async Task<IEnumerable<Photo>> GetPhotosAsync(this IRequestBuilder builder, DateTime? date = null)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            var response = await builder
                                 .AddPath("photos")
                                 .AddQueryParameter("earth_date", date.ToEarthDateString())
                                 .MakeRequest<PhotosResponse>();

            return response?.Photos == null
                    ? Enumerable.Empty<Photo>()
                    : response.Photos.ToImmutableList();
        }
    }
}