using Newtonsoft.Json;

namespace Infrastructure.EFCore.DTOs
{
    public class Response<TEntity>
    {
        public TEntity Data { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public bool IsError { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class FileResponse
    {
        public Byte[] FilesBytes { get; set; }
        public string Extension { get; set; }
    }

    public class ExcelResponse : FileResponse
    {
        public string? Name { get; set; }
    }
}
