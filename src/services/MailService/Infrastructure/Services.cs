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
            return @$"<div style=""box-sizing:inherit;margin:0;color:rgba(0,0,0,0.87);font-family:'Roboto','Helvetica','Arial',sans-serif;font-weight:400;font-size:1rem;line-height:1.5;letter-spacing:0.00938em;background-color:#000000""><div class=""adM"">
                    </div><div style=""box-sizing:inherit;font-weight:400;font-size:16px;padding:32px 0;margin:0;letter-spacing:0.15008px;line-height:1.5;background-color:#000000;font-family:'Iowan Old Style','Palatino Linotype','URW Palladio L',P052,serif;color:#ffffff""><div class=""adM"">
                      </div><div id=""m_6480876213820302322__react-email-preview"" style=""box-sizing:inherit;display:none;overflow:hidden;line-height:1px;opacity:0;max-height:0;max-width:0"">This code will expire in 30 minutes.<div style=""box-sizing:inherit"">&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌<wbr>​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍<wbr>‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏<wbr>﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌<wbr>​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍<wbr>‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏<wbr>﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌<wbr>​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍<wbr>‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏<wbr>﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌<wbr>​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍<wbr>‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏<wbr>﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌<wbr>​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍<wbr>‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏<wbr>﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌<wbr>​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍<wbr>‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏<wbr>﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌<wbr>​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍<wbr>‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏<wbr>﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌<wbr>​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍<wbr>‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏<wbr>﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌<wbr>​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍<wbr>‎‏﻿&nbsp;‌​‍‎‏﻿</div>
                      </div>
                      <table align=""center"" width=""100%"" style=""box-sizing:inherit;background-color:#000000;max-width:600px;min-height:48px"" role=""presentation"" cellspacing=""0"" cellpadding=""0"" border=""0"" bgcolor=""#000000"">
                        <tbody style=""box-sizing:inherit"">
                          <tr style=""box-sizing:inherit;width:100%"">
                            <td style=""box-sizing:inherit"">
                              <div style=""color:#ffffff;font-family:inherit;font-size:16px;font-weight:normal;padding:16px 24px 16px 24px;text-align:center;max-width:100%;box-sizing:border-box"">

                                <div style=""box-sizing:inherit"">
                                  <div style=""box-sizing:inherit"">
                                    <p style=""box-sizing:inherit;margin-top:0px;margin-bottom:0px"">We have processed your password change request.<br/> If you are the one who submitted this request,<br/> please click on the link below to change your password.</p>
                                  </div>
                                </div>
                              </div>
                              <div style=""font-family:inherit;font-weight:bold;padding:16px 24px 16px 24px;text-align:center;max-width:100%;box-sizing:border-box"">
                                <a href=""https://www.google.com?q={id}&email={request.To}"" style=""background:#00466a;margin:0 auto;width:max-content;padding:8px 14px;color:#fff;border-radius:4px; text-decoration: none; font-size: 24px;"">Change password</a>
                            </div>
                              <div style=""color:#868686;font-family:inherit;font-size:14px;font-weight:normal;padding:16px 24px 16px 24px;text-align:center;max-width:100%;box-sizing:border-box"">
                                <div style=""box-sizing:inherit"">
                                  <div style=""box-sizing:inherit"">
                                    <p style=""box-sizing:inherit;margin-top:0px;margin-bottom:0px""><em style=""box-sizing:inherit"">Problems? Just reply to <a href=""mailto:hungbanghung@gmail.com"" target=""_blank"" jslog=""32272; 1:WyIjdGhyZWFkLWY6MTc5MzA0NTQ1MDA1NzQxOTE3NiJd; 4:WyIjbXNnLWY6MTc5MzA0NjQyOTk2NjczMjEyMiJd"">this email</a>.</em></p>
                                  </div>
                                </div>
                              </div>
                            </td>
                          </tr>
                        </tbody>
                      </table><div class=""yj6qo""></div><div class=""adL"">
                    </div></div><div class=""adL"">
                    </div></div>";
        }

        private string TemplateMailOTP(OTP request)
        {
            return @$"<div style=""box-sizing: inherit; margin: 0; color: rgba(0, 0, 0, 0.87); font-family: 'Roboto', 'Helvetica', 'Arial', sans-serif; font-weight: 400; font-size: 1rem; line-height: 1.5; letter-spacing: 0.00938em; background-color: #000000;"">
                  <div class=""MuiBox-root css-1p9u5cx"" style=""box-sizing: inherit; font-weight: 400; font-size: 16px; padding: 32px 0; margin: 0; letter-spacing: 0.15008px; line-height: 1.5; background-color: #000000; font-family: 'Iowan Old Style', 'Palatino Linotype', 'URW Palladio L', P052, serif; color: #ffffff;"">
                    <div id=""__react-email-preview"" style=""box-sizing: inherit; display: none; overflow: hidden; line-height: 1px; opacity: 0; max-height: 0; max-width: 0;"">This code will expire in 30 minutes.<div style=""box-sizing: inherit;"">&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿&nbsp;‌​‍‎‏﻿</div>
                    </div>
                    <table align=""center"" width=""100%"" style=""box-sizing: inherit; background-color: #000000; max-width: 600px; min-height: 48px;"" role=""presentation"" cellspacing=""0"" cellpadding=""0"" border=""0"" bgcolor=""#000000"">
                      <tbody style=""box-sizing: inherit;"">
                        <tr style=""box-sizing: inherit; width: 100%;"">
                          <td style=""box-sizing: inherit;"">
                            <div style=""color: #ffffff; font-family: inherit; font-size: 16px; font-weight: normal; padding: 16px 24px 16px 24px; text-align: center; max-width: 100%; box-sizing: border-box;"">
              
                              <div class=""MuiBox-root css-vii0ua"" style=""box-sizing: inherit;"">
                                <div style=""box-sizing: inherit;"">
                                  <p style=""box-sizing: inherit; margin-top: 0px; margin-bottom: 0px;"">Here is your one-time passcode:</p>
                                </div>
                              </div>
                            </div>
                            <div style=""font-family: inherit; font-weight: bold; padding: 16px 24px 16px 24px; text-align: center; max-width: 100%; box-sizing: border-box;"">
                              <h1 style=""box-sizing: inherit; margin-top: 40px; margin-bottom: 16px; font-weight: inherit; margin: 0; font-size: 32px;"">{request.Code}</h1>
                            </div>
                            <div style=""color: #868686; font-family: inherit; font-size: 16px; font-weight: normal; padding: 16px 24px 16px 24px; text-align: center; max-width: 100%; box-sizing: border-box;"">
                              <div class=""MuiBox-root css-vii0ua"" style=""box-sizing: inherit;"">
                                <div style=""box-sizing: inherit;"">
                                  <p style=""box-sizing: inherit; margin-top: 0px; margin-bottom: 0px;"">This code will expire in 5 minutes.</p>
                                </div>
                              </div>
                            </div>
                            <div style=""color: #868686; font-family: inherit; font-size: 14px; font-weight: normal; padding: 16px 24px 16px 24px; text-align: center; max-width: 100%; box-sizing: border-box;"">
                              <div class=""MuiBox-root css-vii0ua"" style=""box-sizing: inherit;"">
                                <div style=""box-sizing: inherit;"">
                                  <p style=""box-sizing: inherit; margin-top: 0px; margin-bottom: 0px;""><em style=""box-sizing: inherit;"">Problems? Just reply to <a href=""mailto:hungbanghung@gmail.com"">this email</a>.</em></p>
                                </div>
                              </div>
                            </div>
                          </td>
                        </tr>
                      </tbody>
                    </table>
                  </div>
                </div>";
        }
    }
}
