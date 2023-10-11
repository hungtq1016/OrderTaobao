using Microsoft.AspNetCore.Mvc;

namespace BaseSource.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService _authenService;
        public AuthenticateController(IAuthenticateService authenService)
        {
            _authenService = authenService;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            AuthenResponse result = await _authenService.Login(model);
            if (result.Error)
            {
                return Unauthorized(result);
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            AuthenResponse result = await _authenService.Register(model, UserRoles.Customer);
            if (result.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
            else
            {
                return Ok(result);
            }
        }
    }
}
