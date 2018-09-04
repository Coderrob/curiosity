﻿using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NASA.Api.Cameras;
using NASA.Api.Utilities;

[assembly: InternalsVisibleTo("NASA.Api.Tests")]

namespace NASA.Api.Rovers
{
    internal class Opportunity : Rover
    {
        public override string Name => "Opportunity";

        internal Opportunity(IRequestBuilder requestBuilder) : base(requestBuilder)
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