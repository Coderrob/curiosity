using System.Collections.Generic;
using NASA.Api.Cameras;
using NASA.Api.Utilities;

namespace NASA.Api.Rovers
{
    public class Curiosity : Rover
    {
        public override string Name => "Curiosity";

        internal Curiosity(IRequestBuilder requestBuilder) : base(requestBuilder)
        {
            Cameras = new List<ICamera>
            {
                new FrontHazardAvoidanceCamera(RequestBuilder.Clone()),
                new RearHazardAvoidanceCamera(RequestBuilder.Clone()),
                new MastCamera(RequestBuilder.Clone()),
                new ChemistryAndCameraComplex(RequestBuilder.Clone()),
                new MarsHandLensImager(RequestBuilder.Clone()),
                new MarsDescentImager(RequestBuilder.Clone()),
                new NavigationCamera(RequestBuilder.Clone())
            };
        }
    }
}