using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NASA.Api.Cameras;
using NASA.Api.Utilities;

[assembly: InternalsVisibleTo("NASA.Api.Tests")]

namespace NASA.Api.Rovers
{
    internal class Spirit : Rover
    {
        public override string Name => "Spirit";

        internal Spirit(IRequestBuilder requestBuilder) : base(requestBuilder)
        {
            Cameras = new List<ICamera>
            {
                new FrontHazardAvoidanceCamera(RequestBuilder),
                new RearHazardAvoidanceCamera(RequestBuilder),
                new NavigationCamera(RequestBuilder),
                new PanoramicCamera(RequestBuilder),
                new MiniatureThermalEmissionSpectrometer(RequestBuilder)
            };
        }
    }
}