using System.Runtime.CompilerServices;
using NASA.Api.Utilities;

[assembly: InternalsVisibleTo("NASA.Api.Tests")]

namespace NASA.Api.Cameras
{
    internal class MarsDescentImager : Camera
    {
        public override string Name => "Mars Descent Imager";
        public override string Abbreviation => "MARDI";

        public MarsDescentImager(IRequestBuilder requestBuilder) : base(requestBuilder)
        {
        }
    }
}