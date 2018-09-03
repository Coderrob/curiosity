using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using NASA.Api.Rovers;
using NASA.Api.Utilities;

namespace NASA.Api
{
    public class RoverClient : IRoverClient
    {
        private const string ApiBaseUrl = "https://api.nasa.gov/mars-photos/api/v1/";
        private const string ApiDemoKey = "DEMO_KEY";
        private IList<IRover> Rovers { get; }

        public RoverClient() : this(ApiBaseUrl, ApiDemoKey)
        {
        }

        public RoverClient(string apiKey) : this(ApiBaseUrl, apiKey)
        {
        }

        public RoverClient(string apiBaseUrl, string apiKey)
        {
            if (string.IsNullOrEmpty(apiBaseUrl)) throw new ArgumentNullException(nameof(apiBaseUrl));
            if (string.IsNullOrEmpty(apiKey)) throw new ArgumentNullException(nameof(apiKey));

            var requestBuilder = new RequestBuilder(apiBaseUrl)
                                 .AddPath("rovers")
                                 .AddQueryParameter("api_key", apiKey);

            Rovers = new List<IRover>
            {
                new Curiosity(requestBuilder)
            };
        }

        public IEnumerable<IRover> GetRovers()
        {
            return Rovers.ToImmutableList();
        }

        public IRover GetRover(string name)
        {
            if (string.IsNullOrEmpty(name))
                return new NullRover();

            var rover = Rovers?.FirstOrDefault(r => string.Equals(r.Name, name, StringComparison.InvariantCultureIgnoreCase));

            return rover ?? new NullRover();
        }
    }
}