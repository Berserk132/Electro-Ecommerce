using Electro_Project.Models.Mail;

namespace Electro_Project.Models.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);

    }
}
