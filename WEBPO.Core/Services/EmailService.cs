
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using WEBPO.Core.Interfaces;
using WEBPO.Core.Persistances;

namespace WEBPO.Core.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration EmailConfig;
        private readonly MailMessage mailMessage;
        public EmailService(EmailConfiguration emailConfig)
        {
            EmailConfig = emailConfig;
            mailMessage = new MailMessage()
            {
                From = new MailAddress(EmailConfig.UserName, EmailConfig.DisplayName),
                IsBodyHtml = true,
                BodyEncoding = Encoding.UTF8
            };
        }

        private string GetMessageForResetPassword() {
            var htmlBody = "";
            return htmlBody;
        }

        public async Task<bool> SendForResetPassword(ICollection<string> receivers, string subject, string message = null)
        {
            if (string.IsNullOrEmpty(message)) message = GetMessageForResetPassword();

            mailMessage.Subject = subject;
            mailMessage.Body = message;

            if (receivers != null) {
                foreach (var receive in receivers) {
                    mailMessage.To.Add(receive);
                }
            }

           return await SendMail(mailMessage);
        }

        //private void doSendMail(SmtpClient client, MailMessage mailMessage) {

        //    if (!string.IsNullOrEmpty(EmailConfig.IncludeTestEMail))  {
        //        mailMessage.To.Clear();
        //        mailMessage.To.Add(EmailConfig.IncludeTestEMail);
        //    }

        //    client.Send(mailMessage);
        //}
        private async Task<bool> SendMail(MailMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Port = EmailConfig.Port;
                    client.EnableSsl = EmailConfig.EnableSsl;
                    client.Host = EmailConfig.Host;
                    client.UseDefaultCredentials = EmailConfig.UseDefaultCredentials;
                    client.Credentials = new NetworkCredential(EmailConfig.UserName, EmailConfig.Password);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;

                    if (!string.IsNullOrEmpty(EmailConfig.IncludeTestEMail))
                    {
                        mailMessage.To.Clear();
                        mailMessage.To.Add(EmailConfig.IncludeTestEMail);
                    }

                    client.Send(mailMessage);
                    return true;
                } catch(Exception ex) {
                    return false;
                }

            }
        }
    }
}
