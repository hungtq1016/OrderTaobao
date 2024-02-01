using Infrastructure.EFCore.DTOs;
using Infrastructure.EFCore.Service;

namespace Infrastructure.EFCore.Helpers
{
    public class PaginationHelper
    {
        public static PaginationResponse<List<T>> PaginationGeneration<T>(List<T> data, PaginationRequest request, IUriService util, string route)
        {
            int totalRecords = data.Count();

            var response = new PaginationResponse<List<T>>(data, request.PageNumber, request.PageSize);

            int totalPages = totalRecords / request.PageSize;

            response.PreviousPage =
                request.PageNumber > 1
                ? util.GetPageUri(new PaginationRequest(request.PageNumber - 1, request.PageSize), route)
                : null;

            response.NextPage =
                request.PageNumber < totalPages
                ? util.GetPageUri(new PaginationRequest(request.PageNumber + 1, request.PageSize), route)
                : null;

            response.FirstPage = util.GetPageUri(new PaginationRequest(1, request.PageSize), route);
            response.LastPage = util.GetPageUri(new PaginationRequest(totalPages, request.PageSize), route);

            response.TotalPages = totalPages;
            response.TotalRecords = totalRecords;

            return response;
        }
    }
}
