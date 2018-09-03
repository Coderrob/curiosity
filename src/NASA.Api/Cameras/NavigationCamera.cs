using System.Runtime.CompilerServices;
using NASA.Api.Utilities;

[assembly: InternalsVisibleTo("NASA.Api.Tests")]

namespace NASA.Api.Cameras
{
    internal class NavigationCamera : Camera
    {
        public override string Name => "Navigation Camera";
        public override string Abbreviation => "NAVCAM";

        public NavigationCamera(IRequestBuilder requestBuilder) : base(requestBuilder)
        {
        }
    }
}