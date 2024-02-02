using Core;

namespace OrderService.Models
{
    public class Order : Entity
    {
        public Byte Status { get; set; }
        public ICollection<Detail>? Details { get; } = new List<Detail>();
    }
}
