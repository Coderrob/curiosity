using System.Runtime.CompilerServices;
using NASA.Api.Utilities;

[assembly: InternalsVisibleTo("NASA.Api.Tests")]

namespace NASA.Api.Cameras
{
    internal class ChemistryAndCameraComplex : Camera
    {
        public override string Name => "Chemistry and Camera Complex";
        public override string Abbreviation => "CHEMCAM";

        public ChemistryAndCameraComplex(IRequestBuilder requestBuilder) : base(requestBuilder)
        {
        }
    }
}