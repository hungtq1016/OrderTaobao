using BaseSource.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection;

namespace BaseSource.BackendAPI.Services
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationRequest filter, string route);
    }

    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(IServiceProvider serviceProvider)
        {
            var accessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            var request = accessor.HttpContext.Request;
            _baseUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
        }
        public Uri GetPageUri(PaginationRequest filter, string route)
        {
            var _enpointUri = new Uri(string.Concat(_baseUri, route));
            var modifiedUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
            return new Uri(modifiedUri);
        }
    }
}
