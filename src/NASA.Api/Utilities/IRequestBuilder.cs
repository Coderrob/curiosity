using System.Threading.Tasks;

namespace NASA.Api.Utilities
{
    public interface IRequestBuilder
    {
        IRequestBuilder Clone();
        IRequestBuilder AddQueryParameter(string name, string value);
        IRequestBuilder AddPath(string pathName);
        Task<T> MakeRequest<T>();
    }
}