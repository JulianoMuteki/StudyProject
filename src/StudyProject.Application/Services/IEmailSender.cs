using System.Threading.Tasks;

namespace StudyProject.Application.Services
{
    public interface ICustomEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
