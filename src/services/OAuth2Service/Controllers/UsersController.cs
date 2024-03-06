namespace OAuth2Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ResourceController<User, UserRequest, UserResponse>
    {
        public UsersController(IService<User, UserRequest, UserResponse> service) : base(service)
        {
        }
    }
}
