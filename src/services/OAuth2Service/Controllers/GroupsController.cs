using Infrastructure.EFCore.Controllers;
using Infrastructure.EFCore.Service;
using Microsoft.AspNetCore.Mvc;
using OAuth2Service.Models;
using OAuth2Service.DTOs;

namespace AuthorizeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : SingletonController<Group, GroupRequest, GroupResponse>
    {
        public GroupsController(IService<Group, GroupRequest, GroupResponse> service) : base(service)
        {
        }
    }
}
