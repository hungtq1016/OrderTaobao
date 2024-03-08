using MailService.DTOs;
using MimeKit;
using MailKit.Net.Smtp;
using MailService.Models;

namespace MailService.Infrastructure
{
    public interface IService
    {
        void SendEmail(string request);
        void SendEmailOTP(OTP request);
    }
    public class BaseMailServices : IService
    {
        public BaseMailServices()
        {

        }

        public void SendEmail(string request)
        {
            MailRequest mailRequest = new MailRequest
            {
                From = "hungtq1016@gmail.com",
                To = request, // Make sure 'request' contains a valid email address
                Subject = "Change your password",
                Body = "We have processed your password change request. If you are the one who submitted this request, please click on the link below to change your password."
            };

            string id = Guid.NewGuid().ToString();

            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("OrderTaobao Website", mailRequest.From));

            // Make sure 'request' contains a valid email address
            if (IsValidEmail(request))
            {
                email.To.Add(new MailboxAddress("Gửi", request));
            }
            else
            {
                // Handle invalid email address (log, throw exception, etc.)
                Console.WriteLine($"Invalid email address: {request}");
                return;
            }

            email.Subject = mailRequest.Subject;
            var bodyBuilder = new BodyBuilder();

            bodyBuilder.HtmlBody = TemplateMail(mailRequest, id);
            email.Body = bodyBuilder.ToMessageBody();

            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 587, false);
                // Note: only needed if the SMTP server requires authentication
                smtp.Authenticate("hungbanghung@gmail.com", "game qutq paai kazq");

                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }

        public void SendEmailOTP(OTP request)
        {
            MailRequest mailRequest = new MailRequest
            {
                From = "hungtq1016@gmail.com",
                To = request.Email, // Make sure 'request' contains a valid email address
                Subject = "One time password",
                Body = ""
            };

            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("OrderTaobao Website", mailRequest.From));

            // Make sure 'request' contains a valid email address
            if (IsValidEmail(request.Email))
            {
                email.To.Add(new MailboxAddress("Gửi", request.Email));
            }
            else
            {
                // Handle invalid email address (log, throw exception, etc.)
                Console.WriteLine($"Invalid email address: {request.Email}");
                return;
            }

            email.Subject = mailRequest.Subject;
            var bodyBuilder = new BodyBuilder();

            bodyBuilder.HtmlBody = TemplateMailOTP(request);
            email.Body = bodyBuilder.ToMessageBody();

            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 587, false);
                // Note: only needed if the SMTP server requires authentication
                smtp.Authenticate("hungbanghung@gmail.com", "game qutq paai kazq");

                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private string TemplateMail(MailRequest request, string id)
        {
            return @$"
                <div style=""max-width:560px;padding:20px 0;margin:0 auto;font-family:Open Sans,Helvetica,Arial;font-size:15px;color:#666"">
                    <div class=""adM"">
                    </div>
                    <div style=""background:#080606;padding:10px 20px"">
                        <div class=""adM"">
                        </div>
                        <div style=""text-align:center;font-weight:600;font-size:14px;padding:10px 0;color:#fff"">OrderTaobao.net - Web Đặt Hàng #1</div>
                        <div style=""clear:both"">&nbsp;</div>
                    </div>
                    <div style=""padding:0 30px 30px 30px;border-bottom:1px solid #eeeeee;background:#fff"">
                        <div style=""padding:30px 0;font-size:24px;text-align:center;line-height:40px"">{request.Body}</div>
                        <div style=""padding:10px 0 20px 0;text-align:center""><a style=""background:#555555;color:#fff;padding:12px 30px;text-decoration:none;border-radius:3px;letter-spacing:0.3px"" href=""https://www.google.com?q={id}&email={request.To}"" target=""_blank"">Thay đổi mật khẩu</a></div>
                        <div style=""padding:20px;text-align:center"">Nếu có vấn đề liên hệ qua <a style=""color:#3ba1da;text-decoration:none"" href=""mailto:hungtq1016@gmail.com"" target=""_blank"">mail hungtq1016@gmail.com</a> nhé</div>
                    </div>
                    <div style=""color:#585858;padding:10px 20px;text-align:center;background:#fff;font-size:13px"">
                        <div>Copyright © 2022 - 2023 OrderTaobao</div>
                        <div class=""yj6qo""></div>
                        <div class=""adL"">
                        </div>
                    </div>
                    <div class=""adL"">
                    </div>
                </div>";
        }

        private string TemplateMailOTP(OTP request)
        {
            return @$"<div style=""font-family: Helvetica,Arial,sans-serif;min-width:1000px;overflow:auto;line-height:2"">
                        <div style=""margin:50px auto;width:70%;padding:20px 0"">
                            <div style=""border-bottom:1px solid #eee"">
                                <a href="""" style=""font-size:1.4em;color: #00466a;text-decoration:none;font-weight:600"">Order Taobao</a>
                            </div>
                            <p style=""font-size:1.1em"">Hi,{request.Email}</p>
                            <p>Thank you for choosing Order Taobao.<br/> Use the following OTP to complete your Sign Up procedures.<br/> OTP is valid for 5 minutes</p>
                            <h2 style=""background: #00466a;margin: 0 auto;width: max-content;padding: 0 10px;color: #fff;border-radius: 4px;"">{request.Code}</h2>
                            <p style=""font-size:0.9em;"">Regards,<br />Order Taobao</p>
                            <hr style=""border:none;border-top:1px solid #eee"" />
                            <div style=""float:right;padding:8px 0;color:#aaa;font-size:0.8em;line-height:1;font-weight:300"">
                                <p>Order Taobao Inc</p>
                                <p>Ho Chi Minh City</p>
                                <p>VietNam</p>
                            </div>
                        </div>
                    </div>";
        }
    }
}
