using Infrastructure.EFCore.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.EFCore.Service
{
    public interface IUriService
    {
        Uri GetPageUri(PaginationRequest request, string route);
    }

    public class UriService : IUriService
    {
        private readonly string _uri;

        public UriService(IServiceProvider serviceProvider)
        {
            var accessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            var request = accessor.HttpContext.Request;
            _uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
        }

        public Uri GetPageUri(PaginationRequest request, string route)
        {
            Uri endpoint = new Uri(string.Concat(_uri, route));
            string uriUpdated = QueryHelpers.AddQueryString(endpoint.ToString(), "pageNumber", request.PageNumber.ToString());
            uriUpdated = QueryHelpers.AddQueryString(uriUpdated, "pageSize", request.PageSize.ToString());
            uriUpdated = QueryHelpers.AddQueryString(uriUpdated, "status", request.Status.ToString());

            return new Uri(uriUpdated);
        }
    }
}
