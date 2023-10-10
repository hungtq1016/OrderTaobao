using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderTaobao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthController(IAuthService service,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _service = service;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        // POST api/<AuthController>/Login
        [HttpPost("Login")]
        public async Task<ActionResult<Customer>> Login(LoginDto request)
        {
            var customer = await _service.Login(request);
            return Ok(customer);
        }

        // POST api/<AuthController>/Register
        [HttpPost("Register")]
        public async Task<ActionResult<Customer>> Register(RegisterDto request)
        {
            string lowerCase = request.UserName.ToLower();
            if (await _service.UserExists(lowerCase))
                return BadRequest("Tài khoản đã tồn tại");
            var customer = await _service.Register(request);
            return Ok(customer);
        }

    }
}
