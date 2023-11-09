using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : StatusController
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles([FromQuery] PaginationRequest request)
        {
            var result = await _roleService.GetPagedData(request, Request.Path.Value!);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClaimByRole(string id)
        {
            return await PerformAction(id, _roleService.GetClaimByRole);
        }

        [HttpPost("claim")]
        public async Task<IActionResult> AddClaimToRole(IdentityRoleClaim<string> request)
        {
            return await PerformAction(request, _roleService.AddClaim);
        }

    }
}
