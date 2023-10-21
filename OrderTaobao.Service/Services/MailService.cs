
using BaseSource.Dto;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using BaseSource.Model;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.BackendAPI.Services
{
    public interface IMailService
    {
        Task<bool> SendMail(MailRequest request);

    }

    public class MailService : IMailService
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepository<ResetPassword> _repository;
        public MailService(UserManager<User> userManager,IRepository<ResetPassword> repository)
        {
            _userManager = userManager;
            _repository = repository;
        }
        public async Task<bool> SendMail(MailRequest request)
        {
           
            try
            {
                var id = await CreateEmailConfirm(request.To);
                if (id is null)
                {
                    return false;
                }
                var email = new MimeMessage();

                email.From.Add(new MailboxAddress("OrderTaobao Website", request.From));
                email.To.Add(new MailboxAddress("Receiver Name", request.To));

                email.Subject = request.Subject;
                var bodyBuilder = new BodyBuilder();

                bodyBuilder.HtmlBody = TemplateMail(request, id);
                email.Body = bodyBuilder.ToMessageBody();

                using (var smtp = new SmtpClient())
                {
                    smtp.Connect("smtp.gmail.com", 587, false);
                    // Note: only needed if the SMTP server requires authentication
                    smtp.Authenticate("hungbanghung@gmail.com", "iaco jjnk cazh gkpq");

                    smtp.Send(email);
                    smtp.Disconnect(true);
                }
                

            }
            catch (Exception ex)
            {
                //Error
                Console.WriteLine(ex.Message);
               
            }
            return true;
        }
        private async Task<string> CreateEmailConfirm(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return null;
            ResetPassword resetPassword = new ResetPassword
            {
                Id = Guid.NewGuid().ToString(),
                Email = email,
                IsVerify = false,
                User = user,

            };
            await _repository.Create(resetPassword, user.UserName!);

            return resetPassword.Id;
        }

        private string TemplateMail(MailRequest request,string id)
        {
            return $"<div style=\"max-width:560px;padding:20px 0;margin:0 auto;font-family:Open Sans,Helvetica,Arial;font-size:15px;color:#666\"><div class=\"adM\">\r\n</div><div style=\"background:#080606;padding:10px 20px\"><div class=\"adM\">\r\n</div><div style=\"text-align:center;font-weight:600;font-size:14px;padding:10px 0;color:#fff\">OrderTaobao.net - Web Đặt Hàng #1</div>\r\n<div style=\"clear:both\">&nbsp;</div>\r\n</div>\r\n<div style=\"padding:0 30px 30px 30px;border-bottom:1px solid #eeeeee;background:#fff\">\r\n<div style=\"padding:30px 0;font-size:24px;text-align:center;line-height:40px\">{request.Body}</div>\r\n<div style=\"padding:10px 0 20px 0;text-align:center\"><a style=\"background:#555555;color:#fff;padding:12px 30px;text-decoration:none;border-radius:3px;letter-spacing:0.3px\" href=\"http://localhost:3000/auth/reset-password?reset={id}&email={request.To}\" target=\"_blank\">Thay đổi mật khẩu</a></div>\r\n<div style=\"padding:20px;text-align:center\">Nếu có vấn đề liên hệ qua <a style=\"color:#3ba1da;text-decoration:none\" href=\"mailto:hungtq1016@gmail.com\" target=\"_blank\">mail hungtq1016@gmail.com</a> nhé</div>\r\n</div>\r\n<div style=\"color:#585858;padding:10px 20px;text-align:center;background:#fff;font-size:13px\">\r\n<div>Copyright © 2022 - 2023 OrderTaobao</div><div class=\"yj6qo\"></div><div class=\"adL\">\r\n</div></div><div class=\"adL\">\r\n</div></div>";
        }
    }
}
