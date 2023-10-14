using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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

        [HttpPost]
        [Route("check-oauth")]
        [Authorize]
        public async Task<IActionResult> CheckOAuth(TokenRequest model)
        {

            var roles = await _authenService.CheckOAuth(model);
            if (roles is null)
            {
                return Unauthorized();
            }
            else
            {
                return Ok(roles);
            }
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenRequest request)
        {
            if (request is null)
            {
                return BadRequest();
            }
            var result = await _authenService.RefreshToken(request);
            if (result is null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("access-token")]
        public async Task<IActionResult> AccessToken(TokenRequest request)
        {
            if (request is null)
            {
                return BadRequest();
            }
            var result = await _authenService.CreateNewAccessToken(request);
            if (result is null)
            {
                return Unauthorized();
            }
            return Ok(result);
        }
    }
}
