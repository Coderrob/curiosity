using System.Runtime.CompilerServices;
using NASA.Api.Utilities;

[assembly: InternalsVisibleTo("NASA.Api.Tests")]

namespace NASA.Api.Cameras
{
    internal class MiniatureThermalEmissionSpectrometer : Camera
    {
        public override string Name => "Miniature Thermal Emission Spectrometer (Mini-TES)";
        public override string Abbreviation => "MINITES";

        public MiniatureThermalEmissionSpectrometer(IRequestBuilder requestBuilder) : base(requestBuilder)
        {
        }
    }
}