using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        public async Task<IActionResult> Login(LoginDto model)
        {
            ResponseDto result = await _authenService.Login(model);
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
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin(RegisterDto model)
        {
            ResponseDto result = await _authenService.Register(model, UserRoles.Admin);
            if (result.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,result);
            }
            else
            {
                return Ok(result);
            }
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            ResponseDto result = await _authenService.Register(model, "Customer");
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
