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