
namespace BaseSource.Dto
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; } = string.Empty;
        public bool Error { get; set; }
        public string? Data { get; set; } = string.Empty;
    }
}
