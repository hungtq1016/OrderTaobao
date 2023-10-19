
namespace BaseSource.Dto
{
    public class PermissionResponse<T> : Response<T>
    {
        public bool IsAuthen { get; set; }
        public bool AdminPermission { get; set; }

        public PermissionResponse()
        {
            IsAuthen = false;
            AdminPermission = false;
        }
    }
}
