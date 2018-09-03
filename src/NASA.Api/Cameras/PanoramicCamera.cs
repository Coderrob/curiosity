using System.Runtime.CompilerServices;
using NASA.Api.Utilities;

[assembly: InternalsVisibleTo("NASA.Api.Tests")]

namespace NASA.Api.Cameras
{
    internal class PanoramicCamera : Camera
    {
        public override string Name => "Panoramic Camera";
        public override string Abbreviation => "PANCAM";

        public PanoramicCamera(IRequestBuilder requestBuilder) : base(requestBuilder)
        {
        }
    }
}