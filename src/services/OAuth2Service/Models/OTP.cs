namespace OAuth2Service.Models
{
    public class OTP : Entity
    {
        public string Email { get; set; }
        public DateTime ExpiredTime { get; set; }
        public int Code { get; set; }
    }
}
