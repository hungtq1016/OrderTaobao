using Infrastructure.EFCore.Controllers;
using Microsoft.AspNetCore.Mvc;
using OAuth2Service.DTOs;
using OAuth2Service.Models;
using OAuth2Service.Services;

namespace AuthorizeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ResourceController<Role,RoleRequest,RoleResponse>
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService) : base(roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("ByUserId/{userId:Guid}")]
        public async Task<IActionResult> GetRolesByUserId(Guid userId)
        {
            var response = await _roleService.FindAllRolesByUserId(userId);
            return StatusCode(response.StatusCode, response.Data);
        }
    }
}
