namespace OAuth2Service.DTOs
{
    public class TokenRequest
    {
        public string? AccessToken { get; set; } = null;
        public string? RefreshToken { get; set; } = null;
    }
}
