using System;
using Newtonsoft.Json;

namespace NASA.Api
{
    public class Photo
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("sol")]
        public int Sol { get; set; }

        [JsonProperty("earth_date")]
        public DateTime EarthDate { get; set; }

        [JsonProperty("img_src")]
        public string Source { get; set; }
    }
}