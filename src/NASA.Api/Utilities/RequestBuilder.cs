using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

[assembly: InternalsVisibleTo("NASA.Api.Tests")]

namespace NASA.Api.Utilities
{
    internal class RequestBuilder : IRequestBuilder
    {
        private Url RequestUrl { get; set; }
        public string BaseUrl { get; }

        public RequestBuilder(string baseUrl)
        {
            if (string.IsNullOrEmpty(baseUrl))
                throw new ArgumentNullException(nameof(baseUrl));

            BaseUrl = baseUrl;
            RequestUrl = new Url(baseUrl);
        }

        public IRequestBuilder Clone()
        {
            return new RequestBuilder(RequestUrl.ToString());
        }

        public IRequestBuilder AddQueryParameter(string name, string value)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value))
                return this;

            RequestUrl = RequestUrl.SetQueryParam(name, value);
            return this;
        }

        public IRequestBuilder AddPath(string pathName)
        {
            if (string.IsNullOrEmpty(pathName))
                return this;

            RequestUrl = RequestUrl.AppendPathSegment(pathName);
            return this;
        }

        public async Task<T> MakeRequest<T>()
        {
            return await RequestUrl.GetJsonAsync<T>();
        }

        public Url GetRequest()
        {
            return RequestUrl;
        }
    }
}