namespace Infrastructure.EFCore.DTOs
{
    public class PaginationResponse<TEntity>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int FirstPage { get; set; }
        public int LastPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public int NextPage { get; set; }
        public int PreviousPage { get; set; }
        public TEntity Data { get; set; }

        public PaginationResponse(TEntity data, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Data = data;
        }
    }
}
