using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NASA.Api.Cameras;

[assembly: InternalsVisibleTo("NASA.Api.Tests")]

namespace NASA.Api.Rovers
{
    internal class NullRover : IRover
    {
        public string Name => "Unknown rover";

        public ICamera GetCamera(string abbreviation)
        {
            return new NullCamera();
        }

        public IEnumerable<ICamera> GetCameras()
        {
            return Enumerable.Empty<ICamera>();
        }

        public Task<IEnumerable<Photo>> GetPhotosAsync(DateTime? date = null)
        {
            return Task.FromResult(Enumerable.Empty<Photo>());
        }
    }
}