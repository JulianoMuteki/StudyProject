using System.Threading.Tasks;

namespace StudyProject.Application.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
