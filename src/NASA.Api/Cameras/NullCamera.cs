using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("NASA.Api.Tests")]

namespace NASA.Api.Cameras
{
    internal class NullCamera : ICamera
    {
        public string Name => "Unknown camera";
        public string Abbreviation => "N/A";

        public Task<IEnumerable<Photo>> GetPhotosAsync(DateTime? date = null)
        {
            return Task.FromResult(Enumerable.Empty<Photo>());
        }
    }
}