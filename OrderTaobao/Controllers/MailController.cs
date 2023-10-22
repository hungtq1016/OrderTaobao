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
        public async Task<IActionResult> ResetPassword(string request)
        {
            return await PerformAction(request, _mailService.SendMail);
        }    
    }
}
