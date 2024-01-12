using BaseSource.BackendAPI.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> GetClaimByRole(string id, [FromQuery] PaginationRequest request)
        {
            var result = await _roleService.GetClaimByRole(request, id, Request.Path.Value!);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> AddClaimToRole(string id, IdentityRoleClaim<string> request)
        {
            var result = await _roleService.AddClaim(request);
            return StatusCode(result.StatusCode, result);
        }

    }
}
