using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace NASA.Api
{
    public interface IManifest
    {
        string Name { get; }
        DateTime LaunchDate { get; }
        DateTime LandingDate { get; }
        string Status { get; }
        IEnumerable<PhotoManifest> Photos { get; }
        int TotalPhotos { get; }
    }

    public class Manifest : IManifest
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; private set; }

        [JsonProperty(PropertyName = "launch_date")]
        public DateTime LaunchDate { get; private set; }

        [JsonProperty(PropertyName = "landing_date")]
        public DateTime LandingDate { get; private set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; private set; }

        [JsonProperty(PropertyName = "photos")]
        public IEnumerable<PhotoManifest> Photos { get; private set; }

        [JsonProperty(PropertyName = "total_photos")]
        public int TotalPhotos { get; private set; }
    }

    public class PhotoManifest
    {
        [JsonProperty(PropertyName = "sol")]
        public int Sol { get; private set; }

        [JsonProperty(PropertyName = "cameras")]
        public IEnumerable<string> Cameras { get; private set; }

        [JsonProperty(PropertyName = "earth_date")]
        public DateTime EarthDate { get; private set; }

        [JsonProperty(PropertyName = "total_photos")]
        public int TotalPhotos { get; private set; }
    }

    public class NullManifest : IManifest
    {
        public string Name => "Unknown manifest";
        public DateTime LaunchDate => DateTime.MinValue;
        public DateTime LandingDate => DateTime.MinValue;
        public string Status => "unknown";
        public IEnumerable<PhotoManifest> Photos => Enumerable.Empty<PhotoManifest>();
        public int TotalPhotos => 0;
    }
}