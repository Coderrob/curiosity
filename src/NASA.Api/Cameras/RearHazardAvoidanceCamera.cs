using System.Runtime.CompilerServices;
using NASA.Api.Utilities;

[assembly: InternalsVisibleTo("NASA.Api.Tests")]

namespace NASA.Api.Cameras
{
    internal class RearHazardAvoidanceCamera : Camera
    {
        public override string Name => "Rear Hazard Avoidance Camera";
        public override string Abbreviation => "RHAZ";

        public RearHazardAvoidanceCamera(IRequestBuilder requestBuilder) : base(requestBuilder)
        {
        }
    }
}