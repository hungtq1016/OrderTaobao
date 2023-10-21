using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService _authenService;
        private readonly IAuthHistoryService _historyService;
        private readonly IUserService _userService;

        public AuthenticateController(IAuthenticateService authenService, IAuthHistoryService historyService, IUserService userService)
        {
            _authenService = authenService;
            _historyService = historyService;
            _userService = userService;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _authenService.Login(request);
            
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
            var result = await _authenService.Register(request, RolePermission.Customer);
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
        [Route("logout")]
        public async Task<IActionResult> LogOut(TokenRequest request)
        {
            var user = await _authenService.GetUserByToken(request);
            await _historyService.CreateAuthHistory(user,"Đã đăng xuất");
            return Ok();
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

        [HttpPost]
        [Route("reset-password")]
        public async Task<ActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var result = await _userService.UpdatePassword(request);
            if (result)
            {
                return Ok(new Response<bool>(true));
            }
            else
            {
                return Ok(new Response<bool> { Data = false, Error = true, Message = "Có lỗi xảy ra", StatusCode = 200 });
            }

        }

    }
}
