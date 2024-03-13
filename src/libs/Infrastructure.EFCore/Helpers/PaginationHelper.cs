namespace Infrastructure.EFCore.Helpers
{
    public class PaginationHelper<TEntity>
    {
        public static PaginationResponse<List<TEntity>> GeneratePaginationResponse(List<TEntity> data, PaginationRequest request, int totalRecords)
        {
            int totalPages = totalRecords / request.PageSize + 1;

            var response = new PaginationResponse<List<TEntity>>(data, request.PageNumber, request.PageSize);

            response.PreviousPage = request.PageNumber > 1 ? request.PageNumber - 1 : 1;
            response.NextPage = request.PageNumber < totalPages ? request.PageNumber + 1 : totalPages;

            response.FirstPage = 1;
            response.LastPage = totalPages;
            response.TotalPages = totalPages;
            response.TotalRecords = totalRecords;
            
            return response;
        }
    }
}
