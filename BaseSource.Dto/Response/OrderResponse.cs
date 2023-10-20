

namespace BaseSource.Dto
{
    public class OrderResponse
    {
        public string Id { get; set; }
        public int Status { get; set; }
        public bool Enable { get; set; }
        public DateTime CreatedAt { get; set; } 
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; } 
        public string UpdatedBy { get; set; }
        public List<OrderDetailResponse>? Details { get; set; }
    }
}
