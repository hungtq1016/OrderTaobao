namespace OAuth2Service.DTOs
{
    public class RoleResponse 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public bool Enable { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
