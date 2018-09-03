using System.Runtime.CompilerServices;
using Newtonsoft.Json;

[assembly: InternalsVisibleTo("NASA.Api.Tests")]

namespace NASA.Api.Utilities
{
    internal class ManifestResponse
    {
        [JsonProperty(PropertyName = "photo_manifest")]
        public Manifest Manifest { get; private set; }
    }
}