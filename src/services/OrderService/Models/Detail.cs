using Core;

namespace OrderService.Models
{
    public class Detail : Entity
    {
        public int Quantity { get; set; }
        public long Price { get; set; }
        public Guid OrderId { get; set; }
        public Order? Order { get; set; } = null!;
    }
}
