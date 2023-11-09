
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace BaseSource.Dto
{
    public class Response<T>
    {
        public Response()
        {
        }
        public Response(T data)
        {
            Message = "Thành Công!";
            Error = false;
            Data = data;
            StatusCode = 200;
        }
        public T? Data { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; } = string.Empty;
        public UInt16 StatusCode { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
