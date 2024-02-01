
using BaseSource.Model;

namespace BaseSource.Dto
{
    public class OrderDetailResponse
    {
        public string Id { get; set; }
        public UInt16 Quantity { get; set; }
        public UInt32 Price { get; set; }
        public Product Product { get; set; } = null!;
    }
}
