namespace BaseSource.Dto
{
    public class PageResponse<T> 
    {
        public UInt16 PageNumber { get; set; }
        public UInt16 PageSize { get; set; }
        public Uri FirstPage { get; set; }
        public Uri LastPage { get; set; }
        public UInt16 TotalPages { get; set; }
        public UInt16 TotalRecords { get; set; }
        public Uri NextPage { get; set; }
        public Uri PreviousPage { get; set; }
        public T Data { get; set; }

        public PageResponse(T data, UInt16 pageNumber, UInt16 pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data = data;
        }
    }
}
