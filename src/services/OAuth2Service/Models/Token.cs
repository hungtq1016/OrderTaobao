namespace OAuth2Service.Models
{
    public class Token
    {
        public Guid Id { get; set; }
        public string RefreshToken { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public DateTime ExpiredTime { get; set; }
    }
}
