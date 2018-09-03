using System.Runtime.CompilerServices;
using NASA.Api.Utilities;

[assembly: InternalsVisibleTo("NASA.Api.Tests")]

namespace NASA.Api.Cameras
{
    internal class MastCamera : Camera
    {
        public override string Name => "Mast Camera";
        public override string Abbreviation => "MAST";

        public MastCamera(IRequestBuilder requestBuilder) : base(requestBuilder)
        {
        }
    }
}