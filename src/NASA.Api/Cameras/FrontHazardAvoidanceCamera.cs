using System.Runtime.CompilerServices;
using NASA.Api.Utilities;

[assembly: InternalsVisibleTo("NASA.Api.Tests")]

namespace NASA.Api.Cameras
{
    internal class FrontHazardAvoidanceCamera : Camera
    {
        public override string Name => "Front Hazard Avoidance Camera";
        public override string Abbreviation => "FHAZ";

        public FrontHazardAvoidanceCamera(IRequestBuilder requestBuilder)
                : base(requestBuilder)
        {
        }
    }
}