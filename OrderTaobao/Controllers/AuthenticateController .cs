using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : StatusController
    {
        private readonly IAuthenticateService _authenService;
        private readonly IUserService _userService;

        public AuthenticateController(IAuthenticateService authenService, IUserService userService)
        {
            _authenService = authenService;
            _userService = userService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            return await PerformAction(request, _authenService.Login);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            return await PerformAction(request, _authenService.Register);
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> LogOut(TokenRequest request)
        {
            return await PerformAction(request, _authenService.Logout);
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenRequest request)
        {
            return await PerformAction(request, _authenService.RefreshToken);
        }

        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            return await PerformAction(request, _userService.UpdatePassword);
        }

    }
}
