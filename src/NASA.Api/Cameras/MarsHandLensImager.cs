using System.Runtime.CompilerServices;
using NASA.Api.Utilities;

[assembly: InternalsVisibleTo("NASA.Api.Tests")]

namespace NASA.Api.Cameras
{
    internal class MarsHandLensImager : Camera
    {
        public override string Name => "Mars Hand Lens Imager";
        public override string Abbreviation => "MAHLI";

        public MarsHandLensImager(IRequestBuilder requestBuilder) : base(requestBuilder)
        {
        }
    }
}