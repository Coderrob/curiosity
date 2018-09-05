using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NASA.Api.Cameras;
using NASA.Api.Utilities;

[assembly: InternalsVisibleTo("NASA.Api.Tests")]

namespace NASA.Api.Rovers
{
    internal class Curiosity : Rover
    {
        public override string Name => "Curiosity";

        internal Curiosity(IRequestBuilder requestBuilder) : base(requestBuilder)
        {
            Cameras = new List<ICamera>
            {
                new FrontHazardAvoidanceCamera(RequestBuilder),
                new RearHazardAvoidanceCamera(RequestBuilder),
                new MastCamera(RequestBuilder),
                new ChemistryAndCameraComplex(RequestBuilder),
                new MarsHandLensImager(RequestBuilder),
                new MarsDescentImager(RequestBuilder),
                new NavigationCamera(RequestBuilder)
            };
        }
    }
}