using Electro_Project.Models.Mail;
using Electro_Project.Models.Mail.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Electro_Project.Models.Services
{
    /*IMailService,*/
    public class MailService : IEmailSender
    {
        private readonly MailSettings _mailSettings;

        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        #region Test
        //public async Task SendEmailAsync(MailRequest mailRequest)
        //{
        //    var email = new MimeMessage();
        //    email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
        //    email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
        //    email.Subject = mailRequest.Subject;
        //    var builder = new BodyBuilder();

        //    #region Attachments
        //    //if (mailRequest.Attachments != null)
        //    //{
        //    //    byte[] fileBytes;
        //    //    foreach (var file in mailRequest.Attachments)
        //    //    {
        //    //        if (file.Length > 0)
        //    //        {
        //    //            using (var ms = new MemoryStream())
        //    //            {
        //    //                file.CopyTo(ms);
        //    //                fileBytes = ms.ToArray();
        //    //            }
        //    //            builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
        //    //        }
        //    //    }
        //    //}
        //    #endregion

        //    builder.HtmlBody = mailRequest.Body;
        //    email.Body = builder.ToMessageBody();
        //    using var smtp = new SmtpClient();
        //    smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
        //    smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
        //    await smtp.SendAsync(email);
        //    smtp.Disconnect(true);
        //}
        #endregion

        #region Simple Email
        //public async Task SendEmailAsync(string _email, string subject, string htmlMessage)
        //{
        //    var email = new MimeMessage();
        //    email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
        //    email.To.Add(MailboxAddress.Parse(_email));
        //    email.Subject = subject;
        //    var builder = new BodyBuilder();

        //    #region Attachments
        //    //if (mailRequest.Attachments != null)
        //    //{
        //    //    byte[] fileBytes;
        //    //    foreach (var file in mailRequest.Attachments)
        //    //    {
        //    //        if (file.Length > 0)
        //    //        {
        //    //            using (var ms = new MemoryStream())
        //    //            {
        //    //                file.CopyTo(ms);
        //    //                fileBytes = ms.ToArray();
        //    //            }
        //    //            builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
        //    //        }
        //    //    }
        //    //}
        //    #endregion

        //    builder.HtmlBody = htmlMessage;
        //    email.Body = builder.ToMessageBody();
        //    using var smtp = new SmtpClient();
        //    smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
        //    smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
        //    await smtp.SendAsync(email);
        //    smtp.Disconnect(true);
        //}
        #endregion


        public async Task SendEmailAsync(string userName, string _email, string token)
        {
            string FilePath = Directory.GetCurrentDirectory() + @"\Views\WelcomeTemplate.html";
            StreamReader str = new StreamReader(FilePath);

            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[username]", userName).Replace("[email]", _email)
            .Replace("[confirm]", token);

            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(_email));
            email.Subject = $"Welcome {userName}";

            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }


    }
}