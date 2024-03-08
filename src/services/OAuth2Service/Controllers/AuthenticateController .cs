using OAuth2Service.Authen;

namespace OAuth2Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenService _service;

        public AuthenticateController(IAuthenService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _service.LoginAsync(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _service.RegisterAsync(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("send-reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var result = await _service.SendResetPasswordAsync(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("send-otp")]
        public async Task<IActionResult> SendOTP([FromBody] EmailRequest request)
        {
            var result = await _service.SendOTPAsync(request.Email);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("receive-otp")]
        public async Task<IActionResult> ReceiveOTP([FromBody] OTPRequest request)
        {
            var result = await _service.ReceiveOTPAsync(request);
            return StatusCode(result.StatusCode, result);
        }
    }
}
