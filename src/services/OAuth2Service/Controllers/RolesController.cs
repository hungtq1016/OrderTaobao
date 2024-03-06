namespace OAuth2Service.Controllers
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
