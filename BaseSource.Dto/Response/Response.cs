
namespace BaseSource.Dto
{
    public class Response<T>
    {
        public Response()
        {
        }
        public Response(T data)
        {
            Message = string.Empty;
            Error = false;
            Data = data;
            StatusCode = 200;
        }
        public T Data { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}
