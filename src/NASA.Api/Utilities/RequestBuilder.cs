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
        private readonly string _baseUrl;
        private readonly Url _requestUrl;

        public RequestBuilder(string baseUrl)
        {
            if (string.IsNullOrEmpty(baseUrl))
                throw new ArgumentNullException(nameof(baseUrl));

            _baseUrl = baseUrl;
            _requestUrl = new Url(baseUrl);
        }

        private RequestBuilder(string baseUrl, Url requestUrl)
        {
            _requestUrl = requestUrl;
            _baseUrl = baseUrl;
        }

        public IRequestBuilder Clone()
        {
            return new RequestBuilder(_requestUrl.ToString());
        }

        public IRequestBuilder AddQueryParameter(string name, string value)
        {
            return string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value)
                    ? new RequestBuilder(_baseUrl, _requestUrl)
                    : new RequestBuilder(_baseUrl, _requestUrl.ToString().SetQueryParam(name, value));
        }

        public IRequestBuilder AddPath(string pathName)
        {
            return string.IsNullOrEmpty(pathName)
                    ? new RequestBuilder(_baseUrl, _requestUrl)
                    : new RequestBuilder(_baseUrl, _requestUrl.ToString().AppendPathSegment(pathName));
        }

        public async Task<T> MakeRequest<T>()
        {
            return await _requestUrl.GetJsonAsync<T>();
        }

        public Url GetRequest()
        {
            return _requestUrl;
        }
    }
}