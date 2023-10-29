using BaseSource.BackendAPI.Services;
using BaseSource.Dto;

namespace BaseSource.Helper
{
    public class PaginationHelper
    {
        public static PageResponse<List<T>> CreatePagedReponse<T>(List<T> pagedData, PaginationRequest validFilter, UInt16 totalRecords, IUriService uriService, string route)
        {
            var respose = new PageResponse<List<T>>(pagedData, validFilter.PageNumber, validFilter.PageSize);
            PaginationRequest validFilter1 = validFilter;
            UInt16 totalPages = (UInt16)(totalRecords /validFilter1.PageSize);
            UInt16 roundedTotalPages = totalPages;

            respose.NextPage =
                validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(new PaginationRequest(Convert.ToUInt16(validFilter.PageNumber + 1), validFilter.PageSize), route)
                : null;

            respose.PreviousPage =
                validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri(new PaginationRequest(Convert.ToUInt16(validFilter.PageNumber - 1), validFilter.PageSize), route)
                : null;

            respose.FirstPage = uriService.GetPageUri(new PaginationRequest(1, validFilter.PageSize), route);
            respose.LastPage = uriService.GetPageUri(new PaginationRequest(roundedTotalPages, validFilter.PageSize), route);
            respose.TotalPages = roundedTotalPages;
            respose.TotalRecords = totalRecords;

            return respose;
        }
    }
}