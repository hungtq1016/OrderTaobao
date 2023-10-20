
using BaseSource.Model;

namespace BaseSource.Dto
{
    public class OrderDetailResponse
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public Product Product { get; set; } = null!;
    }
}
