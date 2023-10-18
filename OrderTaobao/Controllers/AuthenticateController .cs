using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Login(LoginRequest request)
        {
            AuthenResponse result = await _authenService.Login(request);
            
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
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            AuthenResponse result = await _authenService.Register(request, UserRoles.Customer);
            if (result.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPost]
        [Route("refresh-token")]
        
        public async Task<IActionResult> RefreshToken(TokenRequest request)
        {
            if (request is null)
            {
                return Ok();
            }
            var result = await _authenService.RefreshToken(request);
            return Ok(result);
        }

        

    }
}
