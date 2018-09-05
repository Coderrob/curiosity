using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NASA.Api.Cameras;
using NASA.Api.Utilities;
using NLog;

[assembly: InternalsVisibleTo("NASA.Api.Tests")]

namespace NASA.Api.Rovers
{
    internal abstract class Rover : IRover
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        internal IRequestBuilder RequestBuilder { get; }
        protected IEnumerable<ICamera> Cameras { get; set; }

        internal Rover(IRequestBuilder requestBuilder)
        {
            RequestBuilder = requestBuilder
                             .Clone()
                             .AddPath(Name);
        }

        public abstract string Name { get; }

        public ICamera GetCamera(string abbreviation)
        {
            if (string.IsNullOrEmpty(abbreviation))
                return new NullCamera();

            var camera = Cameras?.FirstOrDefault(c => string.Equals(c.Abbreviation, abbreviation, StringComparison.InvariantCultureIgnoreCase));

            return camera ?? new NullCamera();
        }

        public IEnumerable<ICamera> GetCameras()
        {
            return Cameras?.ToImmutableList() ?? Enumerable.Empty<ICamera>();
        }

        public async Task<IEnumerable<Photo>> GetPhotosAsync(DateTime? date = null)
        {
            return await RequestBuilder.GetPhotosAsync(date);
        }
    }
}