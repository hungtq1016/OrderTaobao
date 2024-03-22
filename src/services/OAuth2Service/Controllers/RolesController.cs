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

        [Permission("permission", "roles.view")]
        public override async Task<IActionResult> Get()
        {
            return await base.Get();
        }

        [Permission]
        public override async Task<IActionResult> Post(RoleRequest request)
        {
            return await base.Post(request);
        }

        [Permission("permission", "roles.view")]
        public override async Task<IActionResult> GetPage([FromQuery] PaginationRequest request)
        {
            return await base.GetPage(request);
        }

        [Permission("permission", "roles.view")]
        public override async Task<IActionResult> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpGet("ByUserId/{userId:Guid}")]
        public async Task<IActionResult> GetRolesByUserId(Guid userId)
        {
            var response = await _roleService.FindAllRolesByUserId(userId);
            return StatusCode(response.StatusCode, response.Data);
        }
    }
}
