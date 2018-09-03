using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NASA.Api.Utilities;

[assembly: InternalsVisibleTo("NASA.Api.Tests")]

namespace NASA.Api.Cameras
{
    internal abstract class Camera : ICamera
    {
        protected IRequestBuilder RequestBuilder { get; }

        protected Camera(IRequestBuilder requestBuilder)
        {
            RequestBuilder = requestBuilder.AddQueryParameter("camera", Abbreviation);
        }

        public abstract string Name { get; }

        public abstract string Abbreviation { get; }

        public async Task<IEnumerable<Photo>> GetPhotosAsync(DateTime? date = null)
        {
            return await RequestBuilder.GetPhotosAsync(date);
        }
    }
}