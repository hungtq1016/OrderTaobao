
namespace BaseSource.Dto
{
    public class PermissionResponse<T> 
    {
        public T Data { get; set; }
        public bool IsAuthen { get; set; }
        public bool IsAdmin { get; set; }

        public PermissionResponse()
        {
            IsAuthen = false;
            IsAdmin = false;
        }
    }
}
