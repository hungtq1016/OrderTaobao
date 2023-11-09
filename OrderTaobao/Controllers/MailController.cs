using Microsoft.AspNetCore.Mvc;

namespace BaseSource.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : StatusController
    {
        private readonly IMailService _mailService;

        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }

        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword(MailRequest request)
        {
            if (request is null)
                return BadRequest();
            var result = await _mailService.SendMail(request,"reset-password");
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [Route("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(MailRequest request)
        {
            if (request is null)
                return BadRequest();
            var result = await _mailService.SendMail(request, "confirm-email");
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [Route("2fa")]
        public async Task<IActionResult> TwoFactorAuthentication(MailRequest request)
        {
            if (request is null)
                return BadRequest();
            var result = await _mailService.SendMail(request, "2fa");
            return StatusCode(result.StatusCode, result);
        }
    }
}
