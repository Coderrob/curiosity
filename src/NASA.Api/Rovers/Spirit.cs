using System.Collections.Generic;
using NASA.Api.Cameras;
using NASA.Api.Utilities;

namespace NASA.Api.Rovers
{
    public class Spirit : Rover
    {
        public override string Name => "Spirit";

        internal Spirit(IRequestBuilder requestBuilder) : base(requestBuilder)
        {
            Cameras = new List<ICamera>
            {
                new FrontHazardAvoidanceCamera(RequestBuilder.Clone()),
                new RearHazardAvoidanceCamera(RequestBuilder.Clone()),
                new NavigationCamera(RequestBuilder.Clone()),
                new PanoramicCamera(RequestBuilder.Clone()),
                new MiniatureThermalEmissionSpectrometer(RequestBuilder.Clone())
            };
        }
    }
}