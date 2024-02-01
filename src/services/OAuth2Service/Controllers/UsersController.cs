using Infrastructure.EFCore.Controllers;
using Infrastructure.EFCore.Service;
using Microsoft.AspNetCore.Mvc;
using OAuth2Service.DTOs;
using OAuth2Service.Models;

namespace BaseSource.BackendAPI.Controllers
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
