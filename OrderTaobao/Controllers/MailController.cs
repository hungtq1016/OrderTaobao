using Microsoft.AspNetCore.Mvc;

namespace BaseSource.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;

        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }
        [HttpPost]
        [Route("reset-password")]
        public async Task<ActionResult> ResetPassword(string mail)
        {
            if (mail is null)
            {
                return BadRequest();
            }
            MailRequest mailRequest = new MailRequest
            {
                From = "hungtq1016@gmail.com",
                To = mail,
                Subject = "Thay đổi mật khẩu",
                Body = "Chúng tôi đã xử lý yêu cầu thay đổi mật khẩu của bạn. Nếu chính bạn là người gửi yêu cầu này thì ấn vào link bên dưới để thay đổi mật khẩu."
            };
            var result = await _mailService.SendMail(mailRequest);
            if (result)
            {
                return Ok(new Response<bool>(true));
            }
            else
            {
                return Ok(new Response<bool> { Data = false,Error = true,Message="Có lỗi xảy ra",StatusCode = 200});
            }

        }

        
    }
}
