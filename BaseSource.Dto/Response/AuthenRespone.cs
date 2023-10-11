
namespace BaseSource.Dto
{
    public class AuthenResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool Error { get; set; }
        public string? Data { get; set; } = null;
        public TokenResponse? Token { get; set; }
    }
}
