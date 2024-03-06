namespace OAuth2Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ResourceController<Permission, PermissionRequest, PermissionResponse>
    {
        public PermissionsController(IService<Permission, PermissionRequest,PermissionResponse> service) : base(service)
        {
        }
    }
}
