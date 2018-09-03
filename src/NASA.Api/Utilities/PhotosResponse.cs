using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

[assembly: InternalsVisibleTo("NASA.Api.Tests")]

namespace NASA.Api.Utilities
{
    internal class PhotosResponse
    {
        [JsonProperty("photos")]
        public List<Photo> Photos { get; set; }
    }
}