namespace OrderService.DTOs
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public byte Status { get; set; }
        public bool Enable { get; set; }
        public DateTime CreatedAt { get; set; } 
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; } 
        public string UpdatedBy { get; set; }
        public List<DetailResponse>? Details { get; set; }
    }
}
