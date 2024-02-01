using Infrastructure.EFCore.Controllers;
using Infrastructure.EFCore.Service;
using Microsoft.AspNetCore.Mvc;
using OAuth2Service.Models;
using OAuth2Service.DTOs;

namespace AuthorizeService.Controllers
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
