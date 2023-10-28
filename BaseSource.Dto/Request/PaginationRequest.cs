
namespace BaseSource.Dto
{
    public class PaginationRequest
    {
        public UInt16 PageNumber { get; set; }
        public UInt16 PageSize { get; set; }
        public PaginationRequest(int v, ushort pageSize)
        {
            PageNumber = 1;
            PageSize = 10;
        }
        public PaginationRequest(UInt16 pageNumber, UInt16 pageSize)
        {
            PageNumber = (UInt16)(pageNumber < 1 ? 1 : pageNumber);
            PageSize = (UInt16)(pageSize > 100 ? 100 : pageSize);
        }
    }
}
